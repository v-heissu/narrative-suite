import { getResults } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, url, fetch }) => {
	const page = parseInt(url.searchParams.get('page') ?? '1', 10);
	const pageSize = parseInt(url.searchParams.get('pageSize') ?? '20', 10);

	try {
		const results = await getResults(params.project, page, pageSize, fetch);
		return { results };
	} catch {
		return { results: { items: [], total: 0, page: 1, pageSize: 20 } };
	}
};
