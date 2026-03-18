import type { PageLoad } from './$types.js';

export const load: PageLoad = async () => {
	const competitors = [
		{
			name: 'Michelin',
			nsov: 34.2,
			trend: 'up' as const,
			topKeywords: ['pneumatici premium', 'gomme estive', 'pneumatici SUV']
		},
		{
			name: 'Continental',
			nsov: 28.7,
			trend: 'up' as const,
			topKeywords: ['pneumatici invernali', 'sicurezza stradale', 'pneumatici all season']
		},
		{
			name: 'Bridgestone',
			nsov: 25.1,
			trend: 'down' as const,
			topKeywords: ['gomme auto', 'pneumatici economici', 'durata pneumatici']
		},
		{
			name: 'Goodyear',
			nsov: 19.8,
			trend: 'down' as const,
			topKeywords: ['pneumatici 4 stagioni', 'gomme SUV', 'pneumatici online']
		},
		{
			name: 'Hankook',
			nsov: 14.3,
			trend: 'up' as const,
			topKeywords: ['pneumatici coreani', 'gomme sportive', 'rapporto qualita prezzo']
		}
	];

	const keywords = [
		'pneumatici invernali',
		'gomme sportive',
		'pneumatici SUV',
		'gomme 4 stagioni',
		'pneumatici premium',
		'pneumatici auto elettriche',
		'gomme estive',
		'sicurezza pneumatici',
		'pneumatici online',
		'pneumatici run flat'
	];

	const visibilityData = keywords.map((kw) => ({
		keyword: kw,
		positions: {
			'Pirelli': Math.floor(Math.random() * 10) + 1,
			'Michelin': Math.floor(Math.random() * 12) + 1,
			'Continental': Math.floor(Math.random() * 12) + 1,
			'Bridgestone': Math.floor(Math.random() * 14) + 1,
			'Goodyear': Math.floor(Math.random() * 15) + 1,
			'Hankook': Math.floor(Math.random() * 18) + 1
		} as Record<string, number>
	}));

	// Use fixed realistic data instead of random
	const fixedData = [
		{ keyword: 'pneumatici invernali', positions: { Pirelli: 2, Michelin: 1, Continental: 3, Bridgestone: 5, Goodyear: 7, Hankook: 11 } },
		{ keyword: 'gomme sportive', positions: { Pirelli: 1, Michelin: 3, Continental: 5, Bridgestone: 4, Goodyear: 9, Hankook: 8 } },
		{ keyword: 'pneumatici SUV', positions: { Pirelli: 3, Michelin: 2, Continental: 1, Bridgestone: 6, Goodyear: 4, Hankook: 10 } },
		{ keyword: 'gomme 4 stagioni', positions: { Pirelli: 4, Michelin: 1, Continental: 2, Bridgestone: 3, Goodyear: 5, Hankook: 7 } },
		{ keyword: 'pneumatici premium', positions: { Pirelli: 1, Michelin: 2, Continental: 4, Bridgestone: 6, Goodyear: 8, Hankook: 12 } },
		{ keyword: 'pneumatici auto elettriche', positions: { Pirelli: 2, Michelin: 4, Continental: 3, Bridgestone: 7, Goodyear: 6, Hankook: 9 } },
		{ keyword: 'gomme estive', positions: { Pirelli: 3, Michelin: 1, Continental: 2, Bridgestone: 5, Goodyear: 6, Hankook: 8 } },
		{ keyword: 'sicurezza pneumatici', positions: { Pirelli: 5, Michelin: 2, Continental: 1, Bridgestone: 3, Goodyear: 4, Hankook: 14 } },
		{ keyword: 'pneumatici online', positions: { Pirelli: 6, Michelin: 3, Continental: 4, Bridgestone: 2, Goodyear: 1, Hankook: 5 } },
		{ keyword: 'pneumatici run flat', positions: { Pirelli: 1, Michelin: 4, Continental: 3, Bridgestone: 2, Goodyear: 7, Hankook: 9 } }
	];

	return { competitors, visibilityData: fixedData };
};
