<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	const metricConfig: Record<string, { label: string; description: string; color: string; format: (v: number) => string }> = {
		nsov: {
			label: 'Narrative Share of Voice',
			description: 'Quota di visibilita narrativa complessiva del brand',
			color: 'text-blue-600',
			format: (v) => `${v.toFixed(1)}%`
		},
		tos: {
			label: 'Topic Opportunity Score',
			description: 'Punteggio di opportunita su topic non ancora presidiati',
			color: 'text-emerald-600',
			format: (v) => v.toFixed(1)
		},
		nri: {
			label: 'Narrative Risk Indicator',
			description: 'Indicatore di rischio narrativo e sentiment negativo',
			color: 'text-red-500',
			format: (v) => v.toFixed(1)
		},
		tai: {
			label: 'Trend Acceleration Index',
			description: 'Indice di accelerazione dei trend rilevanti',
			color: 'text-violet-600',
			format: (v) => v.toFixed(1)
		}
	};

	const metricOrder = ['nsov', 'tos', 'nri', 'tai'];

	let orderedMetrics = $derived(
		metricOrder
			.map((type) => {
				const metric = data.metrics.find((m) => m.metricType === type);
				const config = metricConfig[type];
				return metric && config ? { ...metric, ...config } : null;
			})
			.filter(Boolean)
	);

	function riskLevel(value: number): string {
		if (value >= 7) return 'bg-red-50 border-red-200';
		if (value >= 4) return 'bg-amber-50 border-amber-200';
		return 'bg-green-50 border-green-200';
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Dashboard</h2>
		<p class="text-sm text-gray-500 mt-1">Panoramica delle metriche narrative</p>
	</div>

	<!-- KPI Cards -->
	<div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-4">
		{#each orderedMetrics as metric}
			{#if metric}
				<div
					class="bg-white rounded-xl shadow-sm border p-6 {metric.metricType === 'nri'
						? riskLevel(metric.value)
						: 'border-gray-100'}"
				>
					<p class="text-xs font-medium text-gray-500 uppercase tracking-wider">{metric.label}</p>
					<div class="mt-3 flex items-end gap-2">
						<span class="text-3xl font-bold {metric.color}">{metric.format(metric.value)}</span>
						{#if metric.metricType === 'tai'}
							<svg class="w-5 h-5 mb-1 {metric.value >= 0 ? 'text-emerald-500' : 'text-red-500'}" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
								{#if metric.value >= 0}
									<path stroke-linecap="round" stroke-linejoin="round" d="M5 10l7-7m0 0l7 7m-7-7v18" />
								{:else}
									<path stroke-linecap="round" stroke-linejoin="round" d="M19 14l-7 7m0 0l-7-7m7 7V3" />
								{/if}
							</svg>
						{/if}
					</div>
					<p class="text-xs text-gray-400 mt-2">{metric.description}</p>
				</div>
			{/if}
		{/each}
	</div>

	<!-- Executive Briefing -->
	{#if data.scan?.aiBriefing}
		<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
			<div class="flex items-center gap-2 mb-4">
				<svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
					<path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
				</svg>
				<h3 class="text-lg font-semibold text-gray-900">Executive Briefing</h3>
			</div>
			<div class="prose prose-sm max-w-none text-gray-600 leading-relaxed whitespace-pre-line">
				{data.scan.aiBriefing}
			</div>
			{#if data.scan.completedAt}
				<p class="text-xs text-gray-400 mt-4">
					Ultimo aggiornamento: {new Date(data.scan.completedAt).toLocaleString('it-IT')}
				</p>
			{/if}
		</div>
	{/if}

	<!-- Scan status -->
	{#if data.scan && data.scan.status !== 'completed'}
		<div class="bg-white rounded-xl shadow-sm border border-amber-200 bg-amber-50 p-4">
			<p class="text-sm text-amber-800">
				Scansione in corso: {data.scan.completedTasks}/{data.scan.totalTasks} task completati
			</p>
			<div class="mt-2 h-2 bg-amber-100 rounded-full overflow-hidden">
				<div
					class="h-full bg-amber-500 rounded-full transition-all"
					style="width: {data.scan.totalTasks > 0 ? (data.scan.completedTasks / data.scan.totalTasks) * 100 : 0}%"
				></div>
			</div>
		</div>
	{/if}
</div>
