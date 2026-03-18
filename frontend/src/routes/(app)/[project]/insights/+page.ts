import { getInsights } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, fetch }) => {
	try {
		const insights = await getInsights(params.project, fetch);
		return { insights };
	} catch {
		return { insights: [] };
	}
};
