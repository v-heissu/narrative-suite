<script lang="ts">
	import { goto } from '$app/navigation';
	import { page as pageStore } from '$app/stores';
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	let keywordFilter = $state('');
	let sourceFilter = $state('');

	let allKeywords = $derived(
		[...new Set(data.results.items.map((r) => r.keyword))].sort()
	);

	let allSources = $derived(
		[...new Set(data.results.items.map((r) => r.source))].sort()
	);

	let filteredItems = $derived(
		data.results.items.filter((item) => {
			if (keywordFilter && item.keyword !== keywordFilter) return false;
			if (sourceFilter && item.source !== sourceFilter) return false;
			return true;
		})
	);

	let totalPages = $derived(Math.ceil(data.results.total / data.results.pageSize));
	let currentPage = $derived(data.results.page);

	function goToPage(p: number) {
		const url = new URL($pageStore.url);
		url.searchParams.set('page', String(p));
		goto(url.toString());
	}

	function sentimentColor(sentiment: string): string {
		switch (sentiment) {
			case 'positive': return 'bg-emerald-100 text-emerald-800';
			case 'negative': return 'bg-red-100 text-red-800';
			case 'mixed': return 'bg-amber-100 text-amber-800';
			default: return 'bg-gray-100 text-gray-600';
		}
	}

	function sentimentLabel(sentiment: string): string {
		switch (sentiment) {
			case 'positive': return 'Positivo';
			case 'negative': return 'Negativo';
			case 'mixed': return 'Misto';
			default: return 'Neutro';
		}
	}

	function sourceLabel(source: string): string {
		switch (source) {
			case 'google_organic': return 'Google';
			case 'google_news': return 'News';
			case 'ai_overview': return 'AI Overview';
			default: return source;
		}
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Monitor SERP</h2>
		<p class="text-sm text-gray-500 mt-1">Risultati di ricerca e analisi del sentiment</p>
	</div>

	<!-- Filters -->
	<div class="flex flex-wrap gap-3">
		<select
			bind:value={keywordFilter}
			class="px-3 py-2 bg-white border border-gray-200 rounded-lg text-sm text-gray-700 focus:outline-none focus:ring-2 focus:ring-gray-300"
		>
			<option value="">Tutte le keyword</option>
			{#each allKeywords as kw}
				<option value={kw}>{kw}</option>
			{/each}
		</select>

		<select
			bind:value={sourceFilter}
			class="px-3 py-2 bg-white border border-gray-200 rounded-lg text-sm text-gray-700 focus:outline-none focus:ring-2 focus:ring-gray-300"
		>
			<option value="">Tutte le fonti</option>
			{#each allSources as src}
				<option value={src}>{sourceLabel(src)}</option>
			{/each}
		</select>
	</div>

	<!-- Table -->
	<div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
		<div class="overflow-x-auto">
			<table class="w-full text-sm">
				<thead>
					<tr class="border-b border-gray-100 bg-gray-50">
						<th class="text-left px-4 py-3 font-medium text-gray-500 w-16">Pos</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Titolo</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Dominio</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Keyword</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Fonte</th>
						<th class="text-left px-4 py-3 font-medium text-gray-500">Sentiment</th>
					</tr>
				</thead>
				<tbody>
					{#each filteredItems as item}
						<tr class="border-b border-gray-50 hover:bg-gray-50 transition-colors {item.isCompetitor ? 'bg-orange-50/50' : ''}">
							<td class="px-4 py-3 font-mono text-gray-900 font-medium">{item.position}</td>
							<td class="px-4 py-3">
								<a href={item.url} target="_blank" rel="noopener" class="text-gray-900 hover:text-blue-600 line-clamp-1">
									{item.title}
								</a>
								{#if item.isCompetitor}
									<span class="ml-2 text-[10px] font-medium uppercase text-orange-600 bg-orange-100 px-1.5 py-0.5 rounded">Competitor</span>
								{/if}
							</td>
							<td class="px-4 py-3 text-gray-500">{item.domain}</td>
							<td class="px-4 py-3 text-gray-500">{item.keyword}</td>
							<td class="px-4 py-3">
								<span class="text-xs font-medium text-gray-500">{sourceLabel(item.source)}</span>
							</td>
							<td class="px-4 py-3">
								{#if item.aiAnalysis}
									<span class="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium {sentimentColor(item.aiAnalysis.sentiment)}">
										{sentimentLabel(item.aiAnalysis.sentiment)}
									</span>
								{:else}
									<span class="text-xs text-gray-400">--</span>
								{/if}
							</td>
						</tr>
					{:else}
						<tr>
							<td colspan="6" class="px-4 py-12 text-center text-gray-400">Nessun risultato trovato</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>

	<!-- Pagination -->
	{#if totalPages > 1}
		<div class="flex items-center justify-between">
			<p class="text-sm text-gray-500">
				{data.results.total} risultati totali
			</p>
			<div class="flex items-center gap-1">
				<button
					onclick={() => goToPage(currentPage - 1)}
					disabled={currentPage <= 1}
					class="px-3 py-1.5 text-sm rounded-lg border border-gray-200 text-gray-600 hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed"
				>
					Precedente
				</button>
				<span class="px-3 py-1.5 text-sm text-gray-500">
					{currentPage} / {totalPages}
				</span>
				<button
					onclick={() => goToPage(currentPage + 1)}
					disabled={currentPage >= totalPages}
					class="px-3 py-1.5 text-sm rounded-lg border border-gray-200 text-gray-600 hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed"
				>
					Successiva
				</button>
			</div>
		</div>
	{/if}
</div>
