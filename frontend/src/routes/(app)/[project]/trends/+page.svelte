<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	function badgeClasses(badge: string): string {
		switch (badge) {
			case 'Nuovo': return 'bg-violet-100 text-violet-800';
			case 'In crescita': return 'bg-emerald-100 text-emerald-800';
			case 'Stabile': return 'bg-gray-100 text-gray-600';
			case 'In calo': return 'bg-red-100 text-red-800';
			default: return 'bg-gray-100 text-gray-600';
		}
	}

	function barColor(value: number): string {
		if (value >= 8) return 'bg-emerald-500';
		if (value >= 5) return 'bg-blue-400';
		if (value >= 3) return 'bg-amber-400';
		return 'bg-gray-300';
	}

	function changeColor(val: number): string {
		if (val > 0) return 'text-emerald-600';
		if (val < 0) return 'text-red-600';
		return 'text-gray-500';
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Trend Radar</h2>
		<p class="text-sm text-gray-500 mt-1">Segnali di trend e topic emergenti nel settore pneumatici</p>
	</div>

	<!-- Trend in Accelerazione -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
		<div class="px-6 py-4 border-b border-gray-100">
			<h3 class="text-lg font-semibold text-gray-900">Trend in Accelerazione</h3>
			<p class="text-sm text-gray-500 mt-0.5">Topic ordinati per Trend Acceleration Index</p>
		</div>
		<div class="divide-y divide-gray-50">
			{#each data.trends as trend}
				<div class="px-6 py-4 flex items-center gap-6 hover:bg-gray-50 transition-colors">
					<div class="flex-1 min-w-0">
						<div class="flex items-center gap-3">
							<h4 class="text-sm font-semibold text-gray-900">{trend.topic}</h4>
							<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-semibold uppercase {badgeClasses(trend.badge)}">
								{trend.badge}
							</span>
						</div>
					</div>
					<div class="flex items-center gap-1.5 shrink-0 w-32">
						{#each trend.bars as bar}
							<div class="w-3 rounded-sm {barColor(bar)}" style="height: {bar * 3 + 2}px"></div>
						{/each}
					</div>
					<div class="shrink-0 w-24 text-right">
						<p class="text-xs text-gray-500">TAI</p>
						<p class="text-lg font-bold text-gray-900">+{trend.tai}%</p>
					</div>
					<div class="shrink-0 w-20 text-right">
						<p class="text-xs text-gray-500">Settimana</p>
						<p class="text-sm font-semibold {changeColor(trend.weeklyChange)}">
							{trend.weeklyChange > 0 ? '+' : ''}{trend.weeklyChange}%
						</p>
					</div>
				</div>
			{/each}
		</div>
	</div>

	<!-- Segnali Emergenti -->
	<div>
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Segnali Emergenti</h3>
		<div class="grid grid-cols-1 lg:grid-cols-2 gap-4">
			{#each data.emergingSignals as signal}
				<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
					<div class="flex items-start justify-between mb-3">
						<h4 class="text-base font-semibold text-gray-900">{signal.title}</h4>
						<span class="shrink-0 ml-3 inline-flex items-center px-2.5 py-1 rounded-lg text-xs font-bold bg-violet-100 text-violet-800">
							+{signal.growth}%
						</span>
					</div>
					<p class="text-sm text-gray-500 leading-relaxed mb-4">{signal.description}</p>
					<div class="bg-blue-50 rounded-lg p-3">
						<p class="text-xs font-semibold text-blue-800 uppercase tracking-wider mb-1">Azione raccomandata</p>
						<p class="text-sm text-blue-700">{signal.action}</p>
					</div>
				</div>
			{/each}
		</div>
	</div>
</div>
