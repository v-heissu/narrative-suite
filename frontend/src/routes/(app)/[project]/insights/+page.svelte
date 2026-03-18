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
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Insight & Opportunita</h2>
		<p class="text-sm text-gray-500 mt-1">Analisi strategiche ordinate per rilevanza</p>
	</div>

	<div class="space-y-4">
		{#each sortedInsights as insight}
			{@const tBadge = typeBadge(insight.type)}
			{@const sBadge = statusBadge(insight.status)}
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<div class="flex items-start justify-between gap-4">
					<div class="flex-1 min-w-0">
						<div class="flex items-center gap-2 mb-2">
							<span class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium {tBadge.classes}">
								{tBadge.label}
							</span>
							<span class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium {sBadge.classes}">
								{sBadge.label}
							</span>
						</div>
						<h3 class="text-base font-semibold text-gray-900">{insight.title}</h3>
						<p class="text-sm text-gray-500 mt-1 leading-relaxed">{insight.description}</p>
					</div>
					<div class="shrink-0 w-20 text-right">
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
		{:else}
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-12 text-center">
				<p class="text-gray-400">Nessun insight disponibile</p>
			</div>
		{/each}
	</div>
</div>
