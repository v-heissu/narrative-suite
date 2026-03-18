import type { PageLoad } from './$types.js';

export const load: PageLoad = async () => {
	const columns = [
		{
			id: 'idea',
			title: 'Idea',
			cards: [
				{
					id: 1,
					title: 'Pirelli e la Formula E: tecnologia per la strada',
					keyword: 'pirelli formula e',
					dueDate: '2026-04-02',
					assignee: 'MR',
					priority: 'media'
				},
				{
					id: 2,
					title: 'Confronto pneumatici invernali 2026: guida completa',
					keyword: 'pneumatici invernali 2026',
					dueDate: '2026-04-10',
					assignee: 'LB',
					priority: 'alta'
				}
			]
		},
		{
			id: 'briefed',
			title: 'Briefed',
			cards: [
				{
					id: 3,
					title: 'Come scegliere pneumatici per auto elettriche',
					keyword: 'pneumatici auto elettriche',
					dueDate: '2026-03-28',
					assignee: 'AG',
					priority: 'alta'
				},
				{
					id: 4,
					title: 'Noise labeling EU: cosa cambia per gli automobilisti',
					keyword: 'noise labeling pneumatici',
					dueDate: '2026-04-05',
					assignee: 'MR',
					priority: 'media'
				},
				{
					id: 5,
					title: 'Pneumatici run flat: vantaggi e svantaggi',
					keyword: 'pneumatici run flat',
					dueDate: '2026-04-08',
					assignee: 'LB',
					priority: 'bassa'
				}
			]
		},
		{
			id: 'writing',
			title: 'In Scrittura',
			cards: [
				{
					id: 6,
					title: 'Guida pneumatici 4 stagioni 2026',
					keyword: 'pneumatici 4 stagioni',
					dueDate: '2026-03-25',
					assignee: 'AG',
					priority: 'alta'
				},
				{
					id: 7,
					title: 'Pirelli e la sostenibilita: materiali riciclati',
					keyword: 'pneumatici sostenibili',
					dueDate: '2026-03-27',
					assignee: 'MR',
					priority: 'alta'
				}
			]
		},
		{
			id: 'review',
			title: 'Review',
			cards: [
				{
					id: 8,
					title: 'Pneumatici SUV: come scegliere i migliori',
					keyword: 'pneumatici SUV',
					dueDate: '2026-03-22',
					assignee: 'LB',
					priority: 'media'
				},
				{
					id: 9,
					title: 'Pressione pneumatici: guida al controllo corretto',
					keyword: 'pressione pneumatici corretta',
					dueDate: '2026-03-23',
					assignee: 'AG',
					priority: 'bassa'
				}
			]
		},
		{
			id: 'published',
			title: 'Pubblicato',
			cards: [
				{
					id: 10,
					title: 'Pirelli P Zero: tecnologia da pista alla strada',
					keyword: 'pirelli p zero',
					dueDate: '2026-03-15',
					assignee: 'MR',
					priority: 'alta'
				},
				{
					id: 11,
					title: 'Come leggere le marcature sui pneumatici',
					keyword: 'marcature pneumatici',
					dueDate: '2026-03-12',
					assignee: 'LB',
					priority: 'media'
				},
				{
					id: 12,
					title: 'Quando cambiare i pneumatici: segnali e tempistiche',
					keyword: 'quando cambiare pneumatici',
					dueDate: '2026-03-10',
					assignee: 'AG',
					priority: 'bassa'
				}
			]
		}
	];

	return { columns };
};
