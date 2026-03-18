<script lang="ts">
	import '../app.css';

	let { children } = $props();
</script>

<svelte:head>
	<title>Narrative Intelligence Suite</title>
</svelte:head>

{@render children()}
