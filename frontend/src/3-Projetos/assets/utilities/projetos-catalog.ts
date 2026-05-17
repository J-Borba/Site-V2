import type { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faChartLine } from '@fortawesome/free-solid-svg-icons/faChartLine';

export type ProjectStatus = 'ativo' | 'em-breve' | 'arquivado';

export interface ProjetoEntry {
  slug: string;
  title: string;
  description: string;
  icon: IconDefinition;
  route: string;
  status: ProjectStatus;
  tech: string[];
}

export const ProjetosCatalog: ProjetoEntry[] = [
  {
    slug: 'financeiro',
    title: 'Financeiro',
    description: 'Controle de investimentos em ações, FIIs e renda fixa. Registre posições, proventos e aportes — seu patrimônio em um só lugar.',
    icon: faChartLine,
    route: '/projetos/financeiro',
    status: 'ativo',
    tech: ['Vue 3', 'Pinia', 'ApexCharts'],
  },
];
