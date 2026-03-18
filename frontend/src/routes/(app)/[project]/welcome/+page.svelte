<script lang="ts">
	import type { PageData } from './$types.js';
	import { page as pageStore } from '$app/stores';
	import { goto } from '$app/navigation';

	let { data }: { data: PageData } = $props();

	let projectSlug = $derived($pageStore.params.project ?? 'pirelli');
	let currentStep = $state(0);
	let direction = $state<'next' | 'prev'>('next');

	const metricConfig: Record<string, { label: string; abbr: string; color: string; format: (v: number) => string; explanation: string }> = {
		nsov: {
			label: 'Narrative Share of Voice',
			abbr: 'NsoV',
			color: 'text-blue-500',
			format: (v) => `${v.toFixed(1)}%`,
			explanation: 'La quota di visibilita narrativa del brand rispetto ai competitor su tutti i canali monitorati.'
		},
		tos: {
			label: 'Topic Opportunity Score',
			abbr: 'TOS',
			color: 'text-emerald-500',
			format: (v) => v.toFixed(1),
			explanation: 'Misura le opportunita su topic non ancora presidiati dal brand ma rilevanti per il mercato.'
		},
		nri: {
			label: 'Narrative Risk Indicator',
			abbr: 'NRI',
			color: 'text-red-500',
			format: (v) => v.toFixed(1),
			explanation: 'Indica il livello di rischio narrativo, inclusi sentiment negativo e narrazioni avverse.'
		},
		tai: {
			label: 'Trend Acceleration Index',
			abbr: 'TAI',
			color: 'text-violet-500',
			format: (v) => v.toFixed(1),
			explanation: 'L\'accelerazione dei trend rilevanti per il brand. Valori alti indicano cambiamenti rapidi.'
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

	// AI models for step 2
	const aiModels = [
		{ name: 'ChatGPT', mentioned: true },
		{ name: 'Gemini', mentioned: false },
		{ name: 'Perplexity', mentioned: true },
		{ name: 'Claude', mentioned: true },
		{ name: 'Copilot', mentioned: false },
	];

	// Top highlights for step 2
	const highlights = [
		'Pirelli P Zero mantiene la leadership nelle SERP per pneumatici sportivi',
		'Michelin guadagna terreno nella narrativa sulla sostenibilita nelle risposte AI',
		'Nuovo trend in forte crescita: pneumatici per veicoli elettrici (+340% TAI)',
	];

	// Top insights for step 3
	let topInsights = $derived(
		[...data.insights]
			.sort((a, b) => b.score - a.score)
			.slice(0, 3)
	);

	function nextStep() {
		if (currentStep < 2) {
			direction = 'next';
			currentStep++;
		}
	}

	function prevStep() {
		if (currentStep > 0) {
			direction = 'prev';
			currentStep--;
		}
	}

	function goToDashboard() {
		goto(`/${projectSlug}`);
	}

	const insightColors = ['bg-blue-600', 'bg-violet-600', 'bg-emerald-600'];
</script>

<div class="max-w-3xl mx-auto py-8">
	<!-- Header -->
	<div class="text-center mb-10">
		<h2 class="text-2xl font-bold text-gray-900">Benvenuto in Narrative Suite</h2>
		<p class="text-sm text-gray-500 mt-1">Un tour rapido dei tuoi dati di intelligence narrativa</p>
	</div>

	<!-- Steps container -->
	<div class="relative min-h-[480px]">

		<!-- Step 1: Il tuo ecosistema narrativo -->
		{#if currentStep === 0}
			<div>
				<div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-8">
					<div class="text-center mb-8">
						<span class="inline-flex items-center px-3 py-1 rounded-full text-xs font-semibold bg-blue-50 text-blue-600 border border-blue-100">
							Passo 1 di 3
						</span>
						<h3 class="text-xl font-bold text-gray-900 mt-4">Il tuo ecosistema narrativo</h3>
						<p class="text-sm text-gray-500 mt-1">Ecco lo stato attuale delle tue metriche chiave</p>
					</div>

					<div class="grid grid-cols-2 gap-6">
						{#each orderedMetrics as metric}
							{#if metric}
								<div class="text-center p-6 rounded-xl bg-gray-50 border border-gray-100">
									<p class="text-[10px] font-bold uppercase tracking-wider text-gray-400 mb-2">{metric.abbr}</p>
									<p class="text-4xl font-bold {metric.color}">{metric.format(metric.value)}</p>
									<p class="text-xs text-gray-500 mt-3 leading-relaxed">{metric.explanation}</p>
								</div>
							{/if}
						{/each}
					</div>
				</div>
			</div>
		{/if}

		<!-- Step 2: Cosa sta succedendo -->
		{#if currentStep === 1}
			<div>
				<div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-8">
					<div class="text-center mb-8">
						<span class="inline-flex items-center px-3 py-1 rounded-full text-xs font-semibold bg-violet-50 text-violet-600 border border-violet-100">
							Passo 2 di 3
						</span>
						<h3 class="text-xl font-bold text-gray-900 mt-4">Cosa sta succedendo</h3>
						<p class="text-sm text-gray-500 mt-1">I principali risultati dell'ultima scansione</p>
					</div>

					<!-- Highlights -->
					<div class="space-y-3 mb-8">
						{#each highlights as highlight, idx}
							<div class="flex items-start gap-3 p-4 rounded-xl bg-gray-50 border border-gray-100">
								<div class="w-6 h-6 rounded-full bg-blue-600 flex items-center justify-center shrink-0 mt-0.5">
									<span class="text-white text-xs font-bold">{idx + 1}</span>
								</div>
								<p class="text-sm text-gray-700 leading-relaxed">{highlight}</p>
							</div>
						{/each}
					</div>

					<!-- AI Models -->
					<div>
						<p class="text-xs font-semibold uppercase tracking-wider text-gray-400 mb-3">Modelli AI che menzionano Pirelli</p>
						<div class="flex items-center gap-3">
							{#each aiModels as model}
								<div class="flex-1 p-3 rounded-xl text-center border {model.mentioned ? 'bg-blue-50 border-blue-200' : 'bg-gray-50 border-gray-200 opacity-40'}">
									<p class="text-xs font-semibold {model.mentioned ? 'text-blue-700' : 'text-gray-400'}">{model.name}</p>
									<p class="text-[10px] mt-0.5 {model.mentioned ? 'text-blue-500' : 'text-gray-400'}">
										{model.mentioned ? 'Menzionato' : 'Assente'}
									</p>
								</div>
							{/each}
						</div>
					</div>
				</div>
			</div>
		{/if}

		<!-- Step 3: Le tue priorita -->
		{#if currentStep === 2}
			<div>
				<div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-8">
					<div class="text-center mb-8">
						<span class="inline-flex items-center px-3 py-1 rounded-full text-xs font-semibold bg-emerald-50 text-emerald-600 border border-emerald-100">
							Passo 3 di 3
						</span>
						<h3 class="text-xl font-bold text-gray-900 mt-4">Le tue priorita</h3>
						<p class="text-sm text-gray-500 mt-1">Le opportunita e i rischi a piu alto impatto</p>
					</div>

					<div class="space-y-4">
						{#each topInsights as insight, idx}
							<div class="flex items-start gap-4 p-5 rounded-xl bg-gray-50 border border-gray-100">
								<div class="w-10 h-10 rounded-xl {insightColors[idx]} flex items-center justify-center shrink-0">
									<span class="text-white text-sm font-bold">{insight.score.toFixed(0)}</span>
								</div>
								<div class="flex-1">
									<h4 class="text-sm font-semibold text-gray-900">{insight.title}</h4>
									<p class="text-xs text-gray-500 mt-1 leading-relaxed">{insight.description}</p>
								</div>
							</div>
						{:else}
							<div class="p-8 text-center text-gray-400 text-sm">
								Nessun insight disponibile al momento.
							</div>
						{/each}
					</div>
				</div>
			</div>
		{/if}
	</div>

	<!-- Navigation -->
	<div class="flex items-center justify-between mt-8">
		<button
			onclick={prevStep}
			disabled={currentStep === 0}
			class="px-5 py-2.5 text-sm font-medium rounded-xl border border-gray-200 text-gray-600 hover:bg-gray-50 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
		>
			Indietro
		</button>

		<!-- Step dots -->
		<div class="flex items-center gap-2">
			{#each [0, 1, 2] as step}
				<button
					onclick={() => { direction = step > currentStep ? 'next' : 'prev'; currentStep = step; }}
					class="h-2.5 rounded-full transition-all duration-300 {currentStep === step ? 'bg-blue-600 w-8' : 'bg-gray-300 hover:bg-gray-400 w-2.5'}"
					aria-label="Vai al passo {step + 1}"
				></button>
			{/each}
		</div>

		{#if currentStep < 2}
			<button
				onclick={nextStep}
				class="px-5 py-2.5 text-sm font-medium rounded-xl bg-blue-600 text-white hover:bg-blue-700 transition-colors shadow-sm"
			>
				{currentStep === 0 ? 'Scopri i dettagli' : 'Cosa fare'}
				<span class="ml-1">&rarr;</span>
			</button>
		{:else}
			<button
				onclick={goToDashboard}
				class="px-5 py-2.5 text-sm font-medium rounded-xl bg-blue-600 text-white hover:bg-blue-700 transition-colors shadow-sm"
			>
				Vai alla Dashboard &rarr;
			</button>
		{/if}
	</div>
</div>
