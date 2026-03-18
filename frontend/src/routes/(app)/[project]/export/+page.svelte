<script lang="ts">
	import type { PageData } from './$types.js';

	let { data }: { data: PageData } = $props();

	let sections = $state(data.reportSections.map(s => ({ ...s })));
</script>

<div class="space-y-6">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Export</h2>
		<p class="text-sm text-gray-500 mt-1">Genera e scarica report personalizzati</p>
	</div>

	<div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
		<!-- Settings -->
		<div class="lg:col-span-1 space-y-6">
			<!-- Format -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Report Settimanale</h3>
				<p class="text-xs text-gray-500 mb-4">Seleziona il formato di esportazione</p>
				<div class="flex gap-3">
					<button disabled class="flex-1 px-4 py-3 border border-gray-200 rounded-lg text-sm font-medium text-gray-700 bg-gray-50 opacity-50 cursor-not-allowed flex items-center justify-center gap-2">
						<svg class="w-5 h-5 text-emerald-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
							<path stroke-linecap="round" stroke-linejoin="round" d="M3 10h18M3 14h18m-9-4v8m-7 0h14a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v8a2 2 0 002 2z" />
						</svg>
						Excel
					</button>
					<button disabled class="flex-1 px-4 py-3 border border-gray-200 rounded-lg text-sm font-medium text-gray-700 bg-gray-50 opacity-50 cursor-not-allowed flex items-center justify-center gap-2">
						<svg class="w-5 h-5 text-red-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
							<path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
						</svg>
						PDF
					</button>
				</div>
			</div>

			<!-- Period -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Periodo</h3>
				<div class="space-y-3">
					<div>
						<label class="text-xs text-gray-500 mb-1 block">Data inizio</label>
						<input type="date" value="2026-03-11" disabled class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm text-gray-700 bg-gray-50 opacity-70 cursor-not-allowed" />
					</div>
					<div>
						<label class="text-xs text-gray-500 mb-1 block">Data fine</label>
						<input type="date" value="2026-03-18" disabled class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm text-gray-700 bg-gray-50 opacity-70 cursor-not-allowed" />
					</div>
				</div>
			</div>

			<!-- Content Checklist -->
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
				<h3 class="text-sm font-semibold text-gray-900 mb-4">Contenuto Report</h3>
				<div class="space-y-3">
					{#each sections as section, i}
						<label class="flex items-center gap-3 cursor-pointer">
							<input
								type="checkbox"
								bind:checked={sections[i].checked}
								class="w-4 h-4 rounded border-gray-300 text-gray-900 focus:ring-gray-500"
							/>
							<span class="text-sm text-gray-700">{section.label}</span>
						</label>
					{/each}
				</div>
			</div>

			<!-- Generate Button -->
			<button disabled class="w-full px-4 py-3 bg-gray-900 text-white text-sm font-medium rounded-lg opacity-50 cursor-not-allowed">
				Genera Report
			</button>
		</div>

		<!-- Preview -->
		<div class="lg:col-span-2">
			<div class="bg-white rounded-xl shadow-sm border border-gray-100 p-8">
				<div class="flex items-center justify-between mb-6 pb-6 border-b border-gray-100">
					<div>
						<h3 class="text-lg font-semibold text-gray-900">{data.preview.title}</h3>
						<p class="text-sm text-gray-500 mt-1">Periodo: {data.preview.period}</p>
					</div>
					<div class="text-right">
						<p class="text-xs text-gray-400">Anteprima report</p>
						<p class="text-xs text-gray-400 mt-0.5">Formato: PDF</p>
					</div>
				</div>

				<div class="prose prose-sm max-w-none text-gray-600 leading-relaxed whitespace-pre-line">
					{data.preview.summary}
				</div>

				<div class="mt-8 pt-6 border-t border-gray-100">
					<p class="text-xs text-gray-400">Sezioni incluse nel report:</p>
					<div class="flex flex-wrap gap-2 mt-2">
						{#each sections.filter(s => s.checked) as section}
							<span class="inline-flex items-center px-2.5 py-1 rounded-lg text-xs font-medium bg-gray-100 text-gray-700">
								{section.label}
							</span>
						{/each}
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
