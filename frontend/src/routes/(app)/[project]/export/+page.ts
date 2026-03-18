import type { PageLoad } from './$types.js';

export const load: PageLoad = async () => {
	const reportSections = [
		{ id: 'kpi', label: 'Metriche KPI', checked: true },
		{ id: 'serp', label: 'Risultati SERP', checked: true },
		{ id: 'ai', label: 'Visibilita AI', checked: true },
		{ id: 'insight', label: 'Insight & Opportunita', checked: true },
		{ id: 'editorial', label: 'Piano Editoriale', checked: false }
	];

	const preview = {
		title: 'Report Settimanale - Pirelli',
		period: '11 - 18 Marzo 2026',
		summary: `Il report settimanale include l'analisi completa della presenza narrativa Pirelli nel periodo di riferimento. Sono stati monitorati 24 keyword su Google SERP, Google News e AI Overview, analizzata la visibilita su 5 modelli AI (ChatGPT, Gemini, Perplexity, Grok, Claude) e generati 12 insight strategici.

Principali evidenze:
- NsoV in crescita del 7.3% rispetto alla settimana precedente
- 3 nuove opportunita identificate su topic emergenti
- Rischio narrativo in diminuzione grazie ai contenuti pubblicati
- Trend Acceleration Index positivo su 6 dei 8 topic monitorati

Il report contiene inoltre il confronto competitivo con 5 brand (Michelin, Continental, Bridgestone, Goodyear, Hankook) e le raccomandazioni operative per la settimana successiva.`
	};

	return { reportSections, preview };
};
