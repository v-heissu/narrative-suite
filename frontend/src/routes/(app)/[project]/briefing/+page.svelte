<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	const metricLabels: Record<string, string> = {
		nsov: 'Narrative Share of Voice',
		tos: 'Topic Opportunity Score',
		nri: 'Narrative Risk Indicator',
		tai: 'Trend Acceleration Index'
	};

	const metricFormats: Record<string, (v: number) => string> = {
		nsov: (v) => `${v.toFixed(1)}%`,
		tos: (v) => v.toFixed(1),
		nri: (v) => v.toFixed(1),
		tai: (v) => v.toFixed(1)
	};

	function priorityClasses(priority: string): string {
		switch (priority) {
			case 'Alta': return 'bg-red-100 text-red-800';
			case 'Media': return 'bg-amber-100 text-amber-800';
			case 'Bassa': return 'bg-gray-100 text-gray-600';
			default: return 'bg-gray-100 text-gray-600';
		}
	}

	function changeColor(current: number, previous: number, inverse = false): string {
		const diff = current - previous;
		if (inverse) return diff > 0 ? 'text-red-600' : diff < 0 ? 'text-emerald-600' : 'text-gray-500';
		return diff > 0 ? 'text-emerald-600' : diff < 0 ? 'text-red-600' : 'text-gray-500';
	}

	function changeLabel(current: number, previous: number): string {
		const diff = current - previous;
		if (diff > 0) return `+${diff.toFixed(1)}`;
		return diff.toFixed(1);
	}

	let scanDate = $derived(
		data.scan?.completedAt
			? new Date(data.scan.completedAt).toLocaleDateString('it-IT', { day: 'numeric', month: 'long', year: 'numeric' })
			: '18 marzo 2026'
	);
</script>

<div class="space-y-6">
	<div class="flex items-start justify-between">
		<div>
			<h2 class="text-2xl font-bold text-gray-900">Briefing Executive</h2>
			<p class="text-sm text-gray-500 mt-1">Report settimanale di intelligence narrativa</p>
		</div>
		<div class="text-right">
			<p class="text-sm font-medium text-gray-700">{scanDate}</p>
			<p class="text-xs text-gray-400">Scansione settimanale</p>
		</div>
	</div>

	<!-- Briefing Text -->
	{#if data.scan?.aiBriefing}
		<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-8">
			<div class="flex items-center gap-2 mb-6">
				<svg class="w-6 h-6 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
					<path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
				</svg>
				<h3 class="text-lg font-semibold text-gray-900">Sintesi Settimanale</h3>
			</div>
			<div class="prose prose-sm max-w-none text-gray-600 leading-relaxed whitespace-pre-line">
				{data.scan.aiBriefing}
			</div>
		</div>
	{:else}
		<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-8">
			<div class="flex items-center gap-2 mb-6">
				<svg class="w-6 h-6 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
					<path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
				</svg>
				<h3 class="text-lg font-semibold text-gray-900">Sintesi Settimanale</h3>
			</div>
			<p class="text-gray-400">Briefing non ancora disponibile. Avviare una scansione per generare il report.</p>
		</div>
	{/if}

	<!-- Highlights -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Highlights della Settimana</h3>
		<ul class="space-y-3">
			{#each data.highlights as highlight, i}
				<li class="flex items-start gap-3">
					<span class="shrink-0 w-6 h-6 rounded-full bg-blue-100 text-blue-700 flex items-center justify-center text-xs font-bold mt-0.5">
						{i + 1}
					</span>
					<p class="text-sm text-gray-700 leading-relaxed">{highlight}</p>
				</li>
			{/each}
		</ul>
	</div>

	<!-- Azioni Raccomandate -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Azioni Raccomandate</h3>
		<div class="space-y-4">
			{#each data.actions as action, i}
				<div class="flex items-start gap-4 p-4 bg-gray-50 rounded-lg">
					<span class="shrink-0 w-8 h-8 rounded-lg bg-gray-900 text-white flex items-center justify-center text-sm font-bold">
						{i + 1}
					</span>
					<div class="flex-1">
						<div class="flex items-center gap-2 mb-1">
							<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-semibold uppercase {priorityClasses(action.priority)}">
								{action.priority}
							</span>
						</div>
						<p class="text-sm text-gray-700 leading-relaxed">{action.text}</p>
					</div>
				</div>
			{/each}
		</div>
	</div>

	<!-- Confronto Settimana Precedente -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Confronto con Settimana Precedente</h3>
		<div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-4">
			{#each Object.entries(data.previousMetrics) as [key, vals]}
				{@const isInverse = key === 'nri'}
				<div class="border border-gray-100 rounded-lg p-4">
					<p class="text-xs font-medium text-gray-500 uppercase tracking-wider mb-3">{metricLabels[key] ?? key}</p>
					<div class="flex items-end justify-between">
						<div>
							<p class="text-xs text-gray-400">Precedente</p>
							<p class="text-lg text-gray-400 font-semibold">{metricFormats[key]?.(vals.previous) ?? vals.previous}</p>
						</div>
						<div class="text-center px-2">
							<svg class="w-5 h-5 text-gray-300 mx-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
								<path stroke-linecap="round" stroke-linejoin="round" d="M13.5 4.5L21 12m0 0l-7.5 7.5M21 12H3" />
							</svg>
						</div>
						<div class="text-right">
							<p class="text-xs text-gray-400">Attuale</p>
							<p class="text-lg text-gray-900 font-bold">{metricFormats[key]?.(vals.current) ?? vals.current}</p>
						</div>
					</div>
					<p class="text-sm font-semibold mt-2 text-right {changeColor(vals.current, vals.previous, isInverse)}">
						{changeLabel(vals.current, vals.previous)}
					</p>
				</div>
			{/each}
		</div>
	</div>
</div>
