<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	let sortedInsights = $derived(
		[...data.insights].sort((a, b) => b.score - a.score)
	);

	function typeBadge(type: string): { label: string; classes: string } {
		switch (type) {
			case 'opportunity': return { label: 'Opportunita', classes: 'bg-blue-100 text-blue-800' };
			case 'risk': return { label: 'Rischio', classes: 'bg-red-100 text-red-800' };
			case 'trend': return { label: 'Trend', classes: 'bg-violet-100 text-violet-800' };
			case 'gap': return { label: 'Gap', classes: 'bg-amber-100 text-amber-800' };
			default: return { label: type, classes: 'bg-gray-100 text-gray-600' };
		}
	}

	function statusBadge(status: string): { label: string; classes: string } {
		switch (status) {
			case 'active': return { label: 'Attivo', classes: 'bg-emerald-100 text-emerald-700' };
			case 'resolved': return { label: 'Risolto', classes: 'bg-gray-100 text-gray-500' };
			case 'pending': return { label: 'In attesa', classes: 'bg-amber-100 text-amber-700' };
			default: return { label: status, classes: 'bg-gray-100 text-gray-600' };
		}
	}

	function scoreBarColor(score: number): string {
		if (score >= 8) return 'bg-emerald-500';
		if (score >= 5) return 'bg-amber-500';
		return 'bg-red-500';
	}

	function typeLeftBorderColor(type: string): string {
		switch (type) {
			case 'opportunity': return 'border-l-blue-500';
			case 'risk': return 'border-l-red-500';
			case 'trend': return 'border-l-violet-500';
			case 'gap': return 'border-l-amber-500';
			default: return 'border-l-gray-500';
		}
	}

	// Quadrant bubble data
	interface QuadrantBubble {
		name: string;
		x: number; // 0-100 (relevance)
		y: number; // 0-100 (urgency)
		size: number; // px radius
		type: 'opportunity' | 'risk' | 'trend' | 'gap';
		detail: string;
	}

	const bubbles: QuadrantBubble[] = [
		// AGIRE ORA (top-right)
		{ name: 'Pneumatici sostenibili', x: 78, y: 82, size: 28, type: 'opportunity', detail: 'NsoV gap del 34% vs Michelin su sostenibilita. Trend in forte crescita.' },
		{ name: 'Assenza AI Overview', x: 68, y: 72, size: 20, type: 'gap', detail: 'Pirelli assente nel 60% delle AI Overview su keyword core.' },
		// MONITORARE (top-left)
		{ name: 'Crescita Michelin premium', x: 28, y: 78, size: 26, type: 'risk', detail: 'Michelin aumenta share of voice premium del 12% in 3 mesi.' },
		{ name: 'Continental nelle AI', x: 22, y: 65, size: 18, type: 'risk', detail: 'Continental citata 2.5x piu spesso nelle risposte Gemini.' },
		// PIANIFICARE (bottom-right)
		{ name: 'Pneumatici 4 stagioni', x: 75, y: 35, size: 24, type: 'trend', detail: 'Volume ricerche +89% YoY. Nessun contenuto dedicato.' },
		{ name: 'Pneumatici EV', x: 82, y: 28, size: 20, type: 'trend', detail: 'TAI +340%. Mercato in espansione esponenziale.' },
		{ name: 'Content gap SUV', x: 65, y: 22, size: 14, type: 'gap', detail: 'Gap contenutistico su segmento SUV premium.' },
		// BASSA PRIORITA (bottom-left)
		{ name: 'Rumorosita pneumatici', x: 18, y: 18, size: 12, type: 'trend', detail: 'Topic di nicchia, basso impatto commerciale.' },
		{ name: 'Confronto budget', x: 32, y: 12, size: 12, type: 'gap', detail: 'Keyword informazionale, basso valore strategico.' },
	];

	let hoveredBubble = $state<QuadrantBubble | null>(null);

	function bubbleColor(type: string): string {
		switch (type) {
			case 'opportunity': return '#3b82f6';
			case 'risk': return '#ef4444';
			case 'trend': return '#8b5cf6';
			case 'gap': return '#f59e0b';
			default: return '#6b7280';
		}
	}

	function bubbleBg(type: string): string {
		switch (type) {
			case 'opportunity': return 'rgba(59, 130, 246, 0.15)';
			case 'risk': return 'rgba(239, 68, 68, 0.15)';
			case 'trend': return 'rgba(139, 92, 246, 0.15)';
			case 'gap': return 'rgba(245, 158, 11, 0.15)';
			default: return 'rgba(107, 114, 128, 0.15)';
		}
	}

	// Strategic correlations
	interface StrategicSignal {
		title: string;
		type: string;
		typeColor: string;
		evidence: string[];
		action: string;
		impact: 'Alto' | 'Medio' | 'Basso';
		impactColor: string;
	}

	const strategicSignals: StrategicSignal[] = [
		{
			title: 'Michelin domina la narrativa sostenibilita nelle AI',
			type: 'Competitive Intelligence',
			typeColor: 'bg-red-50 text-red-700 border-red-200',
			evidence: [
				'Michelin citata 4x piu di Pirelli nelle risposte AI su sostenibilita',
				'Volume ricerche "pneumatici sostenibili" +67% YoY',
			],
			action: 'Attivare campagna PR su certificazione FSC Pirelli + aggiornare materiali brand per RAG dei modelli AI',
			impact: 'Alto',
			impactColor: 'bg-red-500'
		},
		{
			title: 'Gap tra posizionamento SERP e visibilita AI su pneumatici invernali',
			type: 'Opportunita Cross-Channel',
			typeColor: 'bg-blue-50 text-blue-700 border-blue-200',
			evidence: [
				'Posizione 1 su Google organic, ma citazione solo 40% nelle AI',
				'Continental domina risposte Gemini e Perplexity',
			],
			action: 'Creare contenuti strutturati (FAQ, how-to) ottimizzati per ingestion dei modelli AI + schema markup',
			impact: 'Alto',
			impactColor: 'bg-red-500'
		},
		{
			title: 'Trend pneumatici EV non presidiato da nessun competitor',
			type: 'First Mover Advantage',
			typeColor: 'bg-emerald-50 text-emerald-700 border-emerald-200',
			evidence: [
				'TAI +340%, nessun brand ha contenuti dedicati',
				'Query AI su pneumatici EV in crescita esponenziale',
			],
			action: 'Lanciare hub editoriale dedicato EV + partnership con media automotive elettrici + case study Elect',
			impact: 'Alto',
			impactColor: 'bg-red-500'
		},
		{
			title: 'Sentiment negativo su prezzo premium in crescita',
			type: 'Reputation Risk',
			typeColor: 'bg-amber-50 text-amber-700 border-amber-200',
			evidence: [
				'NRI +8% su associazione "costoso"',
				'Competitor usano narrativa value-for-money',
			],
			action: 'Rafforzare narrativa TCO (Total Cost of Ownership) + testimonianze durata km + garanzia estesa',
			impact: 'Medio',
			impactColor: 'bg-amber-500'
		},
		{
			title: 'Formula 1 asset sottoutilizzato nella narrativa digitale',
			type: 'Brand Asset Optimization',
			typeColor: 'bg-violet-50 text-violet-700 border-violet-200',
			evidence: [
				'F1 citata nel 78% delle risposte AI positive su Pirelli',
				'Ma zero contenuti editoriali collegano F1 a prodotto consumer',
			],
			action: 'Creare bridge content F1 -> consumer: "la tecnologia P Zero dalla pista alla strada"',
			impact: 'Medio',
			impactColor: 'bg-amber-500'
		}
	];

	// Map insight types to strategic signal indices for "correlato a" tags
	function getCorrelation(insight: { type: string; title: string }): string | null {
		const t = insight.title.toLowerCase();
		if (t.includes('sostenibil')) return 'Michelin domina la narrativa sostenibilita nelle AI';
		if (t.includes('invernali') || t.includes('serp')) return 'Gap tra posizionamento SERP e visibilita AI';
		if (t.includes('ev') || t.includes('elettric')) return 'Trend pneumatici EV non presidiato';
		if (t.includes('prezzo') || t.includes('premium') || t.includes('cost')) return 'Sentiment negativo su prezzo premium';
		if (t.includes('f1') || t.includes('formula')) return 'Formula 1 asset sottoutilizzato';
		return null;
	}

	let expandedInsights = $state<Set<number>>(new Set());

	function toggleInsight(idx: number) {
		const next = new Set(expandedInsights);
		if (next.has(idx)) {
			next.delete(idx);
		} else {
			next.add(idx);
		}
		expandedInsights = next;
	}
</script>

<div class="space-y-8 max-w-7xl mx-auto">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Insight & Opportunita</h2>
		<p class="text-sm text-gray-500 mt-1">Mappa strategica, correlazioni e analisi azionabili</p>
	</div>

	<!-- MAPPA STRATEGICA (Zona Calda) -->
	<div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-6">
		<div class="flex items-center gap-3 mb-6">
			<h3 class="text-lg font-semibold text-gray-900">Mappa Strategica</h3>
			<div class="flex items-center gap-3 ml-auto text-[10px]">
				<span class="flex items-center gap-1"><span class="w-2.5 h-2.5 rounded-full bg-blue-500"></span> Opportunita</span>
				<span class="flex items-center gap-1"><span class="w-2.5 h-2.5 rounded-full bg-red-500"></span> Rischio</span>
				<span class="flex items-center gap-1"><span class="w-2.5 h-2.5 rounded-full bg-violet-500"></span> Trend</span>
				<span class="flex items-center gap-1"><span class="w-2.5 h-2.5 rounded-full bg-amber-500"></span> Gap</span>
			</div>
		</div>

		<!-- Quadrant Chart -->
		<div class="relative w-full aspect-[2/1] min-h-[360px]">
			<!-- Quadrant backgrounds -->
			<svg class="absolute inset-0 w-full h-full" viewBox="0 0 100 100" preserveAspectRatio="none">
				<!-- Bottom-Left: BASSA PRIORITA -->
				<rect x="0" y="50" width="50" height="50" fill="rgba(107, 114, 128, 0.04)" />
				<!-- Top-Left: MONITORARE -->
				<rect x="0" y="0" width="50" height="50" fill="rgba(245, 158, 11, 0.06)" />
				<!-- Bottom-Right: PIANIFICARE -->
				<rect x="50" y="50" width="50" height="50" fill="rgba(59, 130, 246, 0.06)" />
				<!-- Top-Right: AGIRE ORA -->
				<rect x="50" y="0" width="50" height="50" fill="rgba(239, 68, 68, 0.06)" />

				<!-- Grid lines -->
				<line x1="50" y1="0" x2="50" y2="100" stroke="rgba(0,0,0,0.06)" stroke-width="0.3" />
				<line x1="0" y1="50" x2="100" y2="50" stroke="rgba(0,0,0,0.06)" stroke-width="0.3" />

				<!-- Subtle grid -->
				<line x1="25" y1="0" x2="25" y2="100" stroke="rgba(0,0,0,0.03)" stroke-width="0.2" stroke-dasharray="1,2" />
				<line x1="75" y1="0" x2="75" y2="100" stroke="rgba(0,0,0,0.03)" stroke-width="0.2" stroke-dasharray="1,2" />
				<line x1="0" y1="25" x2="100" y2="25" stroke="rgba(0,0,0,0.03)" stroke-width="0.2" stroke-dasharray="1,2" />
				<line x1="0" y1="75" x2="100" y2="75" stroke="rgba(0,0,0,0.03)" stroke-width="0.2" stroke-dasharray="1,2" />
			</svg>

			<!-- Quadrant labels -->
			<div class="absolute top-2 right-4 text-[10px] font-bold uppercase tracking-wider text-red-400/60 z-10">AGIRE ORA</div>
			<div class="absolute top-2 left-4 text-[10px] font-bold uppercase tracking-wider text-amber-400/60 z-10">MONITORARE</div>
			<div class="absolute bottom-2 right-4 text-[10px] font-bold uppercase tracking-wider text-blue-400/60 z-10">PIANIFICARE</div>
			<div class="absolute bottom-2 left-4 text-[10px] font-bold uppercase tracking-wider text-gray-400/40 z-10">BASSA PRIORITA</div>

			<!-- Axis labels -->
			<div class="absolute bottom-[-24px] left-1/2 -translate-x-1/2 text-[10px] text-gray-400 font-medium">Rilevanza per il Brand &rarr;</div>
			<div class="absolute top-1/2 left-[-20px] -translate-y-1/2 -rotate-90 text-[10px] text-gray-400 font-medium whitespace-nowrap">Urgenza di Azione &rarr;</div>

			<!-- Bubbles -->
			{#each bubbles as bubble}
				<div
					role="button"
					tabindex="0"
					class="absolute z-20 flex items-center justify-center cursor-pointer transition-transform duration-200 hover:scale-110"
					style="left: {bubble.x}%; top: {100 - bubble.y}%; transform: translate(-50%, -50%);"
					onmouseenter={() => hoveredBubble = bubble}
					onmouseleave={() => hoveredBubble = null}
				>
					<div
						class="rounded-full flex items-center justify-center"
						style="
							width: {bubble.size * 2}px;
							height: {bubble.size * 2}px;
							background: {bubbleBg(bubble.type)};
							border: 2px solid {bubbleColor(bubble.type)};
						"
					>
						<span class="text-[8px] font-semibold text-center leading-tight px-1 select-none" style="color: {bubbleColor(bubble.type)};">
							{bubble.name}
						</span>
					</div>
				</div>
			{/each}

			<!-- Tooltip -->
			{#if hoveredBubble}
				<div
					class="absolute z-30 bg-gray-900 text-white rounded-lg p-3 text-xs shadow-xl max-w-[220px] pointer-events-none"
					style="left: {Math.min(hoveredBubble.x, 75)}%; top: {Math.max(100 - hoveredBubble.y - 15, 5)}%;"
				>
					<p class="font-semibold mb-1">{hoveredBubble.name}</p>
					<p class="text-gray-300 leading-relaxed">{hoveredBubble.detail}</p>
				</div>
			{/if}
		</div>
	</div>

	<!-- CORRELAZIONI CROSS-DIMENSIONALI -->
	<div>
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Correlazioni Cross-Dimensionali</h3>
		<div class="space-y-4">
			{#each strategicSignals as signal}
				<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 border-l-2 border-l-blue-500">
					<div>
						<div class="flex items-start justify-between gap-4">
							<div class="flex-1">
								<!-- Type badge -->
								<div class="flex items-center gap-2 mb-2">
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-semibold border {signal.typeColor}">
										{signal.type}
									</span>
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-bold text-white {signal.impactColor}">
										Impatto: {signal.impact}
									</span>
								</div>

								<h4 class="text-base font-semibold text-gray-900 mt-1">{signal.title}</h4>

								<!-- Evidence -->
								<div class="mt-3 space-y-1.5">
									{#each signal.evidence as ev}
										<div class="flex items-start gap-2">
											<div class="w-1 h-1 rounded-full bg-gray-300 mt-1.5 shrink-0"></div>
											<p class="text-sm text-gray-500">{ev}</p>
										</div>
									{/each}
								</div>

								<!-- Action -->
								<div class="mt-4 p-3 rounded-lg bg-gray-50 border border-gray-100">
									<p class="text-[10px] font-semibold uppercase tracking-wider text-gray-400 mb-1">Azione raccomandata</p>
									<p class="text-sm text-gray-700">{signal.action}</p>
								</div>
							</div>
						</div>
					</div>
				</div>
			{/each}
		</div>
	</div>

	<!-- INSIGHT CARDS (existing, improved) -->
	<div>
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Insight Dettagliati</h3>
		<div class="space-y-3">
			{#each sortedInsights as insight, idx}
				{@const tBadge = typeBadge(insight.type)}
				{@const sBadge = statusBadge(insight.status)}
				{@const correlation = getCorrelation(insight)}
				<div class="bg-white rounded-xl shadow-sm border border-gray-100 border-l-2 {typeLeftBorderColor(insight.type)} overflow-hidden">
					<div class="p-5 pl-6">
						<div class="flex items-start justify-between gap-4">
							<div class="flex-1 min-w-0">
								<div class="flex items-center gap-2 mb-2 flex-wrap">
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-medium {tBadge.classes}">
										{tBadge.label}
									</span>
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-medium {sBadge.classes}">
										{sBadge.label}
									</span>
									{#if correlation}
										<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-medium bg-gray-100 text-gray-500">
											Correlato a: {correlation}
										</span>
									{/if}
								</div>
								<h4 class="text-sm font-semibold text-gray-900">{insight.title}</h4>
								<p class="text-sm text-gray-500 mt-1 leading-relaxed">{insight.description}</p>

								<!-- Expandable details -->
								{#if insight.dataEvidence}
									<button
										onclick={() => toggleInsight(idx)}
										class="mt-2 text-xs text-blue-500 hover:text-blue-700 font-medium"
									>
										{expandedInsights.has(idx) ? 'Nascondi dettagli' : 'Mostra dettagli'}
									</button>
									{#if expandedInsights.has(idx)}
										<div class="mt-2 p-3 bg-gray-50 rounded-lg text-xs text-gray-600">
											<pre class="whitespace-pre-wrap font-sans">{JSON.stringify(insight.dataEvidence, null, 2)}</pre>
										</div>
									{/if}
								{/if}
							</div>
							<div class="shrink-0 w-16 text-right">
								<span class="text-2xl font-bold text-gray-900">{insight.score.toFixed(1)}</span>
								<div class="mt-1.5 h-1.5 bg-gray-100 rounded-full overflow-hidden">
									<div
										class="h-full rounded-full {scoreBarColor(insight.score)}"
										style="width: {(insight.score / 10) * 100}%"
									></div>
								</div>
							</div>
						</div>
					</div>
				</div>
			{:else}
				<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-12 text-center">
					<p class="text-gray-400">Nessun insight disponibile</p>
				</div>
			{/each}
		</div>
	</div>
</div>
