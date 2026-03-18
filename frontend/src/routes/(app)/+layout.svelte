<script lang="ts">
	import '../../app.css';
	import type { LayoutData } from './$types.js';
	import { page } from '$app/stores';

	let { data, children }: { data: LayoutData; children: any } = $props();

	let currentPath = $derived($page.url.pathname);
	let projectSlug = $derived($page.params.project ?? data.projects[0]?.slug ?? 'pirelli');
	let currentProject = $derived(
		data.projects.find((p) => p.slug === projectSlug) ?? data.projects[0]
	);

	const navItems = [
		{ label: 'Dashboard', href: '', icon: 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6' },
		{ label: 'Monitor', href: '/monitor', icon: 'M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z' },
		{ label: 'AI Visibility', href: '/ai-visibility', icon: 'M15 12a3 3 0 11-6 0 3 3 0 016 0z M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z' },
		{ label: 'Insight & Opportunita', href: '/insights', icon: 'M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z' }
	];

	function isActive(href: string): boolean {
		const fullPath = `/${projectSlug}${href}`;
		if (href === '') return currentPath === `/${projectSlug}` || currentPath === `/${projectSlug}/`;
		return currentPath.startsWith(fullPath);
	}
</script>

<svelte:head>
	<title>Narrative Intelligence Suite</title>
</svelte:head>

<div class="flex h-screen overflow-hidden">
	<!-- Sidebar -->
	<aside class="w-64 bg-gray-950 text-white flex flex-col shrink-0">
		<div class="p-6 border-b border-gray-800">
			<h1 class="text-lg font-semibold tracking-tight">Narrative Suite</h1>
			<p class="text-xs text-gray-500 mt-1">Intelligence Platform</p>
		</div>

		<nav class="flex-1 p-4 space-y-1">
			{#each navItems as item}
				<a
					href="/{projectSlug}{item.href}"
					class="flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm transition-colors {isActive(item.href)
						? 'bg-gray-800 text-white font-medium'
						: 'text-gray-400 hover:text-white hover:bg-gray-900'}"
				>
					<svg class="w-5 h-5 shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
						<path stroke-linecap="round" stroke-linejoin="round" d={item.icon} />
					</svg>
					{item.label}
				</a>
			{/each}
		</nav>

		<div class="p-4 border-t border-gray-800">
			<div class="flex items-center gap-3 px-3 py-2">
				<div class="w-8 h-8 rounded-full bg-gray-700 flex items-center justify-center text-xs font-medium">
					{currentProject?.brandName?.charAt(0) ?? 'P'}
				</div>
				<div>
					<p class="text-sm font-medium">{currentProject?.brandName ?? 'Progetto'}</p>
					<p class="text-xs text-gray-500">{currentProject?.industry ?? ''}</p>
				</div>
			</div>
		</div>
	</aside>

	<!-- Main -->
	<div class="flex-1 flex flex-col overflow-hidden">
		<!-- Top bar -->
		<header class="h-14 bg-white border-b border-gray-200 flex items-center px-6 shrink-0">
			<nav class="flex items-center gap-2 text-sm text-gray-500">
				<span>{currentProject?.brandName ?? 'Progetto'}</span>
				<span class="text-gray-300">/</span>
				<span class="text-gray-900 font-medium">
					{#if currentPath.includes('/monitor')}
						Monitor
					{:else if currentPath.includes('/ai-visibility')}
						AI Visibility
					{:else if currentPath.includes('/insights')}
						Insight & Opportunita
					{:else}
						Dashboard
					{/if}
				</span>
			</nav>
		</header>

		<!-- Content -->
		<main class="flex-1 overflow-auto bg-gray-50 p-6">
			{@render children()}
		</main>
	</div>
</div>
