<script lang="ts">
	import type { PageData } from './$types.js';
	import { page as pageStore } from '$app/stores';

	let { data }: { data: PageData } = $props();

	let projectSlug = $derived($pageStore.params.project ?? 'pirelli');

	const metricConfig: Record<string, { label: string; description: string; borderColor: string; iconColor: string; format: (v: number) => string; delta: string; deltaPositive: boolean }> = {
		nsov: {
			label: 'Narrative Share of Voice',
			description: 'Quota di visibilita narrativa complessiva del brand',
			borderColor: 'border-blue-500',
			iconColor: 'text-blue-600',
			format: (v) => `${v.toFixed(1)}%`,
			delta: '+2.1 vs settimana prec.',
			deltaPositive: true
		},
		tos: {
			label: 'Topic Opportunity Score',
			description: 'Punteggio di opportunita su topic non ancora presidiati',
			borderColor: 'border-emerald-500',
			iconColor: 'text-emerald-600',
			format: (v) => v.toFixed(1),
			delta: '+0.8 vs settimana prec.',
			deltaPositive: true
		},
		nri: {
			label: 'Narrative Risk Indicator',
			description: 'Indicatore di rischio narrativo e sentiment negativo',
			borderColor: 'border-red-500',
			iconColor: 'text-red-500',
			format: (v) => v.toFixed(1),
			delta: '-0.3 vs settimana prec.',
			deltaPositive: true
		},
		tai: {
			label: 'Trend Acceleration Index',
			description: 'Indice di accelerazione dei trend rilevanti',
			borderColor: 'border-violet-500',
			iconColor: 'text-violet-600',
			format: (v) => v.toFixed(1),
			delta: '+1.4 vs settimana prec.',
			deltaPositive: true
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

	let briefingExpanded = $state(false);

	let briefingPreview = $derived(() => {
		if (!data.scan?.aiBriefing) return '';
		const text = data.scan.aiBriefing;
		const firstParagraph = text.split('\n\n')[0];
		return firstParagraph.length > 200 ? firstParagraph.substring(0, 200) + '...' : firstParagraph;
	});

	const strategicActions = [
		{
			title: 'Esplora le opportunita',
			description: 'Analizza insight, gap competitivi e correlazioni strategiche identificate',
			href: 'insights',
			color: 'bg-blue-600',
			icon: 'M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z'
		},
		{
			title: 'Verifica la visibilita AI',
			description: 'Controlla come i modelli AI parlano del brand nelle risposte',
			href: 'ai-visibility',
			color: 'bg-violet-600',
			icon: 'M15 12a3 3 0 11-6 0 3 3 0 016 0z M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z'
		},
		{
			title: 'Pianifica i contenuti',
			description: 'Gestisci il piano editoriale e genera brief strategici',
			href: 'editorial',
			color: 'bg-emerald-600',
			icon: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-3 7h3m-3 4h3m-6-4h.01M9 16h.01'
		}
	];
</script>

<div class="space-y-6 max-w-7xl mx-auto">
	<!-- Header -->
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Buongiorno</h2>
		<p class="text-sm text-gray-500 mt-1">
			{new Date().toLocaleDateString('it-IT', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' })}
			&mdash; NsoV al <span class="text-blue-600 font-semibold">34.2%</span> <span class="text-emerald-600 text-sm">(+2.1)</span>, 3 opportunita prioritarie identificate
		</p>
	</div>

	<!-- KPI Cards -->
	<div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-4">
		{#each orderedMetrics as metric}
			{#if metric}
				<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 border-t-2 {metric.borderColor}">
					<p class="text-[10px] font-semibold text-gray-400 uppercase tracking-wider">{metric.label}</p>

					<div class="mt-3 flex items-end gap-2">
						<span class="text-3xl font-bold {metric.iconColor}">
							{metric.format(metric.value)}
						</span>
					</div>

					<!-- Delta -->
					<p class="text-xs mt-1.5 {metric.deltaPositive ? 'text-emerald-500' : 'text-red-500'}">
						{metric.delta}
					</p>

					<p class="text-[10px] text-gray-400 mt-3">{metric.description}</p>
				</div>
			{/if}
		{/each}
	</div>

	<!-- Executive Briefing -->
	{#if data.scan?.aiBriefing}
		<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 border-l-2 border-l-blue-500">
			<div class="flex items-center justify-between mb-4">
				<div class="flex items-center gap-3">
					<svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
						<path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
					</svg>
					<h3 class="text-lg font-semibold text-gray-900">Executive Briefing</h3>
					<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-medium bg-violet-50 text-violet-600 border border-violet-100">
						Generato automaticamente
					</span>
				</div>
				<button
					onclick={() => briefingExpanded = !briefingExpanded}
					class="text-sm text-gray-400 hover:text-gray-600 flex items-center gap-1"
				>
					{briefingExpanded ? 'Comprimi' : 'Espandi'}
					<svg class="w-4 h-4 transition-transform duration-200 {briefingExpanded ? 'rotate-180' : ''}" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
						<path stroke-linecap="round" stroke-linejoin="round" d="M19 9l-7 7-7-7" />
					</svg>
				</button>
			</div>

			<div>
				{#if !briefingExpanded}
					<div class="prose prose-sm max-w-none text-gray-600 leading-relaxed">
						{briefingPreview()}
					</div>
				{/if}
				<div class="collapsible {briefingExpanded ? 'expanded' : ''}">
					<div>
						<div class="prose prose-sm max-w-none text-gray-600 leading-relaxed whitespace-pre-line">
							{data.scan.aiBriefing}
						</div>
					</div>
				</div>
			</div>

			{#if data.scan.completedAt}
				<p class="text-[10px] text-gray-400 mt-4">
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

	<!-- Azioni Strategiche -->
	<div>
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Azioni Strategiche</h3>
		<div class="grid grid-cols-1 md:grid-cols-3 gap-4">
			{#each strategicActions as action}
				<a
					href="/{projectSlug}/{action.href}"
					class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 group hover:border-gray-200 transition-colors"
				>
					<div class="w-10 h-10 rounded-xl {action.color} flex items-center justify-center mb-4">
						<svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
							<path stroke-linecap="round" stroke-linejoin="round" d={action.icon} />
						</svg>
					</div>
					<h4 class="text-sm font-semibold text-gray-900">{action.title}</h4>
					<p class="text-xs text-gray-500 mt-1.5 leading-relaxed">{action.description}</p>

					<div class="flex items-center gap-1 mt-4 text-xs font-medium text-gray-400 group-hover:text-blue-500 transition-colors duration-200">
						<span>Scopri</span>
						<svg class="w-3.5 h-3.5" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
							<path stroke-linecap="round" stroke-linejoin="round" d="M9 5l7 7-7 7" />
						</svg>
					</div>
				</a>
			{/each}
		</div>
	</div>
</div>
