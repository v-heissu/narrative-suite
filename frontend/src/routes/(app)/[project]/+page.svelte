<script lang="ts">
	import type { PageData } from './$types.js';
	import { page as pageStore } from '$app/stores';

	let { data }: { data: PageData } = $props();

	let projectSlug = $derived($pageStore.params.project ?? 'pirelli');

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

	<!-- Prossimi Passi -->
	<div>
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Prossimi Passi</h3>
		<div class="grid grid-cols-1 md:grid-cols-3 gap-4">
			<a href="/{projectSlug}/insights" class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 hover:shadow-md transition-shadow group">
				<div class="w-10 h-10 rounded-lg bg-blue-100 flex items-center justify-center mb-3">
					<svg class="w-5 h-5 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
						<path stroke-linecap="round" stroke-linejoin="round" d="M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z" />
					</svg>
				</div>
				<h4 class="text-sm font-semibold text-gray-900 group-hover:text-blue-600 transition-colors">Esplora le opportunita</h4>
				<p class="text-xs text-gray-500 mt-1">Analizza insight e gap competitivi identificati</p>
			</a>
			<a href="/{projectSlug}/ai-visibility" class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 hover:shadow-md transition-shadow group">
				<div class="w-10 h-10 rounded-lg bg-violet-100 flex items-center justify-center mb-3">
					<svg class="w-5 h-5 text-violet-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
						<path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
					</svg>
				</div>
				<h4 class="text-sm font-semibold text-gray-900 group-hover:text-violet-600 transition-colors">Verifica la visibilita AI</h4>
				<p class="text-xs text-gray-500 mt-1">Controlla come i modelli AI parlano del brand</p>
			</a>
			<a href="/{projectSlug}/editorial" class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 hover:shadow-md transition-shadow group">
				<div class="w-10 h-10 rounded-lg bg-emerald-100 flex items-center justify-center mb-3">
					<svg class="w-5 h-5 text-emerald-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
						<path stroke-linecap="round" stroke-linejoin="round" d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-3 7h3m-3 4h3m-6-4h.01M9 16h.01" />
					</svg>
				</div>
				<h4 class="text-sm font-semibold text-gray-900 group-hover:text-emerald-600 transition-colors">Pianifica i contenuti</h4>
				<p class="text-xs text-gray-500 mt-1">Gestisci il piano editoriale e i brief</p>
			</a>
		</div>
	</div>
</div>
