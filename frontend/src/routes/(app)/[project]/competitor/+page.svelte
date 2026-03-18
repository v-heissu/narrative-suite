<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	const brands = ['Pirelli', 'Michelin', 'Continental', 'Bridgestone', 'Goodyear', 'Hankook'];

	function positionColor(pos: number): string {
		if (pos <= 3) return 'bg-emerald-100 text-emerald-800';
		if (pos <= 7) return 'bg-amber-100 text-amber-800';
		return 'bg-red-100 text-red-800';
	}

	function trendIcon(trend: string): string {
		return trend === 'up' ? 'M5 10l7-7m0 0l7 7m-7-7v18' : 'M19 14l-7 7m0 0l-7-7m7 7V3';
	}

	function trendColor(trend: string): string {
		return trend === 'up' ? 'text-emerald-500' : 'text-red-500';
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Competitor Analysis</h2>
		<p class="text-sm text-gray-500 mt-1">Confronto con i principali competitor nel settore pneumatici</p>
	</div>

	<!-- Competitor Cards -->
	<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 gap-4">
		{#each data.competitors as comp}
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-5">
				<div class="flex items-center justify-between mb-3">
					<h3 class="text-sm font-semibold text-gray-900">{comp.name}</h3>
					<svg class="w-4 h-4 {trendColor(comp.trend)}" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
						<path stroke-linecap="round" stroke-linejoin="round" d={trendIcon(comp.trend)} />
					</svg>
				</div>
				<div class="mb-3">
					<p class="text-xs text-gray-500 uppercase tracking-wider">NsoV Score</p>
					<span class="text-2xl font-bold text-gray-900">{comp.nsov}%</span>
				</div>
				<div class="h-2 bg-gray-100 rounded-full overflow-hidden mb-4">
					<div
						class="h-full rounded-full bg-blue-500 transition-all"
						style="width: {comp.nsov}%"
					></div>
				</div>
				<div>
					<p class="text-xs text-gray-500 mb-1.5">Top keyword</p>
					<div class="space-y-1">
						{#each comp.topKeywords as kw}
							<p class="text-xs text-gray-600 truncate">{kw}</p>
						{/each}
					</div>
				</div>
			</div>
		{/each}
	</div>

	<!-- Visibility Comparison Table -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
		<div class="px-6 py-4 border-b border-gray-100">
			<h3 class="text-lg font-semibold text-gray-900">Confronto Visibilita</h3>
			<p class="text-sm text-gray-500 mt-0.5">Posizioni SERP per keyword principali</p>
		</div>
		<div class="overflow-x-auto">
			<table class="w-full text-sm">
				<thead>
					<tr class="border-b border-gray-100 bg-gray-50">
						<th class="text-left px-4 py-3 font-medium text-gray-500">Keyword</th>
						{#each brands as brand}
							<th class="text-center px-4 py-3 font-medium text-gray-500 {brand === 'Pirelli' ? 'bg-blue-50' : ''}">
								{brand}
							</th>
						{/each}
					</tr>
				</thead>
				<tbody>
					{#each data.visibilityData as row}
						<tr class="border-b border-gray-50 hover:bg-gray-50 transition-colors">
							<td class="px-4 py-3 text-gray-900 font-medium">{row.keyword}</td>
							{#each brands as brand}
								{@const pos = row.positions[brand]}
								<td class="px-4 py-3 text-center {brand === 'Pirelli' ? 'bg-blue-50/50' : ''}">
									<span class="inline-flex items-center justify-center w-8 h-8 rounded-lg text-xs font-semibold {positionColor(pos)}">
										{pos}
									</span>
								</td>
							{/each}
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
</div>
