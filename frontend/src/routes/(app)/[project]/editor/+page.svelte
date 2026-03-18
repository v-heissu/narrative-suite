<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	function scoreColor(score: number): string {
		if (score >= 0.8) return 'bg-emerald-500';
		if (score >= 0.6) return 'bg-amber-500';
		return 'bg-red-500';
	}

	function scoreTextColor(score: number): string {
		if (score >= 0.8) return 'text-emerald-600';
		if (score >= 0.6) return 'text-amber-600';
		return 'text-red-600';
	}

	let overallPercent = $derived(Math.round(data.brandVoice.overall * 100));
	let circumference = 2 * Math.PI * 40;
	let dashOffset = $derived(circumference - (data.brandVoice.overall * circumference));
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Editor</h2>
		<p class="text-sm text-gray-500 mt-1">Creazione e ottimizzazione contenuti con analisi Brand Voice</p>
	</div>

	<div class="flex gap-6 items-start">
		<!-- Editor Area -->
		<div class="flex-1 min-w-0">
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
				<!-- Toolbar -->
				<div class="px-4 py-2.5 border-b border-gray-100 flex items-center gap-1 bg-gray-50">
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500">
						<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
							<path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h7" />
						</svg>
					</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 font-bold text-sm">B</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 italic text-sm">I</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 underline text-sm">U</button>
					<div class="w-px h-5 bg-gray-200 mx-1"></div>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 text-sm font-mono">H1</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 text-sm font-mono">H2</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500 text-sm font-mono">H3</button>
					<div class="w-px h-5 bg-gray-200 mx-1"></div>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500">
						<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
							<path stroke-linecap="round" stroke-linejoin="round" d="M13.828 10.172a4 4 0 00-5.656 0l-4 4a4 4 0 105.656 5.656l1.102-1.101m-.758-4.899a4 4 0 005.656 0l4-4a4 4 0 00-5.656-5.656l-1.1 1.1" />
						</svg>
					</button>
					<button disabled class="p-1.5 rounded hover:bg-gray-200 text-gray-500">
						<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
							<path stroke-linecap="round" stroke-linejoin="round" d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
						</svg>
					</button>
				</div>
				<!-- Content -->
				<div class="p-8 prose prose-sm max-w-none min-h-[600px]">
					{@html data.article.content}
				</div>
			</div>
		</div>

		<!-- Brand Voice Panel -->
		<div class="w-80 shrink-0 space-y-4">
			<!-- Overall Score -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Brand Voice Check</h3>
				<div class="flex items-center justify-center mb-6">
					<div class="relative">
						<svg class="w-24 h-24 -rotate-90" viewBox="0 0 100 100">
							<circle cx="50" cy="50" r="40" fill="none" stroke="#f3f4f6" stroke-width="8" />
							<circle
								cx="50"
								cy="50"
								r="40"
								fill="none"
								stroke="#10b981"
								stroke-width="8"
								stroke-linecap="round"
								stroke-dasharray={circumference}
								stroke-dashoffset={dashOffset}
							/>
						</svg>
						<div class="absolute inset-0 flex items-center justify-center">
							<span class="text-xl font-bold text-gray-900">{overallPercent}%</span>
						</div>
					</div>
				</div>
				<p class="text-center text-sm text-gray-500 mb-1">Punteggio complessivo</p>
				<p class="text-center text-2xl font-bold text-gray-900">{data.brandVoice.overall.toFixed(2)}</p>
			</div>

			<!-- Breakdown -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Dettaglio Punteggi</h3>
				<div class="space-y-4">
					{#each data.brandVoice.breakdown as item}
						<div>
							<div class="flex items-center justify-between mb-1.5">
								<span class="text-sm text-gray-600">{item.label}</span>
								<span class="text-sm font-semibold {scoreTextColor(item.score)}">{item.score.toFixed(2)}</span>
							</div>
							<div class="h-2 bg-gray-100 rounded-full overflow-hidden">
								<div
									class="h-full rounded-full transition-all {scoreColor(item.score)}"
									style="width: {item.score * 100}%"
								></div>
							</div>
						</div>
					{/each}
				</div>
			</div>

			<!-- Suggestions -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Suggerimenti</h3>
				<div class="space-y-3">
					{#each data.brandVoice.suggestions as suggestion, i}
						<div class="flex items-start gap-3">
							<span class="shrink-0 w-5 h-5 rounded-full bg-amber-100 text-amber-700 flex items-center justify-center text-[10px] font-bold mt-0.5">
								{i + 1}
							</span>
							<p class="text-sm text-gray-600 leading-relaxed">{suggestion}</p>
						</div>
					{/each}
				</div>
			</div>
		</div>
	</div>
</div>
