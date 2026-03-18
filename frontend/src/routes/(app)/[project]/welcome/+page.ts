import { getMetrics, getLatestScan, getInsights } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, fetch }) => {
	try {
		const [metrics, scan, insights] = await Promise.all([
			getMetrics(params.project, fetch),
			getLatestScan(params.project, fetch),
			getInsights(params.project, fetch)
		]);
		return { metrics, scan, insights };
	} catch {
		return { metrics: [], scan: null, insights: [] };
	}
};
