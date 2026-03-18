export interface Project {
	id: number;
	slug: string;
	name: string;
	industry: string;
	brandName: string;
	isActive: boolean;
}

export interface Scan {
	id: number;
	status: string;
	aiBriefing: string | null;
	completedAt: string | null;
	totalTasks: number;
	completedTasks: number;
}

export interface Metric {
	metricType: string;
	value: number;
	breakdown: Record<string, unknown> | null;
	calculatedAt: string;
}

export interface AiAnalysis {
	themes: string[];
	sentiment: string;
	sentimentScore: number;
	entities: string[];
	summary: string;
}

export interface SerpResult {
	keyword: string;
	source: string;
	position: number;
	title: string;
	url: string;
	domain: string;
	isCompetitor: boolean;
	snippet: string;
	aiAnalysis: AiAnalysis | null;
}

export interface PaginatedResults {
	items: SerpResult[];
	total: number;
	page: number;
	pageSize: number;
}

export interface AiVisibility {
	keyword: string;
	llmModel: string;
	brandMentioned: boolean;
	visibilityScore: number;
	responseText: string;
	competitorMentions: string[];
}

export interface Insight {
	type: string;
	title: string;
	description: string;
	score: number;
	status: string;
	dataEvidence: Record<string, unknown> | null;
}
