import type { PageLoad } from './$types.js';

export const load: PageLoad = async () => {
	const trends = [
		{
			topic: 'ADAS e pneumatici smart',
			tai: 420,
			badge: 'Nuovo',
			weeklyChange: 420,
			bars: [1, 2, 2, 3, 5, 8, 10, 10]
		},
		{
			topic: 'Pneumatici EV',
			tai: 340,
			badge: 'In crescita',
			weeklyChange: 34,
			bars: [3, 4, 5, 5, 6, 7, 9, 10]
		},
		{
			topic: 'Sostenibilita pneumatici',
			tai: 180,
			badge: 'In crescita',
			weeklyChange: 22,
			bars: [2, 3, 4, 4, 5, 6, 7, 8]
		},
		{
			topic: 'Noise labeling EU',
			tai: 156,
			badge: 'Nuovo',
			weeklyChange: 156,
			bars: [0, 0, 1, 2, 3, 5, 7, 9]
		},
		{
			topic: 'Pneumatici ricostruiti',
			tai: 89,
			badge: 'In crescita',
			weeklyChange: 12,
			bars: [4, 4, 5, 5, 6, 6, 7, 7]
		},
		{
			topic: 'Gomme all season',
			tai: 67,
			badge: 'Stabile',
			weeklyChange: 3,
			bars: [6, 6, 7, 7, 7, 7, 7, 7]
		},
		{
			topic: 'Pneumatici usati online',
			tai: 45,
			badge: 'Stabile',
			weeklyChange: -2,
			bars: [5, 5, 5, 5, 5, 5, 5, 5]
		},
		{
			topic: 'Run flat vs standard',
			tai: 23,
			badge: 'In calo',
			weeklyChange: -15,
			bars: [8, 7, 7, 6, 6, 5, 4, 4]
		}
	];

	const emergingSignals = [
		{
			title: 'Pneumatici con sensori integrati',
			description: 'Crescente interesse per pneumatici dotati di sensori IoT per il monitoraggio in tempo reale della pressione, temperatura e usura. I produttori premium stanno accelerando su questa tecnologia.',
			growth: 520,
			action: 'Creare contenuti tecnici sul sistema Pirelli Cyber Tyre e posizionarsi come leader tecnologico'
		},
		{
			title: 'Regolamento Euro 7 e impatto pneumatici',
			description: 'Le nuove normative Euro 7 includono per la prima volta limiti sulle emissioni di particolato da pneumatici. Il tema sta generando ricerche informative in forte crescita.',
			growth: 310,
			action: 'Pubblicare una guida autorevole sulla conformita Pirelli alle normative Euro 7'
		},
		{
			title: 'Pneumatici per car sharing e flotte',
			description: 'La crescita del car sharing urbano sta creando un nuovo segmento di domanda con esigenze specifiche: alta percorrenza, resistenza e costo per chilometro ottimizzato.',
			growth: 185,
			action: 'Sviluppare landing page B2B dedicata alla gestione flotte con gamma Pirelli'
		},
		{
			title: 'Materiali sostenibili nei pneumatici',
			description: 'Consumatori e media stanno focalizzando l\'attenzione sui materiali riciclati e bio-based nella produzione di pneumatici. Michelin sta dominando questa narrativa.',
			growth: 240,
			action: 'Contrastare la narrativa Michelin con contenuti sulla strategia Pirelli di sostenibilita al 2030'
		}
	];

	return { trends, emergingSignals };
};
