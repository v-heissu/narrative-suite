import { getLatestScan, getMetrics } from '$lib/api/client.js';
import type { PageLoad } from './$types.js';

export const load: PageLoad = async ({ params, fetch }) => {
	try {
		const [scan, metrics] = await Promise.all([
			getLatestScan(params.project, fetch),
			getMetrics(params.project, fetch)
		]);

		const highlights = [
			'La keyword "pneumatici auto elettriche" ha guadagnato 4 posizioni raggiungendo la seconda posizione SERP',
			'Michelin ha pubblicato una nuova campagna sulla sostenibilita che sta erodendo la visibilita Pirelli su topic ESG',
			'Il Trend Acceleration Index per "ADAS e pneumatici smart" ha raggiunto +420%, segnalando un\'opportunita di primo presidio',
			'La visibilita AI su Perplexity e aumentata del 18% grazie ai nuovi contenuti tecnici pubblicati',
			'Continental ha superato Pirelli sulla keyword "pneumatici SUV" passando dalla 3a alla 1a posizione'
		];

		const actions = [
			{ priority: 'Alta', text: 'Pubblicare entro 48h un contenuto pillar su "pneumatici per auto elettriche" per consolidare la posizione #2 e puntare alla #1' },
			{ priority: 'Alta', text: 'Sviluppare una narrativa di sostenibilita forte per contrastare la campagna Michelin - focus su materiali riciclati e obiettivi 2030' },
			{ priority: 'Media', text: 'Creare un content brief tecnico su "ADAS e pneumatici smart" per presidiare il trend emergente prima dei competitor' },
			{ priority: 'Media', text: 'Ottimizzare la pagina "pneumatici SUV" con dati comparativi e recensioni per recuperare la posizione #1 su Continental' }
		];

		const previousMetrics = {
			nsov: { previous: 30.2, current: 32.4 },
			tos: { previous: 7.1, current: 7.8 },
			nri: { previous: 3.8, current: 3.2 },
			tai: { previous: 4.1, current: 5.6 }
		};

		return { scan, metrics, highlights, actions, previousMetrics };
	} catch {
		return {
			scan: null,
			metrics: [],
			highlights: [],
			actions: [],
			previousMetrics: {
				nsov: { previous: 0, current: 0 },
				tos: { previous: 0, current: 0 },
				nri: { previous: 0, current: 0 },
				tai: { previous: 0, current: 0 }
			}
		};
	}
};
