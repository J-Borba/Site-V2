import { abbr, tiplan } from '@/0-Global/assets/utilities/_variables';
import abbrLogo from '../images/abbr.png';
import tiplanLogo from '../images/tiplan.png';

export interface IRole {
  title: string;
  startDate: string;
  endDate?: string;
  skills: string[];
}

export interface IJob {
  companyLogo: string;
  companyName: string;
  shortCompanyName?: string;
  companyUrl?: string;
  roles: IRole[]; // most recent first
}

export const Jobs: IJob[] = [
  {
    companyLogo: tiplanLogo,
    companyName: tiplan.title,
    companyUrl: tiplan.url,

    roles: [
      {
        title: 'Desenvolvedor Jr',
        startDate: 'Jan/2025',
        skills: ['MVC', '.NET', 'C#', 'Javascript', 'Vue.js 2', 'SQL Server'],
      },
      {
        title: 'Estagiário de Desenvolvimento',
        startDate: 'Out/2023',
        endDate: 'Dez/2024',
        skills: ['MVC', '.NET', 'C#', 'Javascript', 'Vue.js 2', 'SQL Server'],
      },
    ],
  },
  {
    companyLogo: abbrLogo,
    companyName: abbr.shortTitle + ' - ' + abbr.title,
    shortCompanyName: abbr.shortTitle,
    companyUrl: abbr.url,

    roles: [
      {
        title: 'Analista de Sistemas Trainee',
        startDate: 'Fev/2023',
        endDate: 'Jun/2023',
        skills: ['ChatBot', 'Javascript', 'Vue.js 3', 'Power BI', 'SQL Server', 'Power Automate', 'TOTVS - RM', 'TOTVS - Protheus'],
      },
      {
        title: 'Estagiário',
        startDate: 'Mar/2022',
        endDate: 'Fev/2023',
        skills: ['Suporte', 'Infraestrutura de TI', 'Excel'],
      },
    ],
  },
];
