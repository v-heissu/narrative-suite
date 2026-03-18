<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	function statusClasses(status: string): string {
		switch (status) {
			case 'Nuovo': return 'bg-blue-100 text-blue-800';
			case 'In Lavorazione': return 'bg-amber-100 text-amber-800';
			case 'Completato': return 'bg-emerald-100 text-emerald-800';
			default: return 'bg-gray-100 text-gray-600';
		}
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Content Brief</h2>
		<p class="text-sm text-gray-500 mt-1">Brief editoriali basati sull'analisi SERP e insight strategici</p>
	</div>

	<div class="space-y-6">
		{#each data.briefs as brief}
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
				<div class="p-6">
					<div class="flex items-start justify-between mb-4">
						<div>
							<div class="flex items-center gap-3 mb-2">
								<h3 class="text-lg font-semibold text-gray-900">{brief.title}</h3>
								<span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-semibold {statusClasses(brief.status)}">
									{brief.status}
								</span>
							</div>
							<div class="flex items-center gap-1.5">
								<svg class="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
									<path stroke-linecap="round" stroke-linejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z" />
								</svg>
								<span class="text-sm text-gray-500">Target: <span class="font-medium text-gray-700">{brief.keyword}</span></span>
							</div>
						</div>
					</div>

					<!-- SERP Context -->
					<div class="mb-5">
						<p class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-2">Contesto SERP - Top 3 competitor</p>
						<div class="space-y-1.5">
							{#each brief.serpContext as item, i}
								<div class="flex items-center gap-3 text-sm">
									<span class="w-5 h-5 rounded bg-gray-100 flex items-center justify-center text-[10px] font-bold text-gray-500">{i + 1}</span>
									<span class="text-gray-700">{item.title}</span>
									<span class="text-xs text-gray-400">{item.domain}</span>
								</div>
							{/each}
						</div>
					</div>

					<!-- Suggested Structure -->
					<div class="mb-5">
						<p class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-2">Struttura suggerita</p>
						<div class="bg-gray-50 rounded-lg p-4">
							<p class="text-sm font-bold text-gray-900 mb-2">H1: {brief.structure.h1}</p>
							<ul class="space-y-1">
								{#each brief.structure.h2s as h2}
									<li class="text-sm text-gray-600 flex items-start gap-2">
										<span class="text-xs text-gray-400 font-mono mt-0.5 shrink-0">H2</span>
										{h2}
									</li>
								{/each}
							</ul>
						</div>
					</div>

					<!-- Actions -->
					<div class="flex items-center gap-3">
						<button disabled class="px-4 py-2 bg-gray-900 text-white text-sm font-medium rounded-lg opacity-50 cursor-not-allowed">
							Genera Contenuto
						</button>
						<button disabled class="px-4 py-2 bg-white border border-gray-200 text-gray-700 text-sm font-medium rounded-lg opacity-50 cursor-not-allowed">
							Apri Editor
						</button>
					</div>
				</div>
			</div>
		{/each}
	</div>
</div>
