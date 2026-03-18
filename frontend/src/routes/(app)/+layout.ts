import { getProjects } from '$lib/api/client.js';
import type { LayoutLoad } from './$types.js';

export const load: LayoutLoad = async ({ fetch }) => {
	try {
		const projects = await getProjects(fetch);
		return { projects };
	} catch {
		return { projects: [] };
	}
};
