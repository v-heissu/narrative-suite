<script lang="ts">
	import { goto } from '$app/navigation';
	import { onMount } from 'svelte';

	onMount(() => {
		goto('/pirelli');
	});
</script>

<div class="min-h-screen bg-gray-950 flex items-center justify-center">
	<p class="text-gray-400 text-sm">Caricamento...</p>
</div>
