<script lang="ts">
	interface TopicData {
		name: string;
		mentions: number;
		sentiment: 'very_positive' | 'positive' | 'neutral' | 'mixed' | 'negative';
		trend: 'up_strong' | 'up' | 'stable' | 'down';
		isNew: boolean;
		overview: {
			totalMentions: number;
			avgSentiment: number;
			entities: string[];
			weeklyTrend: number[];
		};
		serp: {
			results: { position: number; title: string; domain: string; sentiment: string; isCompetitor: boolean }[];
			breakdown: { organic: number; news: number; aiOverview: number };
		};
		aiVisibility: {
			models: { name: string; mentionsPirelli: boolean; score: number; quote: string }[];
			overallScore: number;
		};
		competitor: {
			entries: { name: string; position: string; contentAngle: string; threatLevel: 'Alto' | 'Medio' | 'Basso' }[];
		};
	}

	const topics: TopicData[] = [
		{
			name: 'pneumatici invernali',
			mentions: 45,
			sentiment: 'positive',
			trend: 'stable',
			isNew: false,
			overview: {
				totalMentions: 45,
				avgSentiment: 7.2,
				entities: ['Pirelli', 'Michelin', 'Continental', 'Bridgestone', 'ADAC'],
				weeklyTrend: [38, 42, 40, 45]
			},
			serp: {
				results: [
					{ position: 1, title: 'Migliori pneumatici invernali 2026: la guida completa', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 2, title: 'Test pneumatici invernali ADAC', domain: 'adac.de', sentiment: 'neutral', isCompetitor: false },
					{ position: 3, title: 'Pneumatici invernali Michelin Alpin 7', domain: 'michelin.it', sentiment: 'positive', isCompetitor: true },
					{ position: 4, title: 'Confronto pneumatici invernali premium', domain: 'alvolante.it', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'Continental WinterContact TS 870', domain: 'continental.it', sentiment: 'positive', isCompetitor: true },
				],
				breakdown: { organic: 28, news: 12, aiOverview: 5 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 72, quote: 'Pirelli Winter Sottozero 3 e tra i migliori pneumatici invernali premium...' },
					{ name: 'Gemini', mentionsPirelli: false, score: 35, quote: 'Continental e Michelin guidano il segmento premium invernale...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 68, quote: 'Tra le opzioni premium, Pirelli e Michelin offrono le migliori prestazioni...' },
					{ name: 'Claude', mentionsPirelli: true, score: 65, quote: 'I pneumatici Pirelli Winter Sottozero sono rinomati per la tenuta su neve...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 28, quote: 'I migliori pneumatici invernali includono Michelin e Continental...' },
				],
				overallScore: 54
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Leader contenuti invernali', contentAngle: 'Sicurezza e certificazioni', threatLevel: 'Alto' },
					{ name: 'Continental', position: 'Forte nelle AI', contentAngle: 'Tecnologia e innovazione', threatLevel: 'Alto' },
					{ name: 'Bridgestone', position: 'Secondo livello', contentAngle: 'Rapporto qualita-prezzo', threatLevel: 'Medio' },
					{ name: 'Goodyear', position: 'Presenza limitata', contentAngle: 'Durata e resistenza', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'gomme auto sportive',
			mentions: 38,
			sentiment: 'very_positive',
			trend: 'up',
			isNew: false,
			overview: {
				totalMentions: 38,
				avgSentiment: 8.5,
				entities: ['Pirelli', 'P Zero', 'Michelin Pilot Sport', 'Ferrari', 'Lamborghini'],
				weeklyTrend: [30, 33, 35, 38]
			},
			serp: {
				results: [
					{ position: 1, title: 'Pirelli P Zero: il pneumatico delle supercar', domain: 'pirelli.com', sentiment: 'very_positive', isCompetitor: false },
					{ position: 2, title: 'Migliori gomme sportive 2026', domain: 'motorsport.com', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'Michelin Pilot Sport 5 recensione', domain: 'michelin.it', sentiment: 'positive', isCompetitor: true },
					{ position: 4, title: 'P Zero vs Pilot Sport: confronto', domain: 'quattroruote.it', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'Continental SportContact 7', domain: 'continental.it', sentiment: 'positive', isCompetitor: true },
				],
				breakdown: { organic: 22, news: 10, aiOverview: 6 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 88, quote: 'Pirelli P Zero e il punto di riferimento per le auto sportive di alta gamma...' },
					{ name: 'Gemini', mentionsPirelli: true, score: 75, quote: 'Per le supercar, Pirelli P Zero resta la scelta di riferimento...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 82, quote: 'Pirelli domina il segmento ultra high performance con il P Zero...' },
					{ name: 'Claude', mentionsPirelli: true, score: 79, quote: 'Il Pirelli P Zero e ampiamente considerato il benchmark nelle gomme sportive...' },
					{ name: 'Copilot', mentionsPirelli: true, score: 70, quote: 'Pirelli P Zero e Michelin Pilot Sport sono i leader del segmento...' },
				],
				overallScore: 79
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Principale sfidante', contentAngle: 'Performance versatili', threatLevel: 'Medio' },
					{ name: 'Continental', position: 'Terza posizione', contentAngle: 'Sicurezza sportiva', threatLevel: 'Basso' },
					{ name: 'Bridgestone', position: 'Presenza di nicchia', contentAngle: 'Potenza RE', threatLevel: 'Basso' },
					{ name: 'Goodyear', position: 'Presenza marginale', contentAngle: 'Eagle F1', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'pirelli p zero',
			mentions: 52,
			sentiment: 'very_positive',
			trend: 'up',
			isNew: false,
			overview: {
				totalMentions: 52,
				avgSentiment: 9.0,
				entities: ['Pirelli', 'P Zero', 'Ferrari', 'Lamborghini', 'Formula 1'],
				weeklyTrend: [44, 46, 50, 52]
			},
			serp: {
				results: [
					{ position: 1, title: 'Pirelli P Zero - Pneumatico Ultra High Performance', domain: 'pirelli.com', sentiment: 'very_positive', isCompetitor: false },
					{ position: 2, title: 'Pirelli P Zero recensione completa', domain: 'quattroruote.it', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'P Zero: dalla Formula 1 alla strada', domain: 'motorsport.com', sentiment: 'very_positive', isCompetitor: false },
					{ position: 4, title: 'Test Pirelli P Zero vs Michelin Pilot', domain: 'alvolante.it', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'Pirelli P Zero Elect per auto elettriche', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
				],
				breakdown: { organic: 35, news: 12, aiOverview: 5 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 92, quote: 'Il Pirelli P Zero e il pneumatico di riferimento per le supercar...' },
					{ name: 'Gemini', mentionsPirelli: true, score: 85, quote: 'P Zero e sinonimo di prestazioni estreme e tecnologia F1...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 88, quote: 'Il P Zero di Pirelli equipaggia la maggior parte delle supercar in primo equipaggiamento...' },
					{ name: 'Claude', mentionsPirelli: true, score: 86, quote: 'Pirelli P Zero rappresenta il vertice della tecnologia pneumatica stradale...' },
					{ name: 'Copilot', mentionsPirelli: true, score: 80, quote: 'Il P Zero e il pneumatico piu iconico di Pirelli, nato dalla F1...' },
				],
				overallScore: 86
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Sfidante con Pilot Sport', contentAngle: 'Performance equilibrate', threatLevel: 'Medio' },
					{ name: 'Continental', position: 'Nicchia premium', contentAngle: 'SportContact', threatLevel: 'Basso' },
					{ name: 'Bridgestone', position: 'Marginale', contentAngle: 'Potenza Sport', threatLevel: 'Basso' },
					{ name: 'Goodyear', position: 'Non rilevante', contentAngle: 'Eagle F1 Asymmetric', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'pneumatici 4 stagioni',
			mentions: 28,
			sentiment: 'neutral',
			trend: 'up_strong',
			isNew: false,
			overview: {
				totalMentions: 28,
				avgSentiment: 5.8,
				entities: ['Michelin', 'Continental', 'Pirelli', 'Goodyear', 'Vredestein'],
				weeklyTrend: [18, 20, 24, 28]
			},
			serp: {
				results: [
					{ position: 1, title: 'Migliori pneumatici 4 stagioni 2026', domain: 'alvolante.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 2, title: 'Michelin CrossClimate 2: re delle 4 stagioni', domain: 'michelin.it', sentiment: 'positive', isCompetitor: true },
					{ position: 3, title: 'Test 4 stagioni: quale scegliere', domain: 'quattroruote.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 4, title: 'Pirelli Cinturato All Season SF3', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'Continental AllSeasonContact 2', domain: 'continental.it', sentiment: 'positive', isCompetitor: true },
				],
				breakdown: { organic: 18, news: 6, aiOverview: 4 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: false, score: 30, quote: 'Michelin CrossClimate 2 e il migliore pneumatico 4 stagioni sul mercato...' },
					{ name: 'Gemini', mentionsPirelli: false, score: 25, quote: 'Per le 4 stagioni, Michelin e Continental dominano il segmento...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 45, quote: 'Tra le opzioni all season, Pirelli Cinturato e una valida alternativa...' },
					{ name: 'Claude', mentionsPirelli: false, score: 32, quote: 'Il segmento all-season e dominato da Michelin CrossClimate...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 20, quote: 'I migliori 4 stagioni sono Michelin, Continental e Goodyear...' },
				],
				overallScore: 30
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Leader indiscusso', contentAngle: 'CrossClimate come riferimento', threatLevel: 'Alto' },
					{ name: 'Continental', position: 'Forte secondo', contentAngle: 'AllSeasonContact 2', threatLevel: 'Alto' },
					{ name: 'Goodyear', position: 'Terzo player', contentAngle: 'Vector 4Seasons', threatLevel: 'Medio' },
					{ name: 'Vredestein', position: 'Nicchia premium', contentAngle: 'Quatrac Pro', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'sostenibilita pneumatici',
			mentions: 18,
			sentiment: 'positive',
			trend: 'up_strong',
			isNew: true,
			overview: {
				totalMentions: 18,
				avgSentiment: 6.5,
				entities: ['Michelin', 'Pirelli', 'Continental', 'FSC', 'ISCC'],
				weeklyTrend: [8, 11, 14, 18]
			},
			serp: {
				results: [
					{ position: 1, title: 'Michelin e la strategia sostenibile 2030', domain: 'michelin.it', sentiment: 'very_positive', isCompetitor: true },
					{ position: 2, title: 'Pneumatici sostenibili: cosa sapere', domain: 'greenme.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 3, title: 'Continental punta sul riciclo totale', domain: 'continental.it', sentiment: 'positive', isCompetitor: true },
					{ position: 4, title: 'Pirelli certificazione FSC', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'Impatto ambientale dei pneumatici', domain: 'repubblica.it', sentiment: 'negative', isCompetitor: false },
				],
				breakdown: { organic: 10, news: 6, aiOverview: 2 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: false, score: 22, quote: 'Michelin guida l\'innovazione sostenibile nei pneumatici...' },
					{ name: 'Gemini', mentionsPirelli: false, score: 18, quote: 'Continental e Michelin sono i leader nella sostenibilita...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 40, quote: 'Pirelli ha ottenuto la certificazione FSC per la gomma naturale...' },
					{ name: 'Claude', mentionsPirelli: false, score: 25, quote: 'Il settore e guidato da Michelin con il target 100% sostenibile...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 15, quote: 'La sostenibilita nei pneumatici e un tema emergente...' },
				],
				overallScore: 24
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Leader narrativo', contentAngle: 'Strategia 2030 e materiali bio', threatLevel: 'Alto' },
					{ name: 'Continental', position: 'Forte challenger', contentAngle: 'Riciclo e economia circolare', threatLevel: 'Alto' },
					{ name: 'Bridgestone', position: 'In crescita', contentAngle: 'E8 Commitment', threatLevel: 'Medio' },
					{ name: 'Goodyear', position: 'Emergente', contentAngle: 'Soybean oil technology', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'pneumatici EV',
			mentions: 12,
			sentiment: 'neutral',
			trend: 'up_strong',
			isNew: true,
			overview: {
				totalMentions: 12,
				avgSentiment: 5.5,
				entities: ['Pirelli Elect', 'Michelin', 'Continental', 'Tesla', 'EV'],
				weeklyTrend: [3, 5, 8, 12]
			},
			serp: {
				results: [
					{ position: 1, title: 'Pneumatici per auto elettriche: guida 2026', domain: 'vaielettrico.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 2, title: 'Pirelli Elect: pneumatici per EV', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'I migliori pneumatici per Tesla', domain: 'teslaclub.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 4, title: 'Michelin e.Primacy per elettriche', domain: 'michelin.it', sentiment: 'positive', isCompetitor: true },
					{ position: 5, title: 'Come scegliere gomme per EV', domain: 'alvolante.it', sentiment: 'neutral', isCompetitor: false },
				],
				breakdown: { organic: 7, news: 3, aiOverview: 2 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 55, quote: 'Pirelli Elect e una linea dedicata ai veicoli elettrici...' },
					{ name: 'Gemini', mentionsPirelli: false, score: 30, quote: 'I pneumatici per EV devono gestire peso e coppia maggiori...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 48, quote: 'Pirelli Elect e Michelin e.Primacy sono le principali opzioni...' },
					{ name: 'Claude', mentionsPirelli: true, score: 52, quote: 'Pirelli ha sviluppato la tecnologia Elect specificamente per EV...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 22, quote: 'Per le auto elettriche servono pneumatici specifici...' },
				],
				overallScore: 41
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Forte con e.Primacy', contentAngle: 'Efficienza energetica', threatLevel: 'Alto' },
					{ name: 'Continental', position: 'EcoContact', contentAngle: 'Bassa resistenza', threatLevel: 'Medio' },
					{ name: 'Bridgestone', position: 'Turanza EV', contentAngle: 'Comfort e silenziosita', threatLevel: 'Medio' },
					{ name: 'Hankook', position: 'OEM Tesla/Porsche', contentAngle: 'Primo equipaggiamento', threatLevel: 'Medio' },
				]
			}
		},
		{
			name: 'sicurezza stradale',
			mentions: 32,
			sentiment: 'positive',
			trend: 'stable',
			isNew: false,
			overview: {
				totalMentions: 32,
				avgSentiment: 7.0,
				entities: ['ACI', 'Pirelli', 'Michelin', 'Euro NCAP', 'ANAS'],
				weeklyTrend: [30, 31, 33, 32]
			},
			serp: {
				results: [
					{ position: 1, title: 'Sicurezza stradale: il ruolo dei pneumatici', domain: 'aci.it', sentiment: 'positive', isCompetitor: false },
					{ position: 2, title: 'Come i pneumatici influenzano la frenata', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'Test frenata sul bagnato ADAC', domain: 'adac.de', sentiment: 'neutral', isCompetitor: false },
					{ position: 4, title: 'Michelin: sicurezza fino a fine vita', domain: 'michelin.it', sentiment: 'positive', isCompetitor: true },
					{ position: 5, title: 'Quando cambiare i pneumatici', domain: 'alvolante.it', sentiment: 'neutral', isCompetitor: false },
				],
				breakdown: { organic: 20, news: 8, aiOverview: 4 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 60, quote: 'La qualita del pneumatico e fondamentale per la sicurezza...' },
					{ name: 'Gemini', mentionsPirelli: true, score: 55, quote: 'Brand come Pirelli e Michelin investono nella sicurezza...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 62, quote: 'Pirelli offre pneumatici con elevati standard di sicurezza...' },
					{ name: 'Claude', mentionsPirelli: true, score: 58, quote: 'La sicurezza stradale dipende fortemente dalla qualita dei pneumatici...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 38, quote: 'Per la sicurezza, scegliere pneumatici di marca premium...' },
				],
				overallScore: 55
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Co-leader', contentAngle: 'Sicurezza fino all\'usura', threatLevel: 'Medio' },
					{ name: 'Continental', position: 'Forte nei test', contentAngle: 'Risultati ADAC', threatLevel: 'Medio' },
					{ name: 'Bridgestone', position: 'Presente', contentAngle: 'Turanza sicurezza', threatLevel: 'Basso' },
					{ name: 'Goodyear', position: 'Marginale', contentAngle: 'Assurance', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'formula 1 pirelli',
			mentions: 41,
			sentiment: 'very_positive',
			trend: 'stable',
			isNew: false,
			overview: {
				totalMentions: 41,
				avgSentiment: 8.8,
				entities: ['Pirelli', 'F1', 'FIA', 'P Zero', 'Grand Prix'],
				weeklyTrend: [39, 40, 42, 41]
			},
			serp: {
				results: [
					{ position: 1, title: 'Pirelli fornitore unico F1 fino al 2027', domain: 'pirelli.com', sentiment: 'very_positive', isCompetitor: false },
					{ position: 2, title: 'Strategia gomme GP Australia 2026', domain: 'formula1.com', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'Pirelli P Zero F1: tecnologia estrema', domain: 'motorsport.com', sentiment: 'very_positive', isCompetitor: false },
					{ position: 4, title: 'Come Pirelli sviluppa le gomme F1', domain: 'autosport.com', sentiment: 'positive', isCompetitor: false },
					{ position: 5, title: 'F1 2026: nuove mescole Pirelli', domain: 'f1news.it', sentiment: 'positive', isCompetitor: false },
				],
				breakdown: { organic: 25, news: 14, aiOverview: 2 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 95, quote: 'Pirelli e il fornitore esclusivo di pneumatici per la Formula 1...' },
					{ name: 'Gemini', mentionsPirelli: true, score: 92, quote: 'Pirelli fornisce pneumatici alla F1, sviluppando tecnologie all\'avanguardia...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 90, quote: 'Come fornitore unico, Pirelli sviluppa tutte le mescole per la F1...' },
					{ name: 'Claude', mentionsPirelli: true, score: 93, quote: 'Pirelli ha un contratto esclusivo con la F1 come fornitore unico...' },
					{ name: 'Copilot', mentionsPirelli: true, score: 88, quote: 'Pirelli e il fornitore ufficiale della Formula 1 dal 2011...' },
				],
				overallScore: 92
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Assente da F1', contentAngle: 'Focus su WEC/Le Mans', threatLevel: 'Basso' },
					{ name: 'Bridgestone', position: 'Ex fornitore', contentAngle: 'Nostalgia storica', threatLevel: 'Basso' },
					{ name: 'Continental', position: 'Non presente', contentAngle: 'N/A', threatLevel: 'Basso' },
					{ name: 'Goodyear', position: 'Non presente', contentAngle: 'NASCAR focus', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'confronto pneumatici',
			mentions: 22,
			sentiment: 'mixed',
			trend: 'stable',
			isNew: false,
			overview: {
				totalMentions: 22,
				avgSentiment: 5.2,
				entities: ['Pirelli', 'Michelin', 'Continental', 'Bridgestone', 'test'],
				weeklyTrend: [20, 21, 23, 22]
			},
			serp: {
				results: [
					{ position: 1, title: 'Confronto pneumatici 2026: quale marca scegliere', domain: 'alvolante.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 2, title: 'Michelin vs Pirelli: chi vince', domain: 'quattroruote.it', sentiment: 'mixed', isCompetitor: false },
					{ position: 3, title: 'Test comparativo pneumatici estivi', domain: 'adac.de', sentiment: 'neutral', isCompetitor: false },
					{ position: 4, title: 'I 10 migliori pneumatici per qualita-prezzo', domain: 'trovaprezzi.it', sentiment: 'negative', isCompetitor: false },
					{ position: 5, title: 'Pirelli vs Continental: confronto completo', domain: 'gomme-auto.it', sentiment: 'positive', isCompetitor: false },
				],
				breakdown: { organic: 15, news: 4, aiOverview: 3 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: true, score: 60, quote: 'Pirelli eccelle nelle prestazioni sportive, Michelin nella versatilita...' },
					{ name: 'Gemini', mentionsPirelli: true, score: 50, quote: 'Nel segmento premium, Pirelli, Michelin e Continental si contendono...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 55, quote: 'Pirelli e spesso associata al segmento ultra high performance...' },
					{ name: 'Claude', mentionsPirelli: true, score: 58, quote: 'Pirelli si distingue nel segmento sportivo e premium...' },
					{ name: 'Copilot', mentionsPirelli: true, score: 45, quote: 'I principali brand premium sono Michelin, Pirelli e Continental...' },
				],
				overallScore: 54
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Spesso preferito nei confronti', contentAngle: 'Versatilita e durata', threatLevel: 'Alto' },
					{ name: 'Continental', position: 'Forte nei test', contentAngle: 'Risultati oggettivi', threatLevel: 'Medio' },
					{ name: 'Bridgestone', position: 'Terza scelta', contentAngle: 'Rapporto qualita-prezzo', threatLevel: 'Medio' },
					{ name: 'Goodyear', position: 'Alternativa budget', contentAngle: 'Prezzo competitivo', threatLevel: 'Basso' },
				]
			}
		},
		{
			name: 'manutenzione gomme',
			mentions: 15,
			sentiment: 'neutral',
			trend: 'down',
			isNew: false,
			overview: {
				totalMentions: 15,
				avgSentiment: 5.0,
				entities: ['pneumatici', 'pressione', 'usura', 'convergenza', 'sostituzione'],
				weeklyTrend: [18, 17, 16, 15]
			},
			serp: {
				results: [
					{ position: 1, title: 'Come mantenere i pneumatici in perfetto stato', domain: 'aci.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 2, title: 'Controllo pressione pneumatici: guida', domain: 'pirelli.com', sentiment: 'positive', isCompetitor: false },
					{ position: 3, title: 'Quando sostituire i pneumatici', domain: 'alvolante.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 4, title: 'Usura pneumatici: i segnali da controllare', domain: 'quattroruote.it', sentiment: 'neutral', isCompetitor: false },
					{ position: 5, title: 'Manutenzione auto: guida ai pneumatici', domain: 'Repubblica.it', sentiment: 'neutral', isCompetitor: false },
				],
				breakdown: { organic: 10, news: 3, aiOverview: 2 }
			},
			aiVisibility: {
				models: [
					{ name: 'ChatGPT', mentionsPirelli: false, score: 35, quote: 'La manutenzione dei pneumatici include controllo pressione e usura...' },
					{ name: 'Gemini', mentionsPirelli: false, score: 28, quote: 'Controllare regolarmente la pressione e l\'usura del battistrada...' },
					{ name: 'Perplexity', mentionsPirelli: true, score: 42, quote: 'Pirelli consiglia di controllare la pressione ogni mese...' },
					{ name: 'Claude', mentionsPirelli: false, score: 30, quote: 'La corretta manutenzione include pressione, convergenza e rotazione...' },
					{ name: 'Copilot', mentionsPirelli: false, score: 20, quote: 'Mantenere la corretta pressione dei pneumatici e fondamentale...' },
				],
				overallScore: 31
			},
			competitor: {
				entries: [
					{ name: 'Michelin', position: 'Buoni contenuti educativi', contentAngle: 'Guide manutenzione', threatLevel: 'Basso' },
					{ name: 'Continental', position: 'Presente', contentAngle: 'Consigli sicurezza', threatLevel: 'Basso' },
					{ name: 'Bridgestone', position: 'Presente', contentAngle: 'Tutorial video', threatLevel: 'Basso' },
					{ name: 'Goodyear', position: 'Marginale', contentAngle: 'FAQ base', threatLevel: 'Basso' },
				]
			}
		}
	];

	let selectedTopic = $state<TopicData | null>(null);
	let activeTab = $state(0);

	function selectTopic(topic: TopicData) {
		selectedTopic = topic;
		activeTab = 0;
		setTimeout(() => {
			document.getElementById('topic-drilldown')?.scrollIntoView({ behavior: 'smooth', block: 'start' });
		}, 100);
	}

	function sentimentColor(s: string): string {
		switch (s) {
			case 'very_positive': return 'bg-emerald-500';
			case 'positive': return 'bg-emerald-400';
			case 'neutral': return 'bg-gray-400';
			case 'mixed': return 'bg-amber-400';
			case 'negative': return 'bg-red-500';
			default: return 'bg-gray-400';
		}
	}

	function sentimentBorder(s: string): string {
		switch (s) {
			case 'very_positive': return 'border-emerald-300';
			case 'positive': return 'border-emerald-200';
			case 'neutral': return 'border-gray-200';
			case 'mixed': return 'border-amber-200';
			case 'negative': return 'border-red-200';
			default: return 'border-gray-200';
		}
	}

	function sentimentLabel(s: string): string {
		switch (s) {
			case 'very_positive': return 'Molto positivo';
			case 'positive': return 'Positivo';
			case 'neutral': return 'Neutro';
			case 'mixed': return 'Misto';
			case 'negative': return 'Negativo';
			default: return s;
		}
	}

	function trendIcon(t: string): string {
		switch (t) {
			case 'up_strong': return 'rotate-[-45deg]';
			case 'up': return 'rotate-[-30deg]';
			case 'stable': return 'rotate-0';
			case 'down': return 'rotate-[30deg]';
			default: return 'rotate-0';
		}
	}

	function trendColor(t: string): string {
		switch (t) {
			case 'up_strong': return 'text-emerald-500';
			case 'up': return 'text-emerald-400';
			case 'stable': return 'text-gray-400';
			case 'down': return 'text-red-400';
			default: return 'text-gray-400';
		}
	}

	function trendLabel(t: string): string {
		switch (t) {
			case 'up_strong': return 'In forte crescita';
			case 'up': return 'In crescita';
			case 'stable': return 'Stabile';
			case 'down': return 'In calo';
			default: return t;
		}
	}

	function threatColor(t: string): string {
		switch (t) {
			case 'Alto': return 'bg-red-100 text-red-700';
			case 'Medio': return 'bg-amber-100 text-amber-700';
			case 'Basso': return 'bg-gray-100 text-gray-600';
			default: return 'bg-gray-100 text-gray-600';
		}
	}

	const tabLabels = ['Panoramica', 'SERP & News', 'Visibilita AI', 'Competitor'];
</script>

<div class="space-y-6 max-w-7xl mx-auto">
	<div>
		<h2 class="text-2xl font-bold text-gray-900">Topic Explorer</h2>
		<p class="text-sm text-gray-500 mt-1">Esplora i topic monitorati e analizza il posizionamento su ogni canale</p>
	</div>

	<!-- Topic Grid -->
	<div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-3">
		{#each topics as topic}
			<button
				onclick={() => selectTopic(topic)}
				class="relative text-left p-4 rounded-xl border bg-white shadow-sm transition-colors hover:border-gray-300 {selectedTopic?.name === topic.name ? 'ring-2 ring-blue-500 border-blue-200' : sentimentBorder(topic.sentiment)} {topic.mentions > 35 ? 'lg:col-span-1' : ''}"
			>
				{#if topic.isNew}
					<span class="absolute top-2 right-2 px-1.5 py-0.5 text-[8px] font-bold uppercase bg-violet-100 text-violet-600 rounded-full">NEW</span>
				{/if}

				<h4 class="text-sm font-semibold text-gray-900 capitalize pr-6">{topic.name}</h4>

				<div class="flex items-center gap-2 mt-2">
					<span class="text-xs text-gray-500">{topic.mentions} menzioni</span>
					<span class="w-2 h-2 rounded-full {sentimentColor(topic.sentiment)}"></span>
				</div>

				<div class="flex items-center gap-1 mt-1 {trendColor(topic.trend)}">
					<svg class="w-3.5 h-3.5 {trendIcon(topic.trend)}" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2.5">
						<path stroke-linecap="round" stroke-linejoin="round" d="M5 12h14M12 5l7 7" />
					</svg>
					<span class="text-[10px] font-medium">{trendLabel(topic.trend)}</span>
				</div>
			</button>
		{/each}
	</div>

	<!-- Drilldown Section -->
	{#if selectedTopic}
		<div id="topic-drilldown">
			<div class="bg-white rounded-2xl shadow-sm border border-gray-100 overflow-hidden">
				<!-- Topic Header -->
				<div class="p-6 bg-gray-900 text-white">
					<div class="flex items-center justify-between">
						<div>
							<h3 class="text-xl font-bold capitalize">{selectedTopic.name}</h3>
							<div class="flex items-center gap-4 mt-2 text-sm text-gray-300">
								<span>{selectedTopic.mentions} menzioni totali</span>
								<span class="flex items-center gap-1">
									<span class="w-2 h-2 rounded-full {sentimentColor(selectedTopic.sentiment)}"></span>
									{sentimentLabel(selectedTopic.sentiment)}
								</span>
								<span class="{trendColor(selectedTopic.trend)}">{trendLabel(selectedTopic.trend)}</span>
							</div>
						</div>
						<button
							onclick={() => selectedTopic = null}
							class="text-gray-400 hover:text-white transition-colors"
						>
							<svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
								<path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
							</svg>
						</button>
					</div>
				</div>

				<!-- Tabs -->
				<div class="border-b border-gray-100 px-6">
					<div class="flex gap-0">
						{#each tabLabels as label, idx}
							<button
								onclick={() => activeTab = idx}
								class="px-4 py-3 text-sm font-medium border-b-2 transition-colors duration-200 {activeTab === idx ? 'border-blue-500 text-blue-600' : 'border-transparent text-gray-400 hover:text-gray-600'}"
							>
								{label}
							</button>
						{/each}
					</div>
				</div>

				<!-- Tab Content -->
				<div class="p-6">
					<!-- Tab 0: Panoramica -->
					{#if activeTab === 0}
						<div class="grid grid-cols-1 md:grid-cols-3 gap-6">
							<div class="space-y-4">
								<div>
									<p class="text-[10px] font-semibold uppercase tracking-wider text-gray-400 mb-1">Menzioni totali</p>
									<p class="text-3xl font-bold text-gray-900">{selectedTopic.overview.totalMentions}</p>
								</div>
								<div>
									<p class="text-[10px] font-semibold uppercase tracking-wider text-gray-400 mb-1">Sentiment medio</p>
									<div class="flex items-center gap-2">
										<p class="text-3xl font-bold text-gray-900">{selectedTopic.overview.avgSentiment.toFixed(1)}</p>
										<span class="text-xs text-gray-500">/ 10</span>
									</div>
									<!-- Simple gauge — functional gradient bar (sentiment scale) -->
									<div class="mt-2 h-2 bg-gray-100 rounded-full overflow-hidden">
										<div class="h-full rounded-full bg-gradient-to-r from-red-500 via-amber-400 to-emerald-500" style="width: {selectedTopic.overview.avgSentiment * 10}%"></div>
									</div>
								</div>
							</div>

							<div>
								<p class="text-[10px] font-semibold uppercase tracking-wider text-gray-400 mb-3">Entita associate</p>
								<div class="flex flex-wrap gap-2">
									{#each selectedTopic.overview.entities as entity}
										<span class="px-2.5 py-1 text-xs font-medium bg-gray-100 text-gray-700 rounded-full">{entity}</span>
									{/each}
								</div>
							</div>

							<div>
								<p class="text-[10px] font-semibold uppercase tracking-wider text-gray-400 mb-3">Trend ultime 4 settimane</p>
								{#if selectedTopic}
									{@const values = selectedTopic.overview.weeklyTrend}
									{@const max = Math.max(...values)}
									{@const min = Math.min(...values)}
									{@const range = max - min || 1}
									{@const points = values.map((v, i) => `${(i / (values.length - 1)) * 110 + 5},${45 - ((v - min) / range) * 35}`).join(' ')}
									<svg viewBox="0 0 120 50" class="w-full h-16">
										<polyline
											points={points}
											fill="none"
											stroke="#3b82f6"
											stroke-width="2"
											stroke-linecap="round"
											stroke-linejoin="round"
										/>
										{#each values as v, i}
											<circle
												cx={(i / (values.length - 1)) * 110 + 5}
												cy={45 - ((v - min) / range) * 35}
												r="3"
												fill="white"
												stroke="#3b82f6"
												stroke-width="2"
											/>
											<text
												x={(i / (values.length - 1)) * 110 + 5}
												y={45 - ((v - min) / range) * 35 - 8}
												text-anchor="middle"
												class="text-[8px] fill-gray-500"
											>{v}</text>
										{/each}
									</svg>
								{/if}
							</div>
						</div>
					{/if}

					<!-- Tab 1: SERP & News -->
					{#if activeTab === 1}
						<div class="space-y-4">
							<!-- Source breakdown -->
							<div class="flex items-center gap-4 mb-4">
								<span class="text-xs text-gray-500">{selectedTopic.serp.breakdown.organic} Organic</span>
								<span class="text-xs text-gray-500">{selectedTopic.serp.breakdown.news} News</span>
								<span class="text-xs text-gray-500">{selectedTopic.serp.breakdown.aiOverview} AI Overview</span>
							</div>

							{#each selectedTopic.serp.results as result}
								<div class="flex items-start gap-4 p-4 rounded-xl bg-gray-50 border border-gray-100 {result.isCompetitor ? 'border-orange-200 bg-orange-50/30' : ''}">
									<div class="w-8 h-8 rounded-lg bg-gray-200 flex items-center justify-center shrink-0">
										<span class="text-sm font-bold text-gray-600">{result.position}</span>
									</div>
									<div class="flex-1 min-w-0">
										<p class="text-sm font-medium text-gray-900 truncate">{result.title}</p>
										<p class="text-xs text-gray-400 mt-0.5">{result.domain}</p>
									</div>
									<div class="flex items-center gap-2 shrink-0">
										{#if result.isCompetitor}
											<span class="text-[10px] font-semibold uppercase text-orange-600 bg-orange-100 px-1.5 py-0.5 rounded">Competitor</span>
										{/if}
										<span class="w-2 h-2 rounded-full {sentimentColor(result.sentiment)}"></span>
									</div>
								</div>
							{/each}
						</div>
					{/if}

					<!-- Tab 2: Visibilita AI -->
					{#if activeTab === 2}
						<div class="space-y-4">
							<div class="flex items-center gap-3 mb-4">
								<p class="text-sm text-gray-500">Punteggio complessivo visibilita AI:</p>
								<span class="text-2xl font-bold text-gray-900">{selectedTopic.aiVisibility.overallScore}%</span>
							</div>

							{#each selectedTopic.aiVisibility.models as model}
								<div class="p-4 rounded-xl bg-gray-50 border border-gray-100">
									<div class="flex items-center justify-between mb-2">
										<div class="flex items-center gap-3">
											<span class="text-sm font-semibold text-gray-900">{model.name}</span>
											{#if model.mentionsPirelli}
												<span class="text-[10px] font-semibold uppercase text-emerald-600 bg-emerald-100 px-1.5 py-0.5 rounded">Menzionato</span>
											{:else}
												<span class="text-[10px] font-semibold uppercase text-red-500 bg-red-100 px-1.5 py-0.5 rounded">Assente</span>
											{/if}
										</div>
										<span class="text-sm font-bold text-gray-700">{model.score}%</span>
									</div>
									<!-- Score bar — functional progress bar -->
									<div class="h-1.5 bg-gray-200 rounded-full overflow-hidden mb-2">
										<div
											class="h-full rounded-full bg-blue-500"
											style="width: {model.score}%"
										></div>
									</div>
									<p class="text-xs text-gray-500 italic">"{model.quote}"</p>
								</div>
							{/each}
						</div>
					{/if}

					<!-- Tab 3: Competitor -->
					{#if activeTab === 3}
						<div class="overflow-x-auto">
							<table class="w-full text-sm">
								<thead>
									<tr class="border-b border-gray-100">
										<th class="text-left py-3 px-4 font-medium text-gray-500">Competitor</th>
										<th class="text-left py-3 px-4 font-medium text-gray-500">Posizionamento</th>
										<th class="text-left py-3 px-4 font-medium text-gray-500">Angolo contenuti</th>
										<th class="text-left py-3 px-4 font-medium text-gray-500">Livello minaccia</th>
									</tr>
								</thead>
								<tbody>
									{#each selectedTopic.competitor.entries as entry}
										<tr class="border-b border-gray-50 hover:bg-gray-50 transition-colors">
											<td class="py-3 px-4 font-semibold text-gray-900">{entry.name}</td>
											<td class="py-3 px-4 text-gray-600">{entry.position}</td>
											<td class="py-3 px-4 text-gray-600">{entry.contentAngle}</td>
											<td class="py-3 px-4">
												<span class="inline-flex items-center px-2 py-0.5 rounded-full text-[10px] font-semibold {threatColor(entry.threatLevel)}">
													{entry.threatLevel}
												</span>
											</td>
										</tr>
									{/each}
								</tbody>
							</table>
						</div>
					{/if}
				</div>
			</div>
		</div>
	{/if}
</div>
