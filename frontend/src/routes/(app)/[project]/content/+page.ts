import type { PageLoad } from './$types.js';

export const load: PageLoad = async () => {
	const briefs = [
		{
			id: 1,
			title: 'Guida completa pneumatici 4 stagioni 2026',
			keyword: 'pneumatici 4 stagioni',
			status: 'In Lavorazione',
			serpContext: [
				{ title: 'Migliori pneumatici 4 stagioni 2026 - Altroconsumo', domain: 'altroconsumo.it' },
				{ title: 'Pneumatici all season: quando convengono davvero', domain: 'automobile.it' },
				{ title: 'Test pneumatici 4 stagioni: la classifica', domain: 'alvolante.it' }
			],
			structure: {
				h1: 'Pneumatici 4 stagioni 2026: guida completa alla scelta',
				h2s: [
					'Cosa sono i pneumatici 4 stagioni e come funzionano',
					'Vantaggi e limiti rispetto a invernali ed estivi',
					'I migliori pneumatici 4 stagioni Pirelli: gamma Cinturato All Season',
					'Come scegliere la misura corretta per la propria auto',
					'Normativa e marcature: M+S e 3PMSF',
					'Quando conviene scegliere un pneumatico 4 stagioni'
				]
			}
		},
		{
			id: 2,
			title: 'Pneumatici per auto elettriche: tutto quello che devi sapere',
			keyword: 'pneumatici auto elettriche',
			status: 'Nuovo',
			serpContext: [
				{ title: 'Pneumatici per EV: perche sono diversi - Motor1', domain: 'motor1.com' },
				{ title: 'Le migliori gomme per auto elettriche 2026', domain: 'vaielettrico.it' },
				{ title: 'Pneumatici EV: resistenza al rotolamento e autonomia', domain: 'quattroruote.it' }
			],
			structure: {
				h1: 'Pneumatici per auto elettriche: guida alla scelta 2026',
				h2s: [
					'Perche le auto elettriche hanno bisogno di pneumatici specifici',
					'Peso, coppia e silenziosita: le sfide tecniche',
					'Pirelli Elect: la tecnologia dedicata ai veicoli elettrici',
					'Impatto dei pneumatici sull\'autonomia: dati reali',
					'Confronto tra i principali pneumatici EV sul mercato',
					'Consigli per la manutenzione e la longevita'
				]
			}
		},
		{
			id: 3,
			title: 'Pirelli e la sostenibilita: materiali riciclati e obiettivi 2030',
			keyword: 'pneumatici sostenibili pirelli',
			status: 'Completato',
			serpContext: [
				{ title: 'Sostenibilita Michelin: il piano 2030 - Michelin', domain: 'michelin.it' },
				{ title: 'Pneumatici green: la sfida della sostenibilita', domain: 'rinnovabili.it' },
				{ title: 'Gomme riciclate: a che punto siamo', domain: 'repubblica.it' }
			],
			structure: {
				h1: 'Pirelli e la sostenibilita: la roadmap verso il 2030',
				h2s: [
					'L\'impegno Pirelli per la sostenibilita ambientale',
					'Materiali riciclati e bio-based: lo stato dell\'arte',
					'Il primo pneumatico con il 40% di materiali sostenibili',
					'Economia circolare: il programma di ritiro e riciclo',
					'Confronto con i competitor: dove si posiziona Pirelli',
					'Obiettivi 2030: carbon neutrality e oltre'
				]
			}
		},
		{
			id: 4,
			title: 'ADAS e pneumatici intelligenti: il futuro della sicurezza',
			keyword: 'pneumatici smart ADAS',
			status: 'Nuovo',
			serpContext: [
				{ title: 'Pneumatici connessi: il futuro e gia qui - Wired', domain: 'wired.it' },
				{ title: 'Cyber Tyre Pirelli: il pneumatico che comunica', domain: 'pirelli.com' },
				{ title: 'Sensori nei pneumatici: come funzionano', domain: 'sicurauto.it' }
			],
			structure: {
				h1: 'Pneumatici intelligenti e ADAS: come cambiera la sicurezza stradale',
				h2s: [
					'Cosa sono i pneumatici smart e come funzionano',
					'L\'integrazione tra pneumatici e sistemi ADAS',
					'Pirelli Cyber Tyre: la piattaforma di sensoristica avanzata',
					'Dati in tempo reale: pressione, temperatura e aderenza',
					'Impatto sulla sicurezza: i numeri della ricerca',
					'Prospettive future: dal pneumatico al veicolo autonomo'
				]
			}
		}
	];

	return { briefs };
};
