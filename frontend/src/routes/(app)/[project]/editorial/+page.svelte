<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	function priorityClasses(priority: string): string {
		switch (priority) {
			case 'alta': return 'bg-red-100 text-red-800';
			case 'media': return 'bg-amber-100 text-amber-800';
			case 'bassa': return 'bg-gray-100 text-gray-600';
			default: return 'bg-gray-100 text-gray-600';
		}
	}

	function priorityLabel(priority: string): string {
		switch (priority) {
			case 'alta': return 'Alta';
			case 'media': return 'Media';
			case 'bassa': return 'Bassa';
			default: return priority;
		}
	}

	function columnHeaderColor(id: string): string {
		switch (id) {
			case 'idea': return 'bg-gray-200 text-gray-700';
			case 'briefed': return 'bg-blue-100 text-blue-800';
			case 'writing': return 'bg-amber-100 text-amber-800';
			case 'review': return 'bg-violet-100 text-violet-800';
			case 'published': return 'bg-emerald-100 text-emerald-800';
			default: return 'bg-gray-200 text-gray-700';
		}
	}

	function formatDate(dateStr: string): string {
		return new Date(dateStr).toLocaleDateString('it-IT', { day: 'numeric', month: 'short' });
	}
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Piano Editoriale</h2>
		<p class="text-sm text-gray-500 mt-1">Gestione dei contenuti in pipeline</p>
	</div>

	<div class="flex gap-4 overflow-x-auto pb-4">
		{#each data.columns as column}
			<div class="shrink-0 w-72">
				<div class="flex items-center justify-between mb-3">
					<span class="inline-flex items-center px-3 py-1 rounded-lg text-xs font-semibold uppercase tracking-wider {columnHeaderColor(column.id)}">
						{column.title}
					</span>
					<span class="text-xs text-gray-400 font-medium">{column.cards.length}</span>
				</div>
				<div class="space-y-3">
					{#each column.cards as card}
						<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-4 hover:shadow-md transition-shadow cursor-default">
							<h4 class="text-sm font-semibold text-gray-900 leading-snug mb-2">{card.title}</h4>
							<div class="flex items-center gap-1.5 mb-3">
								<svg class="w-3.5 h-3.5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
									<path stroke-linecap="round" stroke-linejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z" />
								</svg>
								<span class="text-xs text-gray-500">{card.keyword}</span>
							</div>
							<div class="flex items-center justify-between">
								<div class="flex items-center gap-2">
									<span class="inline-flex items-center px-1.5 py-0.5 rounded text-[10px] font-semibold uppercase {priorityClasses(card.priority)}">
										{priorityLabel(card.priority)}
									</span>
									<span class="text-xs text-gray-400">{formatDate(card.dueDate)}</span>
								</div>
								<div class="w-7 h-7 rounded-full bg-gray-700 text-white flex items-center justify-center text-[10px] font-semibold">
									{card.assignee}
								</div>
							</div>
						</div>
					{/each}
				</div>
			</div>
		{/each}
	</div>
</div>
