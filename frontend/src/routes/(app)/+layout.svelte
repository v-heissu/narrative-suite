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

	interface NavItem {
		label: string;
		href: string;
		icon: string;
	}

	interface NavSection {
		title: string;
		items: NavItem[];
	}

	const navSections: NavSection[] = [
		{
			title: 'Overview',
			items: [
				{ label: 'Dashboard', href: '', icon: 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6' }
			]
		},
		{
			title: 'Understand',
			items: [
				{ label: 'Topic Explorer', href: '/topics', icon: 'M4 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2V6zm10 0a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V6zM4 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2v-2zm10 0a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z' },
				{ label: 'Monitor', href: '/monitor', icon: 'M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z' },
				{ label: 'Visibilita AI', href: '/ai-visibility', icon: 'M15 12a3 3 0 11-6 0 3 3 0 016 0z M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z' },
				{ label: 'Competitor', href: '/competitor', icon: 'M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z' },
				{ label: 'Trend Radar', href: '/trends', icon: 'M13 7h8m0 0v8m0-8l-8 8-4-4-6 6' }
			]
		},
		{
			title: 'Decide',
			items: [
				{ label: 'Insight & Opportunita', href: '/insights', icon: 'M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z' },
				{ label: 'Briefing Executive', href: '/briefing', icon: 'M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z' },
				{ label: 'Piano Editoriale', href: '/editorial', icon: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-3 7h3m-3 4h3m-6-4h.01M9 16h.01' }
			]
		},
		{
			title: 'Create',
			items: [
				{ label: 'Content Brief', href: '/content', icon: 'M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z' },
				{ label: 'Editor', href: '/editor', icon: 'M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z' },
				{ label: 'Export', href: '/export', icon: 'M12 10v6m0 0l-3-3m3 3l3-3m2 8H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z' }
			]
		}
	];

	const breadcrumbMap: Record<string, string> = {
		'/topics': 'Topic Explorer',
		'/monitor': 'Monitor',
		'/ai-visibility': 'Visibilita AI',
		'/competitor': 'Competitor',
		'/trends': 'Trend Radar',
		'/insights': 'Insight & Opportunita',
		'/briefing': 'Briefing Executive',
		'/editorial': 'Piano Editoriale',
		'/content': 'Content Brief',
		'/editor': 'Editor',
		'/export': 'Export',
		'/welcome': 'Tour Guidato'
	};

	let currentPageLabel = $derived(() => {
		for (const [path, label] of Object.entries(breadcrumbMap)) {
			if (currentPath.includes(path)) return label;
		}
		return 'Dashboard';
	});

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
	<aside class="w-64 bg-gradient-to-b from-gray-950 via-gray-900 to-gray-950 text-white flex flex-col shrink-0 border-r border-white/5">
		<div class="p-6 border-b border-white/10">
			<div class="flex items-center gap-3">
				<img src="/icons/icon-512.svg" alt="Logo" class="w-8 h-8 rounded-lg" />
				<div>
					<h1 class="text-lg font-semibold tracking-tight text-brand-gradient">Narrative Suite</h1>
					<p class="text-[10px] text-gray-500 mt-0.5 uppercase tracking-widest">Intelligence Platform</p>
				</div>
			</div>
		</div>

		<nav class="flex-1 px-3 py-4 space-y-5 overflow-y-auto">
			{#each navSections as section, sIdx}
				<div class="animate-slide-in-left" style="animation-delay: {sIdx * 80}ms; opacity: 0;">
					<div class="flex items-center gap-2 px-3 mb-2">
						<p class="text-[10px] font-semibold uppercase tracking-widest text-gray-500">
							{section.title}
						</p>
						<div class="flex-1 h-px bg-gradient-to-r from-gray-700/50 to-transparent"></div>
					</div>
					<div class="space-y-0.5">
						{#each section.items as item}
							<a
								href="/{projectSlug}{item.href}"
								class="relative flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition-all duration-200
									{isActive(item.href)
									? 'bg-gradient-to-r from-blue-500/10 to-transparent text-white font-medium'
									: 'text-gray-400 hover:text-white hover:bg-white/5'}"
							>
								{#if isActive(item.href)}
									<div class="absolute left-0 top-1/2 -translate-y-1/2 w-[3px] h-5 bg-blue-500 rounded-r-full"></div>
								{/if}
								<svg class="w-5 h-5 shrink-0 {isActive(item.href) ? 'text-blue-400' : ''}" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
									<path stroke-linecap="round" stroke-linejoin="round" d={item.icon} />
								</svg>
								{item.label}
							</a>
						{/each}
					</div>
				</div>
			{/each}
		</nav>

		<!-- Tour link + Project info -->
		<div class="p-3 border-t border-white/10 space-y-3">
			<a
				href="/{projectSlug}/welcome"
				class="flex items-center gap-2 px-3 py-1.5 text-xs text-gray-500 hover:text-gray-300 transition-colors duration-200"
			>
				<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
					<path stroke-linecap="round" stroke-linejoin="round" d="M9.813 15.904L9 18.75l-.813-2.846a4.5 4.5 0 00-3.09-3.09L2.25 12l2.846-.813a4.5 4.5 0 003.09-3.09L9 5.25l.813 2.846a4.5 4.5 0 003.09 3.09L15.75 12l-2.846.813a4.5 4.5 0 00-3.09 3.09zM18.259 8.715L18 9.75l-.259-1.035a3.375 3.375 0 00-2.455-2.456L14.25 6l1.036-.259a3.375 3.375 0 002.455-2.456L18 2.25l.259 1.035a3.375 3.375 0 002.455 2.456L21.75 6l-1.036.259a3.375 3.375 0 00-2.455 2.456z" />
				</svg>
				Tour Guidato
			</a>

			<div class="glass rounded-xl p-3">
				<div class="flex items-center gap-3">
					<div class="w-8 h-8 rounded-full bg-gradient-to-br from-blue-500 to-violet-600 flex items-center justify-center text-xs font-bold text-white">
						{currentProject?.brandName?.charAt(0) ?? 'P'}
					</div>
					<div>
						<p class="text-sm font-medium text-white">{currentProject?.brandName ?? 'Progetto'}</p>
						<p class="text-[10px] text-gray-500">{currentProject?.industry ?? ''}</p>
					</div>
				</div>
				<p class="text-[10px] text-gray-600 mt-2 pl-11">Ultima scansione: 18 Mar 2026</p>
			</div>
		</div>
	</aside>

	<!-- Main -->
	<div class="flex-1 flex flex-col overflow-hidden">
		<!-- Top bar -->
		<header class="h-14 bg-white/80 backdrop-blur-sm border-b border-gray-200/80 flex items-center px-6 shrink-0">
			<nav class="flex items-center gap-2 text-sm text-gray-500">
				<span>{currentProject?.brandName ?? 'Progetto'}</span>
				<span class="text-gray-300">/</span>
				<span class="text-gray-900 font-medium">{currentPageLabel()}</span>
			</nav>
		</header>

		<!-- Content -->
		<main class="flex-1 overflow-auto bg-gray-50 p-6">
			{@render children()}
		</main>
	</div>
</div>
