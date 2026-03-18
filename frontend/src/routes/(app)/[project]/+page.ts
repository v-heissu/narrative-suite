import { getMetrics, getLatestScan } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, fetch }) => {
	try {
		const [metrics, scan] = await Promise.all([
			getMetrics(params.project, fetch),
			getLatestScan(params.project, fetch)
		]);
		return { metrics, scan };
	} catch {
		return { metrics: [], scan: null };
	}
};
