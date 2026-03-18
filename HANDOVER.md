# Narrative Suite — Handover Tecnico

> Stato: **Demo funzionante su seed data**. Il frontend è completo, il backend è una facciata read-only.
> Tutto il "motore" (scan, API esterne, calcolo metriche, generazione briefing) è da costruire.

---

## Architettura attuale

```
┌─────────────┐     ┌──────────────┐     ┌─────────┐
│  SvelteKit   │────▶│  ASP.NET 8   │────▶│ MySQL 8 │
│  (frontend)  │     │  (read API)  │     │  (seed) │
└─────────────┘     └──────────────┘     └─────────┘
       :3000              :5080              :3306
```

**Docker Compose**: 3 servizi (frontend, backend, db). Funziona. Deploy su Contabo VPS via sslip.io.

---

## Cosa FUNZIONA oggi

| Componente | Stato | Note |
|---|---|---|
| Frontend (15 pagine) | ✅ Completo | Tutte le route, UI pulita, responsive |
| Database schema | ✅ Completo | 16 entità, relazioni, indici |
| Seed data (Pirelli demo) | ✅ Completo | ~644 righe, dati realistici italiani |
| API read-only (6 GET) | ✅ Funzionante | Projects, Scans, Metrics, Results, Insights, AI-Visibility |
| Docker deploy | ✅ Funzionante | Multi-stage build, healthcheck |
| JWT auth config | ✅ Configurato | Ma `[Authorize]` non è su nessun endpoint |

## Cosa NON ESISTE

| Componente | Stato | Effort stimato |
|---|---|---|
| Services layer | ❌ Zero | — |
| Scan engine (orchestratore) | ❌ Zero | L |
| DataForSEO client (SERP) | ❌ Zero | M |
| LLM fanout (5 modelli AI) | ❌ Zero | M |
| AI Analysis (sentiment, temi) | ❌ Zero | M |
| Metric calculation engine | ❌ Zero | M |
| Briefing generation (LLM) | ❌ Zero | S |
| Insight generation (LLM) | ❌ Zero | M |
| Background job processor | ❌ Zero | M |
| POST/PUT/DELETE endpoints | ❌ Zero | S-M |
| Auth endpoints (login/register) | ❌ Zero | S |
| `[Authorize]` sui controller | ❌ Zero | XS |
| RAGFlow integration | ❌ Zero | L |
| Scheduled scans (cron) | ❌ Zero | S |
| EF Migrations (prod-ready) | ❌ Zero | XS |

---

## Piano di implementazione — Priorità

### Fase 1: MVP Scansione (la dashboard mostra dati VERI)

Obiettivo: lanciare una scan per un progetto e popolare il DB con dati reali.

#### 1.1 — DataForSEO Client `Services/DataForSeoService.cs`
- **API**: DataForSEO SERP API + Google News API
- **Input**: keyword[], language, location_code
- **Output**: SerpResult[] (position, url, title, snippet, domain)
- **Config da aggiungere** in appsettings.json:
  ```json
  "DataForSeo": {
    "Login": "...",
    "Password": "..."
  }
  ```
- **Costo**: ~$0.01-0.05 per keyword per source. Con 10 keyword × 3 source = ~$1.50/scan
- Il campo `Project.LocationCode = 2380` (Italy) è già predisposto
- `Project.Sources` definisce quali fonti attivare (`google_organic`, `google_news`, `ai_overview`)

#### 1.2 — LLM Fanout Service `Services/LlmFanoutService.cs`
- Per ogni keyword, interrogare 5 modelli AI: "Cosa sai di [keyword] nel contesto [brand]?"
- **API keys necessarie**:
  ```json
  "Llm": {
    "OpenAiKey": "...",
    "GeminiKey": "...",
    "PerplexityKey": "...",
    "GrokKey": "...",
    "ClaudeKey": "..."
  }
  ```
- **Output**: AiVisibility record (response_text, brand_mentioned, visibility_score)
- **Costo**: ~$0.02-0.10 per keyword per modello. Con 10 keyword × 5 modelli = ~$5-10/scan
- Prompt suggerito: "Quali sono i migliori {keyword}? Includi brand specifici e motivazioni."

#### 1.3 — AI Analysis Service `Services/AiAnalysisService.cs`
- Prende ogni SerpResult e analizza con un LLM (Claude o GPT-4):
  - Sentiment (positive/negative/neutral/mixed)
  - Temi chiave (JSON array)
  - Entità menzionate
  - È contenuto competitor? (match con `Project.Competitors`)
- **Output**: AiAnalysis record per ogni SerpResult
- Può usare lo stesso OpenAI/Claude key della 1.2
- Batch processing consigliato per costi

#### 1.4 — Metric Calculator `Services/MetricCalculatorService.cs`
- Calcola i 4 KPI dalla scan completata:
  - **NsoV** (Narrative Share of Voice): % di risultati SERP dove il brand appare vs competitor
  - **TOS** (Topic Opportunity Score): keyword dove il brand è assente ma i competitor presenti
  - **NRI** (Narrative Risk Indicator): % di sentiment negativo / menzioni avverse
  - **TAI** (Trend Acceleration Index): delta volume menzioni vs scan precedente
- L'entità `Metric.Breakdown` (JSON) è già predisposta per il dettaglio

#### 1.5 — Briefing Generator `Services/BriefingService.cs`
- Prende metriche + insight + risultati chiave
- Genera `Scan.AiBriefing` con un singolo prompt LLM
- Prompt: "Sei un analista strategico. Genera un briefing esecutivo in italiano basato su questi dati: [metriche, top insight, variazioni vs settimana precedente]"

#### 1.6 — Scan Orchestrator `Services/ScanOrchestrator.cs`
- Crea una Scan, popola la JobQueue, esegue in sequenza:
  1. `serp` jobs → DataForSeoService (per ogni keyword × source)
  2. `llm_fanout` jobs → LlmFanoutService (per ogni keyword × modello)
  3. `analysis` jobs → AiAnalysisService (per ogni SerpResult)
  4. `metrics` job → MetricCalculatorService
  5. `briefing` job → BriefingService
- Aggiorna `Scan.CompletedTasks` ad ogni step
- Setta `Scan.Status = completed` alla fine
- **Hosting**: `IHostedService` o Hangfire

#### 1.7 — POST endpoint per trigger scan
```csharp
POST /api/v1/projects/{slug}/scans
→ Crea Scan + avvia ScanOrchestrator in background
→ Return 202 Accepted + scan ID
```

---

### Fase 2: Insight automatici

#### 2.1 — Insight Generator `Services/InsightGeneratorService.cs`
- Post-scan, analizza i dati e genera insight con LLM:
  - **gap**: keyword dove il brand non è presente ma i competitor sì
  - **opportunity**: trend in crescita + bassa competizione
  - **risk**: sentiment negativo in aumento
  - **trend**: TAI alto su keyword specifiche
- Salva come `Insight` records con score calcolato

---

### Fase 3: Piano editoriale e contenuti

#### 3.1 — CRUD Editorial Plan
```
POST   /api/v1/projects/{slug}/editorial-plans
PUT    /api/v1/projects/{slug}/editorial-plans/{id}
DELETE /api/v1/projects/{slug}/editorial-plans/{id}
```
- Le entità `EditorialPlan`, `ContentBrief`, `ContentDraft` esistono già nel DB

#### 3.2 — Content Brief Generator
- Da un `EditorialPlan` con keyword target, genera un `ContentBrief`:
  - Analizza top 5 SERP per quella keyword
  - Suggerisce struttura (H1, H2s)
  - Identifica subtopic da coprire
- LLM-based, salva in `ContentBrief.SuggestedStructure` (JSON)

#### 3.3 — Brand Voice Check `Services/BrandVoiceService.cs`
- Analizza un `ContentDraft.ContentHtml` vs il tone of voice del brand
- Restituisce score + suggerimenti
- Salva in `ContentDraft.VoiceCheckScore` + `VoiceCheckDetails`

---

### Fase 4: Auth & multi-tenant

#### 4.1 — Auth endpoints
```
POST /api/v1/auth/login    → JWT token
POST /api/v1/auth/register → (admin only)
```
- Il modello `User` con `PasswordHash` e `Role` esiste già
- `ProjectUser` con ruoli (viewer/editor) esiste già
- Aggiungere `[Authorize]` a tutti i controller
- Filtrare progetti per user

#### 4.2 — Scheduled scans
- Leggere `Project.Schedule` (weekly/monthly)
- `IHostedService` con timer che controlla e lancia scan automatiche

---

### Fase 5: RAGFlow (opzionale, post-launch)

#### 5.1 — RAGFlow integration
- Il campo `Project.RagflowDatasetId` è già predisposto
- Upload dei contenuti brand in RAGFlow per retrieval-augmented generation
- Usare per migliorare la qualità dei briefing e dei content brief

---

## API Keys necessarie

| Servizio | Costo stimato/mese | Uso |
|---|---|---|
| **DataForSEO** | ~$50-100 | SERP scraping (10 keyword × 3 fonti × 4 scan/mese × N progetti) |
| **OpenAI** (GPT-4o) | ~$20-50 | Analysis, briefing, content brief, voice check |
| **Anthropic** (Claude) | ~$10-30 | Alternative/backup LLM per analysis |
| **Google AI** (Gemini) | ~$5-15 | LLM fanout visibility check |
| **Perplexity API** | ~$5-20 | LLM fanout visibility check |
| **xAI** (Grok) | ~$5-15 | LLM fanout visibility check |

**Totale stimato per 1 progetto, 4 scan/mese: ~$100-230/mese**

---

## File da creare (backend)

```
NarrativeSuite.Api/
├── Services/
│   ├── DataForSeoService.cs        ← SERP fetch
│   ├── LlmFanoutService.cs         ← 5-model AI visibility
│   ├── AiAnalysisService.cs        ← Sentiment/theme analysis
│   ├── MetricCalculatorService.cs   ← NsoV, TOS, NRI, TAI
│   ├── BriefingService.cs           ← Executive briefing gen
│   ├── InsightGeneratorService.cs   ← Gap/opportunity/risk detection
│   ├── ScanOrchestrator.cs          ← Job sequencing
│   ├── BrandVoiceService.cs         ← Content voice check
│   └── AuthService.cs               ← JWT login/register
├── Controllers/
│   ├── AuthController.cs            ← POST login/register
│   ├── ScanTriggerController.cs     ← POST trigger scan
│   └── EditorialController.cs       ← CRUD editorial plans
└── BackgroundJobs/
    ├── ScanWorker.cs                ← IHostedService job processor
    └── SchedulerWorker.cs           ← Cron scan scheduler
```

---

## Pagine frontend con dati mock (da collegare a API future)

Queste pagine oggi funzionano con dati hardcoded nel `+page.ts`. Quando i backend endpoint saranno pronti, basterà sostituire i mock con fetch:

| Pagina | Mock in | Endpoint futuro |
|---|---|---|
| Topics | `+page.svelte` (inline) | `GET /api/v1/projects/{slug}/topics` |
| Competitor | `+page.ts` | `GET /api/v1/projects/{slug}/competitors/analysis` |
| Trends | `+page.ts` | `GET /api/v1/projects/{slug}/trends` |
| Editorial | `+page.ts` | `GET /api/v1/projects/{slug}/editorial-plans` |
| Content | `+page.ts` | `GET /api/v1/projects/{slug}/content-briefs` |
| Export | `+page.ts` | `GET /api/v1/projects/{slug}/reports` |
| Editor | `+page.ts` (partial) | `GET /api/v1/projects/{slug}/drafts/{id}` |

---

## Quick wins per il dev

1. **Migrazioni EF** — Sostituire `EnsureCreatedAsync()` con `MigrateAsync()`. Serve per evoluzioni schema.
   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. **`[Authorize]` su tutti i controller** — 10 minuti, un attributo per controller.

3. **POST scan trigger** — Anche solo sincrono inizialmente, poi background.

4. **DataForSEO** — È la prima integrazione da fare. Senza dati SERP reali, niente funziona.

---

## Ordine di implementazione consigliato

```
Settimana 1: DataForSEO + Scan trigger + Job processor basic
Settimana 2: LLM Fanout (5 modelli) + AI Analysis
Settimana 3: Metric Calculator + Briefing Generator
Settimana 4: Insight Generator + collegare frontend mock a API reali
Settimana 5: Auth + CRUD Editorial + Content Brief
Settimana 6: Scheduled scans + Brand Voice + polish
```

---

*Generato il 18 marzo 2026 — Narrative Suite v0.1 (demo)*
