using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Models.Entities;

namespace NarrativeSuite.Api.Data;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Only seed if database is empty
        if (await db.Projects.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        // ── Admin User ──────────────────────────────────────────────
        var adminId = Guid.NewGuid().ToString();
        var admin = new User
        {
            Id = adminId,
            Email = "admin@narrative.suite",
            DisplayName = "Admin",
            Role = "admin",
            PasswordHash = "$2a$12$PLACEHOLDER_HASH_FOR_DEMO_USE_ONLY_000000000000000000",
            CreatedAt = now.AddDays(-30)
        };
        db.Users.Add(admin);

        // ── Project: Pirelli ────────────────────────────────────────
        var projectId = Guid.NewGuid().ToString();
        var project = new Project
        {
            Id = projectId,
            Slug = "pirelli",
            Name = "Pirelli",
            Industry = "Automotive / Tires",
            BrandName = "Pirelli",
            Keywords = "[\"pneumatici invernali\",\"gomme auto sportive\",\"pneumatici 4 stagioni\",\"pneumatici moto\",\"pirelli cinturato\",\"pirelli p zero\",\"gomme estive\",\"pneumatici SUV\",\"cambio gomme\",\"pirelli scorpion\"]",
            Competitors = "[\"michelin.it\",\"continental-pneumatici.it\",\"bridgestone.it\",\"goodyear.it\",\"hankook.it\"]",
            AiCompetitors = "[\"Michelin\",\"Continental\",\"Bridgestone\",\"Goodyear\",\"Hankook\"]",
            Sources = "[\"google_organic\",\"google_news\",\"ai_overview\"]",
            Schedule = "weekly",
            ScheduleDay = 1,
            Language = "it",
            LocationCode = 2380,
            Context = "Pirelli è un brand premium italiano di pneumatici, noto per le alte prestazioni in Formula 1 e nel segmento sportivo. Focus su innovazione, sicurezza e sostenibilità.",
            IsActive = true,
            CreatedAt = now.AddDays(-30)
        };
        db.Projects.Add(project);

        // ── ProjectUser ─────────────────────────────────────────────
        db.ProjectUsers.Add(new ProjectUser
        {
            ProjectId = projectId,
            UserId = adminId,
            Role = "editor"
        });

        // ── Scan ────────────────────────────────────────────────────
        var scanId = Guid.NewGuid().ToString();
        var scan = new Scan
        {
            Id = scanId,
            ProjectId = projectId,
            TriggerType = "manual",
            Status = "completed",
            TotalTasks = 50,
            CompletedTasks = 50,
            DateFrom = now.AddDays(-7),
            DateTo = now,
            AiBriefing = "Nell'ultima settimana, la Narrative Share of Voice (NsoV) di Pirelli si attesta al 34,2%, in crescita di 2,1 punti rispetto alla rilevazione precedente. Il brand domina le conversazioni nel segmento pneumatici ad alte prestazioni, grazie alla forte associazione con la Formula 1 e al posizionamento premium del P Zero. Le keyword \"pneumatici invernali\" e \"gomme auto sportive\" vedono Pirelli stabilmente nelle prime tre posizioni organiche, con snippet arricchiti che ne aumentano la visibilità.\n\nSul fronte competitivo, Michelin ha intensificato la propria presenza editoriale con contenuti focalizzati sulla sostenibilità e sulla durata chilometrica, guadagnando terreno nelle query informative. Continental mantiene una forte presenza nelle AI Overview di Google per le query tecniche, un'area in cui Pirelli risulta attualmente sottorappresentata. Bridgestone e Goodyear mostrano attività stabile senza variazioni significative.\n\nSi raccomanda di investire in contenuti tecnici ottimizzati per le AI Overview, con particolare focus su pneumatici 4 stagioni e pneumatici per veicoli elettrici, due segmenti in rapida crescita dove il brand ha margini di miglioramento. Il topic \"pneumatici sostenibili\" rappresenta un'opportunità emergente da presidiare con una strategia editoriale dedicata, considerando il crescente interesse dei consumatori e l'allineamento con i valori di brand di Pirelli.",
            StartedAt = now.AddHours(-2),
            CompletedAt = now.AddHours(-1),
            CreatedAt = now.AddHours(-2)
        };
        db.Scans.Add(scan);

        // ── SERP Results + AI Analysis ──────────────────────────────
        var serpResults = CreateSerpResults(scanId, now);
        db.SerpResults.AddRange(serpResults);

        var aiAnalyses = CreateAiAnalyses(serpResults, now);
        db.AiAnalyses.AddRange(aiAnalyses);

        // ── AI Visibility ───────────────────────────────────────────
        var aiVisibilities = CreateAiVisibility(scanId, now);
        db.AiVisibilities.AddRange(aiVisibilities);

        // ── Metrics ─────────────────────────────────────────────────
        var metrics = CreateMetrics(projectId, scanId, now);
        db.Metrics.AddRange(metrics);

        // ── Insights ────────────────────────────────────────────────
        var insights = CreateInsights(projectId, scanId, now);
        db.Insights.AddRange(insights);

        // ── Tags + TagScans ─────────────────────────────────────────
        var tags = CreateTags(projectId, scanId, now);
        db.Tags.AddRange(tags.Select(t => t.Tag));
        db.TagScans.AddRange(tags.Select(t => t.TagScan));

        await db.SaveChangesAsync();
    }

    // ────────────────────────────────────────────────────────────────
    // SERP Results
    // ────────────────────────────────────────────────────────────────
    private static List<SerpResult> CreateSerpResults(string scanId, DateTime now)
    {
        return new List<SerpResult>
        {
            // ── pneumatici invernali (google_organic) ───────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_organic", Position = 1,
                Url = "https://www.pirelli.com/it/it/auto/pneumatici-invernali",
                Title = "Pneumatici Invernali Pirelli | Sicurezza su neve e ghiaccio",
                Snippet = "Scopri la gamma completa di pneumatici invernali Pirelli: tecnologia avanzata per garantire aderenza, sicurezza e prestazioni ottimali su neve, ghiaccio e fondi bagnati.",
                Domain = "pirelli.com", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_organic", Position = 2,
                Url = "https://www.michelin.it/pneumatici/pneumatici-invernali",
                Title = "Pneumatici Invernali Michelin | Affidabilità in ogni condizione",
                Snippet = "I pneumatici invernali Michelin offrono prestazioni eccezionali su neve e ghiaccio, con una durata chilometrica superiore alla media del segmento.",
                Domain = "michelin.it", IsCompetitor = true, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_organic", Position = 3,
                Url = "https://www.automobile.it/magazine/pneumatici/migliori-pneumatici-invernali-2025",
                Title = "I 10 migliori pneumatici invernali 2025-2026: la classifica completa",
                Snippet = "La nostra classifica dei migliori pneumatici invernali testati su strada: confronto prestazioni, prezzo e sicurezza. Pirelli Sottozero 3 e Michelin Alpin 6 ai primi posti.",
                Domain = "automobile.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_organic", Position = 4,
                Url = "https://www.continental-pneumatici.it/auto/pneumatici/pneumatici-invernali",
                Title = "Pneumatici invernali Continental | WinterContact TS 870",
                Snippet = "I pneumatici invernali Continental garantiscono sicurezza e controllo ottimali. Scopri la nuova gamma WinterContact con tecnologia brevettata.",
                Domain = "continental-pneumatici.it", IsCompetitor = true, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_organic", Position = 6,
                Url = "https://www.sicurauto.it/guida-acquisto/pneumatici-invernali-come-scegliere/",
                Title = "Pneumatici invernali: guida alla scelta 2025 | SicurAuto.it",
                Snippet = "Come scegliere i pneumatici invernali giusti: guida completa con confronto tra i principali brand, normative vigenti e consigli degli esperti.",
                Domain = "sicurauto.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },

            // ── gomme auto sportive (google_organic) ────────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "gomme auto sportive", Source = "google_organic", Position = 1,
                Url = "https://www.pirelli.com/it/it/auto/pneumatici-sportivi",
                Title = "Pneumatici Sportivi Pirelli | P Zero e gamma performance",
                Snippet = "Pirelli P Zero: il pneumatico scelto dalle migliori supercar al mondo. Prestazioni estreme, grip laterale superiore e tecnologia derivata dalla Formula 1.",
                Domain = "pirelli.com", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "gomme auto sportive", Source = "google_organic", Position = 3,
                Url = "https://www.alvolante.it/test/confronto-gomme-sportive-2025",
                Title = "Test gomme sportive 2025: il confronto definitivo | AlVolante",
                Snippet = "Abbiamo testato 8 pneumatici sportivi su pista e su strada. Pirelli P Zero e Michelin Pilot Sport 5 si contendono il primo posto con prestazioni eccezionali.",
                Domain = "alvolante.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "gomme auto sportive", Source = "google_organic", Position = 5,
                Url = "https://www.bridgestone.it/pneumatici/auto/potenza",
                Title = "Bridgestone Potenza | Pneumatici sportivi ad alte prestazioni",
                Snippet = "Bridgestone Potenza: pneumatici sportivi per chi cerca il massimo delle prestazioni. Tecnologia innovativa per una guida sportiva senza compromessi.",
                Domain = "bridgestone.it", IsCompetitor = true, FetchedAt = now.AddHours(-2)
            },

            // ── pirelli p zero (google_organic) ─────────────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", Source = "google_organic", Position = 1,
                Url = "https://www.pirelli.com/it/it/auto/catalogo/p-zero",
                Title = "P ZERO™ | Il pneumatico ultra high performance di Pirelli",
                Snippet = "P ZERO™ di Pirelli: il riferimento mondiale per i pneumatici ultra high performance. Scelto come primo equipaggiamento dalle più prestigiose case automobilistiche.",
                Domain = "pirelli.com", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", Source = "google_organic", Position = 2,
                Url = "https://www.automobile.it/magazine/pneumatici/pirelli-p-zero-recensione-2025",
                Title = "Pirelli P Zero 2025: test, opinioni e prezzi | Automobile.it",
                Snippet = "Recensione completa del Pirelli P Zero: test su strada e su bagnato, confronto con i concorrenti e listino prezzi aggiornato per tutte le misure disponibili.",
                Domain = "automobile.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },

            // ── pneumatici invernali (google_news) ──────────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_news", Position = 1,
                Url = "https://www.sicurauto.it/news/obbligo-pneumatici-invernali-2025-date-sanzioni/",
                Title = "Pneumatici invernali 2025: date obbligo, sanzioni e novità normative",
                Snippet = "Dal 15 novembre scatta l'obbligo di pneumatici invernali o catene a bordo su strade e autostrade. Ecco tutto quello che c'è da sapere su date, sanzioni e deroghe.",
                Domain = "sicurauto.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", Source = "google_news", Position = 2,
                Url = "https://www.alvolante.it/news/pirelli-sottozero-nuova-generazione-2025",
                Title = "Pirelli presenta la nuova generazione Sottozero: più grip e sostenibilità",
                Snippet = "Pirelli svela il nuovo Sottozero con mescola a base di materiali sostenibili e prestazioni migliorate del 12% su neve. Disponibile da settembre per oltre 200 misure.",
                Domain = "alvolante.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "gomme auto sportive", Source = "google_news", Position = 1,
                Url = "https://www.motorsport.com/f1/news/pirelli-fornitore-f1-2025-nuove-mescole/",
                Title = "F1 2025: Pirelli introduce nuove mescole per maggiore spettacolo in pista",
                Snippet = "Pirelli, fornitore unico della Formula 1, ha sviluppato mescole più performanti per la stagione 2025 con l'obiettivo di favorire strategie diversificate e sorpassi.",
                Domain = "motorsport.com", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },

            // ── pirelli p zero (google_news) ────────────────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", Source = "google_news", Position = 1,
                Url = "https://www.alvolante.it/news/pirelli-p-zero-e-auto-elettriche-sportive",
                Title = "Pirelli P Zero E: il pneumatico nato per le sportive elettriche",
                Snippet = "Pirelli lancia il P Zero E, specifico per auto elettriche ad alte prestazioni. Riduzione della resistenza al rotolamento del 15% con materiali sostenibili oltre il 50%.",
                Domain = "alvolante.it", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },

            // ── pneumatici 4 stagioni (google_organic) ──────────────
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici 4 stagioni", Source = "google_organic", Position = 2,
                Url = "https://www.michelin.it/pneumatici/4-stagioni",
                Title = "Pneumatici 4 Stagioni Michelin | CrossClimate 2",
                Snippet = "Michelin CrossClimate 2: il pneumatico all season che non scende a compromessi. Certificato per l'inverno, performante in estate. La scelta intelligente tutto l'anno.",
                Domain = "michelin.it", IsCompetitor = true, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici 4 stagioni", Source = "google_organic", Position = 3,
                Url = "https://www.pirelli.com/it/it/auto/pneumatici-4-stagioni",
                Title = "Pneumatici 4 Stagioni Pirelli | Cinturato All Season SF3",
                Snippet = "Pirelli Cinturato All Season SF3: sicurezza e versatilità in ogni condizione meteo. Tecnologia Seal Inside per la protezione dalle forature.",
                Domain = "pirelli.com", IsCompetitor = false, FetchedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici 4 stagioni", Source = "google_organic", Position = 5,
                Url = "https://www.goodyear.it/auto/pneumatici-4-stagioni",
                Title = "Goodyear Vector 4Seasons Gen-3 | Pneumatici All Season",
                Snippet = "Il Goodyear Vector 4Seasons Gen-3 offre eccellente aderenza tutto l'anno con la tecnologia Weather Reactive. Massima versatilità per ogni condizione climatica.",
                Domain = "goodyear.it", IsCompetitor = true, FetchedAt = now.AddHours(-2)
            },
        };
    }

    // ────────────────────────────────────────────────────────────────
    // AI Analysis (one per SERP result)
    // ────────────────────────────────────────────────────────────────
    private static List<AiAnalysis> CreateAiAnalyses(List<SerpResult> serpResults, DateTime now)
    {
        var sentiments = new[] { "positive", "neutral", "mixed", "positive", "neutral", "positive", "neutral", "mixed", "positive", "positive", "neutral", "neutral", "positive", "positive", "mixed", "positive", "neutral", "positive" };
        var sentimentScores = new[] { 0.85f, 0.50f, 0.45f, 0.78f, 0.52f, 0.92f, 0.55f, 0.40f, 0.88f, 0.95f, 0.48f, 0.50f, 0.82f, 0.90f, 0.38f, 0.87f, 0.46f, 0.60f };

        var themesOptions = new[]
        {
            "[{\"name\":\"sicurezza stradale\",\"confidence\":0.92},{\"name\":\"prestazioni invernali\",\"confidence\":0.88}]",
            "[{\"name\":\"durata pneumatici\",\"confidence\":0.85},{\"name\":\"rapporto qualità-prezzo\",\"confidence\":0.78}]",
            "[{\"name\":\"confronto brand\",\"confidence\":0.90},{\"name\":\"test indipendenti\",\"confidence\":0.86}]",
            "[{\"name\":\"tecnologia pneumatici\",\"confidence\":0.87},{\"name\":\"innovazione\",\"confidence\":0.82}]",
            "[{\"name\":\"guida acquisto\",\"confidence\":0.94},{\"name\":\"normativa pneumatici\",\"confidence\":0.79}]",
            "[{\"name\":\"prestazioni sportive\",\"confidence\":0.95},{\"name\":\"Formula 1\",\"confidence\":0.91}]",
            "[{\"name\":\"test su pista\",\"confidence\":0.89},{\"name\":\"confronto prestazionale\",\"confidence\":0.85}]",
            "[{\"name\":\"pneumatici sportivi\",\"confidence\":0.88},{\"name\":\"guida sportiva\",\"confidence\":0.83}]",
            "[{\"name\":\"ultra high performance\",\"confidence\":0.96},{\"name\":\"primo equipaggiamento\",\"confidence\":0.90}]",
            "[{\"name\":\"recensione prodotto\",\"confidence\":0.91},{\"name\":\"valutazione esperta\",\"confidence\":0.87}]",
            "[{\"name\":\"normativa stradale\",\"confidence\":0.93},{\"name\":\"obbligo stagionale\",\"confidence\":0.89}]",
            "[{\"name\":\"innovazione sostenibile\",\"confidence\":0.88},{\"name\":\"nuovi prodotti\",\"confidence\":0.84}]",
            "[{\"name\":\"Formula 1\",\"confidence\":0.94},{\"name\":\"tecnologia racing\",\"confidence\":0.90}]",
            "[{\"name\":\"auto elettriche\",\"confidence\":0.92},{\"name\":\"sostenibilità\",\"confidence\":0.89}]",
            "[{\"name\":\"all season\",\"confidence\":0.91},{\"name\":\"versatilità\",\"confidence\":0.86}]",
            "[{\"name\":\"all season\",\"confidence\":0.90},{\"name\":\"certificazione invernale\",\"confidence\":0.84}]",
            "[{\"name\":\"all season\",\"confidence\":0.87},{\"name\":\"aderenza tutto l'anno\",\"confidence\":0.82}]",
            "[{\"name\":\"tecnologia antiforatura\",\"confidence\":0.88},{\"name\":\"sicurezza\",\"confidence\":0.85}]"
        };

        var entitiesOptions = new[]
        {
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Sottozero\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Michelin\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Alpin\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Michelin\",\"type\":\"brand\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Continental\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"WinterContact\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"neutral\"},{\"name\":\"Continental\",\"type\":\"brand\",\"sentiment\":\"neutral\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"P Zero\",\"type\":\"product\",\"sentiment\":\"positive\"},{\"name\":\"Formula 1\",\"type\":\"event\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Michelin\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Pilot Sport\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Bridgestone\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Potenza\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"P Zero\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"P Zero\",\"type\":\"product\",\"sentiment\":\"neutral\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"neutral\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Sottozero\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Formula 1\",\"type\":\"event\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"P Zero E\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Michelin\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"CrossClimate\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Cinturato\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Goodyear\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Vector 4Seasons\",\"type\":\"product\",\"sentiment\":\"positive\"}]",
            "[{\"name\":\"Pirelli\",\"type\":\"brand\",\"sentiment\":\"positive\"},{\"name\":\"Cinturato\",\"type\":\"product\",\"sentiment\":\"positive\"}]"
        };

        var summaries = new[]
        {
            "Pagina ufficiale Pirelli dedicata ai pneumatici invernali, con enfasi su sicurezza e aderenza su neve e ghiaccio. Presentazione della gamma completa con tecnologie proprietarie.",
            "Pagina prodotto Michelin per pneumatici invernali, focalizzata sulla durata chilometrica superiore come principale vantaggio competitivo rispetto ai concorrenti.",
            "Articolo editoriale indipendente con classifica dei migliori pneumatici invernali. Pirelli Sottozero 3 e Michelin Alpin 6 in testa alla graduatoria.",
            "Landing page Continental per la gamma WinterContact TS 870, con focus su tecnologia brevettata e sicurezza. Posizionamento su innovazione tecnica.",
            "Guida alla scelta dei pneumatici invernali con confronto tra brand, normative e consigli esperti. Contenuto informativo neutrale senza preferenze di brand.",
            "Pagina flagship Pirelli per la linea sportiva P Zero, con forte richiamo alla Formula 1 e al posizionamento ultra high performance. Tono premium.",
            "Test comparativo su pista e strada di 8 pneumatici sportivi. Pirelli P Zero e Michelin Pilot Sport 5 ai vertici della classifica con valutazioni molto ravvicinate.",
            "Pagina prodotto Bridgestone Potenza per il segmento sportivo. Comunicazione focalizzata su innovazione e prestazioni senza compromessi.",
            "Scheda prodotto ufficiale P Zero di Pirelli con dettagli tecnici e lista delle case automobilistiche che lo adottano come primo equipaggiamento.",
            "Recensione giornalistica approfondita del Pirelli P Zero con test su strada e bagnato, confronto con concorrenti diretti e analisi del rapporto prezzo-prestazioni.",
            "Articolo informativo sulle date dell'obbligo di pneumatici invernali, sanzioni previste e novità normative per la stagione 2025. Contenuto di servizio.",
            "Notizia sul lancio della nuova generazione Pirelli Sottozero con materiali sostenibili e miglioramento prestazionale del 12% su neve. Tono positivo.",
            "Notizia sulle nuove mescole Pirelli per la stagione di Formula 1 2025, con focus sullo spettacolo in pista e strategie diversificate.",
            "Lancio del Pirelli P Zero E per auto elettriche sportive con riduzione resistenza al rotolamento del 15% e oltre 50% di materiali sostenibili.",
            "Pagina prodotto Michelin CrossClimate 2 per il segmento all season, con enfasi sulla certificazione invernale e le prestazioni estive. Forte posizionamento competitivo.",
            "Pagina ufficiale Pirelli per la gamma 4 stagioni Cinturato All Season SF3 con tecnologia Seal Inside antiforatura. Posizionamento su sicurezza e versatilità.",
            "Landing page Goodyear Vector 4Seasons Gen-3 con tecnologia Weather Reactive. Comunicazione centrata sulla versatilità in ogni condizione climatica.",
            "Contenuto editoriale Pirelli sul Cinturato con approfondimento sulla tecnologia antiforatura e la sicurezza stradale."
        };

        var analyses = new List<AiAnalysis>();
        for (int i = 0; i < serpResults.Count; i++)
        {
            analyses.Add(new AiAnalysis
            {
                Id = Guid.NewGuid().ToString(),
                SerpResultId = serpResults[i].Id,
                Themes = themesOptions[i],
                Sentiment = sentiments[i],
                SentimentScore = sentimentScores[i],
                Entities = entitiesOptions[i],
                Summary = summaries[i],
                LanguageDetected = "it",
                IsPriority = i < 3 || i == 5 || i == 8,
                AnalyzedAt = now.AddHours(-1).AddMinutes(i)
            });
        }
        return analyses;
    }

    // ────────────────────────────────────────────────────────────────
    // AI Visibility
    // ────────────────────────────────────────────────────────────────
    private static List<AiVisibility> CreateAiVisibility(string scanId, DateTime now)
    {
        return new List<AiVisibility>
        {
            // pneumatici invernali
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", LlmModel = "chatgpt",
                ResponseText = "I migliori pneumatici invernali per il 2025-2026 includono diversi modelli di alta qualità. Tra i più consigliati ci sono il Michelin Alpin 6, noto per la sua durata e aderenza su neve, il Pirelli Sottozero 3 che offre eccellenti prestazioni su bagnato e neve, e il Continental WinterContact TS 870 con ottimo comfort di marcia. Per chi cerca un buon rapporto qualità-prezzo, anche il Goodyear UltraGrip Performance+ è un'opzione valida.",
                BrandMentioned = true, BrandSentiment = 0.75f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":1},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":3},{\"brand\":\"Goodyear\",\"mentioned\":true,\"position\":4},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 72.5f,
                TopicsCovered = "[\"prestazioni su neve\",\"aderenza\",\"durata\",\"comfort\",\"rapporto qualità-prezzo\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", LlmModel = "gemini",
                ResponseText = "Per scegliere i pneumatici invernali migliori bisogna considerare diversi fattori come la misura, lo stile di guida e le condizioni stradali tipiche. I brand premium come Continental, Michelin e Pirelli offrono le prestazioni più elevate ma a prezzi superiori. Il Continental WinterContact TS 870 è particolarmente apprezzato nei test ADAC. Un'alternativa valida nel segmento medio è rappresentata da Hankook con il Winter i*cept RS3.",
                BrandMentioned = true, BrandSentiment = 0.60f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":2},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":1},{\"brand\":\"Hankook\",\"mentioned\":true,\"position\":4},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false}]",
                VisibilityScore = 45.0f,
                TopicsCovered = "[\"fattori di scelta\",\"brand premium\",\"test ADAC\",\"segmento medio\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", LlmModel = "perplexity",
                ResponseText = "Secondo i test più recenti del 2025, i migliori pneumatici invernali sono: 1) Pirelli Sottozero 3 - eccellente grip su neve e bagnato, 2) Michelin Alpin 6 - migliore durata chilometrica, 3) Continental WinterContact TS 870 - miglior comfort. Il Pirelli si distingue per le prestazioni derivate dall'esperienza in Formula 1 e per la resa su fondi bagnati. Fonti: ADAC, TCS, Automobile.it.",
                BrandMentioned = true, BrandSentiment = 0.88f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":2},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":3},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 85.0f,
                TopicsCovered = "[\"classifica test\",\"grip neve\",\"durata\",\"Formula 1\",\"fondi bagnati\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", LlmModel = "grok",
                ResponseText = "I pneumatici invernali sono essenziali per la guida sicura nei mesi freddi. Tra le migliori opzioni troviamo il Michelin Alpin 6, il Continental WinterContact TS 870 e il Bridgestone Blizzak LM005. Questi modelli eccellono nelle condizioni di neve e ghiaccio. È importante verificare l'indice di velocità e il codice M+S o il simbolo del fiocco di neve per la conformità normativa.",
                BrandMentioned = false, BrandSentiment = null,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":1},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":2},{\"brand\":\"Bridgestone\",\"mentioned\":true,\"position\":3},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 0.0f,
                TopicsCovered = "[\"sicurezza\",\"neve e ghiaccio\",\"normativa\",\"indice velocità\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pneumatici invernali", LlmModel = "claude",
                ResponseText = "Per i pneumatici invernali, la scelta dipende molto dall'uso specifico. Nel segmento premium, Pirelli con il Sottozero 3 e Michelin con l'Alpin 6 rappresentano i riferimenti del mercato. Pirelli offre un vantaggio nelle prestazioni su bagnato grazie alla tecnologia sviluppata per la Formula 1, mentre Michelin eccelle nella durata. Nel segmento medio, Continental WinterContact TS 870 offre un ottimo equilibrio prestazioni-prezzo.",
                BrandMentioned = true, BrandSentiment = 0.80f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":2},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":3},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 78.0f,
                TopicsCovered = "[\"segmento premium\",\"prestazioni bagnato\",\"Formula 1\",\"durata\",\"rapporto prezzo-prestazioni\"]",
                QueriedAt = now.AddHours(-2)
            },

            // pirelli p zero
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", LlmModel = "chatgpt",
                ResponseText = "Il Pirelli P Zero è uno dei pneumatici ultra high performance più rinomati al mondo. È il pneumatico scelto come primo equipaggiamento da marchi come Ferrari, Lamborghini, BMW M e Porsche. Offre grip eccezionale sull'asciutto, buone prestazioni sul bagnato e un feeling di guida sportivo. Esistono diverse varianti: P Zero standard, P Zero Corsa per uso pista, e P Zero E per auto elettriche. I principali concorrenti sono il Michelin Pilot Sport 5 e il Continental SportContact 7.",
                BrandMentioned = true, BrandSentiment = 0.92f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":2},{\"brand\":\"Continental\",\"mentioned\":true,\"position\":3},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 82.0f,
                TopicsCovered = "[\"ultra high performance\",\"primo equipaggiamento\",\"supercar\",\"varianti prodotto\",\"concorrenti\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", LlmModel = "gemini",
                ResponseText = "Il Pirelli P Zero è un pneumatico ad altissime prestazioni progettato per auto sportive e supercar. Le sue caratteristiche principali includono elevato grip laterale, stabilità ad alta velocità e risposta di sterzo precisa. È disponibile in un'ampia gamma di misure, dalle 17 alle 22 pollici. Il prezzo è nella fascia alta del mercato ma giustificato dalle prestazioni. Nota: per l'uso quotidiano su strade normali potrebbe risultare rumoroso rispetto ad alternative touring.",
                BrandMentioned = true, BrandSentiment = 0.70f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":false},{\"brand\":\"Continental\",\"mentioned\":false},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 65.0f,
                TopicsCovered = "[\"prestazioni\",\"grip laterale\",\"gamma misure\",\"prezzo\",\"rumorosità\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", LlmModel = "perplexity",
                ResponseText = "Il Pirelli P Zero è il pneumatico UHP di riferimento nel mercato, scelto come equipaggiamento originale da oltre 50 case automobilistiche premium e sportive tra cui Ferrari, Lamborghini, McLaren, Porsche, BMW M e Mercedes-AMG. Nelle prove su asciutto ottiene valutazioni eccellenti con un grip laterale ai vertici della categoria. Sul bagnato le prestazioni sono molto buone ma leggermente inferiori al Michelin Pilot Sport 5 secondo i test di Auto Bild Sportscars 2025. Prezzo medio: 180-350€ per pneumatico a seconda della misura. Fonti: Pirelli.com, Auto Bild, ADAC.",
                BrandMentioned = true, BrandSentiment = 0.85f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":true,\"position\":2},{\"brand\":\"Continental\",\"mentioned\":false},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 80.0f,
                TopicsCovered = "[\"equipaggiamento originale\",\"case automobilistiche\",\"test comparativi\",\"prezzi\",\"prestazioni bagnato\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", LlmModel = "grok",
                ResponseText = "Il Pirelli P Zero è un pneumatico sportivo leggendario, utilizzato in Formula 1 e scelto dalle supercar più esclusive. Offre prestazioni di altissimo livello sull'asciutto con un grip eccellente. La versione P Zero Corsa è pensata per l'uso in pista. Pirelli ha anche lanciato il P Zero E per veicoli elettrici sportivi, con materiali sostenibili. Un prodotto iconico del made in Italy nel motorsport.",
                BrandMentioned = true, BrandSentiment = 0.90f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":false},{\"brand\":\"Continental\",\"mentioned\":false},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 75.0f,
                TopicsCovered = "[\"Formula 1\",\"supercar\",\"P Zero Corsa\",\"veicoli elettrici\",\"made in Italy\"]",
                QueriedAt = now.AddHours(-2)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ScanId = scanId,
                Keyword = "pirelli p zero", LlmModel = "claude",
                ResponseText = "Il Pirelli P Zero è un pneumatico ultra high performance (UHP) che rappresenta il vertice della gamma stradale Pirelli. Punti di forza: grip eccezionale sull'asciutto, stabilità ad altissima velocità, feedback di guida diretto e preciso. È il pneumatico più scelto come primo equipaggiamento dalle case automobilistiche sportive, tra cui Ferrari, Porsche e BMW M. Limitazioni: comfort non ai livelli dei pneumatici touring, durata inferiore rispetto a pneumatici meno performanti, e costo superiore alla media. Per l'uso quotidiano su auto non sportive, il Pirelli Cinturato P7 potrebbe essere una scelta più equilibrata.",
                BrandMentioned = true, BrandSentiment = 0.82f,
                CompetitorMentions = "[{\"brand\":\"Michelin\",\"mentioned\":false},{\"brand\":\"Continental\",\"mentioned\":false},{\"brand\":\"Bridgestone\",\"mentioned\":false},{\"brand\":\"Goodyear\",\"mentioned\":false},{\"brand\":\"Hankook\",\"mentioned\":false}]",
                VisibilityScore = 70.0f,
                TopicsCovered = "[\"UHP\",\"primo equipaggiamento\",\"grip asciutto\",\"limitazioni\",\"alternativa Cinturato\"]",
                QueriedAt = now.AddHours(-2)
            }
        };
    }

    // ────────────────────────────────────────────────────────────────
    // Metrics
    // ────────────────────────────────────────────────────────────────
    private static List<Metric> CreateMetrics(string projectId, string scanId, DateTime now)
    {
        return new List<Metric>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                MetricType = "nsov", Value = 34.2f,
                Breakdown = "{\"byKeyword\":{\"pneumatici invernali\":38.5,\"gomme auto sportive\":52.0,\"pneumatici 4 stagioni\":22.0,\"pirelli p zero\":78.0,\"pirelli cinturato\":65.0,\"pneumatici moto\":28.0,\"gomme estive\":30.5,\"pneumatici SUV\":18.0,\"cambio gomme\":12.0,\"pirelli scorpion\":72.0},\"bySource\":{\"google_organic\":36.8,\"google_news\":42.1,\"ai_overview\":23.7},\"previousValue\":32.1,\"change\":2.1,\"trend\":\"up\"}",
                CalculatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                MetricType = "tos", Value = 72.5f,
                Breakdown = "{\"totalTopics\":28,\"dominantTopics\":12,\"presentTopics\":20,\"absentTopics\":8,\"topDominated\":[\"pneumatici sportivi\",\"Formula 1\",\"P Zero\",\"primo equipaggiamento\"],\"topMissing\":[\"pneumatici EV\",\"sostenibilità\",\"pneumatici budget\",\"rumorosità\"],\"previousValue\":70.8,\"change\":1.7,\"trend\":\"up\"}",
                CalculatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                MetricType = "nri", Value = 28.8f,
                Breakdown = "{\"totalRisks\":5,\"highRisks\":1,\"mediumRisks\":2,\"lowRisks\":2,\"topRisks\":[{\"title\":\"Crescita Michelin nel segmento premium\",\"severity\":\"high\",\"score\":74},{\"title\":\"Assenza in AI Overview per query tecniche\",\"severity\":\"medium\",\"score\":69},{\"title\":\"Continental domina le risposte AI per pneumatici invernali\",\"severity\":\"medium\",\"score\":62}],\"previousValue\":31.5,\"change\":-2.7,\"trend\":\"down\"}",
                CalculatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                MetricType = "tai", Value = 156.3f,
                Breakdown = "{\"totalKeywords\":10,\"avgPosition\":3.2,\"top3Count\":6,\"top10Count\":9,\"byKeyword\":{\"pneumatici invernali\":{\"position\":1,\"visibility\":95},\"gomme auto sportive\":{\"position\":1,\"visibility\":92},\"pirelli p zero\":{\"position\":1,\"visibility\":98},\"pneumatici 4 stagioni\":{\"position\":3,\"visibility\":68},\"pirelli cinturato\":{\"position\":2,\"visibility\":85},\"pneumatici moto\":{\"position\":4,\"visibility\":55},\"gomme estive\":{\"position\":3,\"visibility\":70},\"pneumatici SUV\":{\"position\":6,\"visibility\":42},\"cambio gomme\":{\"position\":8,\"visibility\":28},\"pirelli scorpion\":{\"position\":1,\"visibility\":96}},\"previousValue\":148.7,\"change\":7.6,\"trend\":\"up\"}",
                CalculatedAt = now.AddHours(-1)
            }
        };
    }

    // ────────────────────────────────────────────────────────────────
    // Insights
    // ────────────────────────────────────────────────────────────────
    private static List<Insight> CreateInsights(string projectId, string scanId, DateTime now)
    {
        return new List<Insight>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                Type = "opportunity",
                Title = "Gap contenutistico su pneumatici 4 stagioni",
                Description = "L'analisi delle SERP per la keyword \"pneumatici 4 stagioni\" evidenzia che Pirelli è posizionata solo in terza posizione, dietro a Michelin CrossClimate 2 e Continental AllSeasonContact. Il segmento all-season è in crescita del 23% YoY in Italia. Creare contenuti approfonditi e ottimizzati su questa categoria può portare a un significativo guadagno di visibilità organica e rafforzare la percezione del Cinturato All Season SF3.",
                Score = 87f,
                DataEvidence = "{\"keyword\":\"pneumatici 4 stagioni\",\"currentPosition\":3,\"competitorPositions\":{\"michelin.it\":1,\"continental-pneumatici.it\":2},\"searchVolume\":22100,\"trend\":\"+23% YoY\"}",
                Status = "new",
                CreatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                Type = "risk",
                Title = "Crescita menzioni Michelin nel segmento premium",
                Description = "Michelin ha incrementato del 18% la propria presenza nelle risposte AI e nei contenuti editoriali premium nell'ultimo mese. In particolare, il Michelin Pilot Sport 5 viene citato come alternativa diretta al P Zero in 4 risposte AI su 5. La narrativa di Michelin si sta concentrando su durata e sostenibilità, due attributi su cui Pirelli è attualmente meno visibile.",
                Score = 74f,
                DataEvidence = "{\"competitorBrand\":\"Michelin\",\"aiMentionRate\":0.80,\"previousRate\":0.62,\"change\":\"+18%\",\"focusTopics\":[\"durata\",\"sostenibilità\"],\"affectedProducts\":[\"Pilot Sport 5\",\"Alpin 6\"]}",
                Status = "new",
                CreatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                Type = "trend",
                Title = "Accelerazione ricerche pneumatici EV",
                Description = "Le ricerche relative a \"pneumatici auto elettriche\" e \"gomme EV\" hanno registrato un incremento del 145% negli ultimi 6 mesi in Italia. Pirelli ha il P Zero E come prodotto dedicato ma la visibilità organica su queste keyword è ancora limitata. I concorrenti Michelin e Continental stanno già creando landing page e contenuti dedicati al segmento EV.",
                Score = 82f,
                DataEvidence = "{\"keywords\":[\"pneumatici auto elettriche\",\"gomme EV\",\"pneumatici veicoli elettrici\"],\"searchGrowth\":\"+145%\",\"pirelliProduct\":\"P Zero E\",\"currentVisibility\":\"bassa\",\"competitorActivity\":{\"Michelin\":\"landing page dedicata\",\"Continental\":\"guida all'acquisto EV\"}}",
                Status = "new",
                CreatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                Type = "gap",
                Title = "Assenza in AI Overview per query tecniche",
                Description = "Continental e Michelin appaiono regolarmente nelle AI Overview di Google per query tecniche come \"differenza pneumatici H e V\", \"come leggere misura pneumatico\" e \"quando cambiare pneumatici\". Pirelli è assente da questi snippet AI nonostante abbia contenuti tecnici sul sito. È necessario ottimizzare i contenuti informativi per il formato AI Overview con struttura FAQ e dati strutturati.",
                Score = 69f,
                DataEvidence = "{\"aiOverviewQueries\":[\"differenza pneumatici H e V\",\"come leggere misura pneumatico\",\"quando cambiare pneumatici\"],\"pirelliPresence\":0,\"continentalPresence\":3,\"michelinPresence\":2,\"suggestedFormat\":\"FAQ con dati strutturati\"}",
                Status = "new",
                CreatedAt = now.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid().ToString(), ProjectId = projectId, ScanId = scanId,
                Type = "opportunity",
                Title = "Topic emergente: pneumatici sostenibili",
                Description = "Il tema della sostenibilità nei pneumatici sta emergendo con forza sia nelle ricerche organiche (+67% YoY) sia nelle conversazioni AI. Pirelli ha un forte posizionamento reale con materiali riciclati e la certificazione FSC, ma questa narrativa non è sufficientemente presente nei contenuti online. Creare una strategia editoriale dedicata alla sostenibilità può differenziare Pirelli dai competitor e intercettare un pubblico sempre più sensibile a queste tematiche.",
                Score = 91f,
                DataEvidence = "{\"topic\":\"pneumatici sostenibili\",\"searchGrowth\":\"+67% YoY\",\"pirelliAssets\":[\"materiali riciclati\",\"certificazione FSC\",\"P Zero E\"],\"contentGap\":\"alto\",\"competitorContent\":{\"Michelin\":\"strategia sostenibilità ben comunicata\",\"Continental\":\"report ESG linkato nei contenuti prodotto\"},\"potentialImpact\":\"alto\"}",
                Status = "new",
                CreatedAt = now.AddHours(-1)
            }
        };
    }

    // ────────────────────────────────────────────────────────────────
    // Tags + TagScans
    // ────────────────────────────────────────────────────────────────
    private record TagWithScan(Tag Tag, TagScan TagScan);

    private static List<TagWithScan> CreateTags(string projectId, string scanId, DateTime now)
    {
        var tagsData = new (string name, string slug, int count, float avgSentiment)[]
        {
            ("sicurezza", "sicurezza", 14, 0.72f),
            ("prestazioni", "prestazioni", 18, 0.85f),
            ("sostenibilità", "sostenibilita", 8, 0.68f),
            ("Formula 1", "formula-1", 6, 0.92f),
            ("inverno", "inverno", 12, 0.55f),
            ("pneumatici EV", "pneumatici-ev", 5, 0.78f),
            ("manutenzione", "manutenzione", 7, 0.50f),
            ("confronto brand", "confronto-brand", 11, 0.45f),
            ("primo equipaggiamento", "primo-equipaggiamento", 4, 0.90f),
            ("all season", "all-season", 9, 0.62f)
        };

        return tagsData.Select(t =>
        {
            var tagId = Guid.NewGuid().ToString();
            return new TagWithScan(
                new Tag
                {
                    Id = tagId,
                    ProjectId = projectId,
                    Name = t.name,
                    Slug = t.slug,
                    Count = t.count,
                    LastSeenAt = now.AddHours(-1)
                },
                new TagScan
                {
                    TagId = tagId,
                    ScanId = scanId,
                    Count = t.count,
                    AvgSentiment = t.avgSentiment
                }
            );
        }).ToList();
    }
}
