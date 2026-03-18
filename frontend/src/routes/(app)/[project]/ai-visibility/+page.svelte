<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	const modelLabels: Record<string, string> = {
		chatgpt: 'ChatGPT',
		gemini: 'Gemini',
		perplexity: 'Perplexity',
		grok: 'Grok',
		claude: 'Claude'
	};

	const modelOrder = ['chatgpt', 'gemini', 'perplexity', 'grok', 'claude'];

	interface ModelSummary {
		model: string;
		label: string;
		avgScore: number;
		mentionCount: number;
		total: number;
	}

	let modelSummaries = $derived<ModelSummary[]>(() => {
		const grouped = new Map<string, { scores: number[]; mentions: number }>();
		for (const item of data.visibility) {
			const key = item.llmModel.toLowerCase();
			if (!grouped.has(key)) {
				grouped.set(key, { scores: [], mentions: 0 });
			}
			const g = grouped.get(key)!;
			g.scores.push(item.visibilityScore);
			if (item.brandMentioned) g.mentions++;
		}
		return modelOrder
			.filter((m) => grouped.has(m))
			.map((m) => {
				const g = grouped.get(m)!;
				return {
					model: m,
					label: modelLabels[m] ?? m,
					avgScore: g.scores.reduce((a, b) => a + b, 0) / g.scores.length,
					mentionCount: g.mentions,
					total: g.scores.length
				};
			});
	});

	function scoreColor(score: number): string {
		if (score >= 70) return 'bg-emerald-500';
		if (score >= 40) return 'bg-amber-500';
		return 'bg-red-500';
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">AI Visibility</h2>
		<p class="text-sm text-gray-500 mt-1">Visibilita del brand nelle risposte dei modelli AI</p>
	</div>

	<!-- Model Cards -->
	<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 gap-4">
		{#each modelSummaries() as summary}
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-5">
				<h3 class="text-sm font-semibold text-gray-900">{summary.label}</h3>
				<div class="mt-3">
					<div class="flex items-end justify-between mb-1.5">
						<span class="text-2xl font-bold text-gray-900">{summary.avgScore.toFixed(0)}</span>
						<span class="text-xs text-gray-400">/100</span>
					</div>
					<div class="h-2 bg-gray-100 rounded-full overflow-hidden">
						<div
							class="h-full rounded-full transition-all {scoreColor(summary.avgScore)}"
							style="width: {summary.avgScore}%"
						></div>
					</div>
				</div>
				<div class="mt-3 flex items-center justify-between">
					<span class="text-xs text-gray-500">Brand menzionato</span>
					<span class="text-xs font-medium {summary.mentionCount > 0 ? 'text-emerald-600' : 'text-gray-400'}">
						{summary.mentionCount}/{summary.total}
					</span>
				</div>
			</div>
		{/each}
	</div>

	<!-- Detail Table -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
		<div class="px-6 py-4 border-b border-gray-100">
			<h3 class="text-sm font-semibold text-gray-900">Dettaglio per keyword</h3>
		</div>
		<div class="overflow-x-auto">
			<table class="w-full text-sm">
				<thead>
					<tr class="border-b border-gray-100 bg-gray-50">
						<th class="text-left px-4 py-3 font-medium text-gray-500">Keyword</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Modello</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Score</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Brand menzionato</th>
					</tr>
				</thead>
				<tbody>
					{#each data.visibility as item}
						<tr class="border-b border-gray-50 hover:bg-gray-50 transition-colors">
							<td class="px-4 py-3 text-gray-900">{item.keyword}</td>
							<td class="px-4 py-3 text-gray-600">{modelLabels[item.llmModel.toLowerCase()] ?? item.llmModel}</td>
							<td class="px-4 py-3">
								<div class="flex items-center gap-2">
									<div class="w-16 h-1.5 bg-gray-100 rounded-full overflow-hidden">
										<div
											class="h-full rounded-full {scoreColor(item.visibilityScore)}"
											style="width: {item.visibilityScore}%"
										></div>
									</div>
									<span class="text-gray-700 font-medium">{item.visibilityScore}</span>
								</div>
							</td>
							<td class="px-4 py-3">
								{#if item.brandMentioned}
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-emerald-100 text-emerald-800">Si</span>
								{:else}
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-gray-100 text-gray-500">No</span>
								{/if}
							</td>
						</tr>
					{:else}
						<tr>
							<td colspan="4" class="px-4 py-12 text-center text-gray-400">Nessun dato disponibile</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
</div>
