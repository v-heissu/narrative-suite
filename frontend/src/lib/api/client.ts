import type {
	Project,
	Scan,
	Metric,
	PaginatedResults,
	AiVisibility,
	Insight
} from '$lib/types/index.js';

const BASE = '/api/v1';

async function request<T>(path: string, fetchFn: typeof fetch = fetch): Promise<T> {
	const res = await fetchFn(`${BASE}${path}`);
	if (!res.ok) {
		throw new Error(`API error ${res.status}: ${res.statusText}`);
	}
	return res.json() as Promise<T>;
}

export function getProjects(fetchFn: typeof fetch = fetch) {
	return request<Project[]>('/projects', fetchFn);
}

export function getProject(slug: string, fetchFn: typeof fetch = fetch) {
	return request<Project>(`/projects/${slug}`, fetchFn);
}

export function getLatestScan(slug: string, fetchFn: typeof fetch = fetch) {
	return request<Scan>(`/projects/${slug}/scans/latest`, fetchFn);
}

export function getMetrics(slug: string, fetchFn: typeof fetch = fetch) {
	return request<Metric[]>(`/projects/${slug}/metrics`, fetchFn);
}

export function getResults(
	slug: string,
	page = 1,
	pageSize = 20,
	fetchFn: typeof fetch = fetch
) {
	return request<PaginatedResults>(
		`/projects/${slug}/results?page=${page}&pageSize=${pageSize}`,
		fetchFn
	);
}

export function getAiVisibility(slug: string, fetchFn: typeof fetch = fetch) {
	return request<AiVisibility[]>(`/projects/${slug}/ai-visibility`, fetchFn);
}

export function getInsights(slug: string, fetchFn: typeof fetch = fetch) {
	return request<Insight[]>(`/projects/${slug}/insights`, fetchFn);
}
