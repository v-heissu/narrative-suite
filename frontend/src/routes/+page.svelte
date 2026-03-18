<script lang="ts">
	let health = $state<{ status: string; database: string; timestamp: string } | null>(null);
	let error = $state<string | null>(null);

	async function checkHealth() {
		try {
			const res = await fetch('/api/v1/health');
			health = await res.json();
			error = null;
		} catch (e) {
			error = 'Backend non raggiungibile';
			health = null;
		}
	}
</script>

<div class="min-h-screen bg-gray-950 text-white flex items-center justify-center">
	<div class="text-center space-y-6">
		<h1 class="text-4xl font-bold tracking-tight">Narrative Intelligence Suite</h1>
		<p class="text-gray-400">Piattaforma di intelligence narrativa</p>

		<button
			onclick={checkHealth}
			class="px-6 py-2 bg-white text-gray-950 rounded-lg font-medium hover:bg-gray-200 transition-colors"
		>
			Verifica Backend
		</button>

		{#if health}
			<div class="mt-4 p-4 bg-gray-900 rounded-lg text-sm text-left inline-block">
				<p>Status: <span class="text-green-400">{health.status}</span></p>
				<p>Database: <span class={health.database === 'connected' ? 'text-green-400' : 'text-yellow-400'}>{health.database}</span></p>
				<p class="text-gray-500">{health.timestamp}</p>
			</div>
		{/if}

		{#if error}
			<p class="text-red-400 mt-4">{error}</p>
		{/if}
	</div>
</div>
