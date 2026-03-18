import { getAiVisibility } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, fetch }) => {
	try {
		const visibility = await getAiVisibility(params.project, fetch);
		return { visibility };
	} catch {
		return { visibility: [] };
	}
};
