import { abbr, tiplan } from '@/0-Global/assets/utilities/_variables';
import abbrLogo from '../images/abbr.png';
import tiplanLogo from '../images/tiplan.png';

interface IRole {
  title: string;
  startDate: string;
  endDate?: string;
  skills: string[];
}

interface IJob {
  companyLogo: string;
  companyName: string;
  shortCompanyName?: string;
  companyUrl?: string;

  role: IRole;
}

export const Jobs: IJob[] = [
  {
    companyLogo: tiplanLogo,
    companyName: tiplan.title,
    companyUrl: tiplan.url,

    role: {
      title: 'Estagiário de Desenvolvimento',
      startDate: 'Out/2023',

      skills: ['MVC', '.NET', 'C#', 'Javascript', 'Vue.js 2', 'SQL Server'],
    },
  },
  {
    companyLogo: abbrLogo,
    companyName: abbr.shortTitle + ' - ' + abbr.title,
    shortCompanyName: abbr.shortTitle,
    companyUrl: abbr.url,

    role: {
      title: 'Analista de Sistemas Trainee',
      startDate: 'Fev/2023',
      endDate: 'Jun/2023',

      skills: ['ChatBot', 'Javascript', 'Vue.js 3'],
    },
  },
  {
    companyLogo: abbrLogo,
    companyName: abbr.shortTitle + ' - ' + abbr.title,
    shortCompanyName: abbr.shortTitle,
    companyUrl: abbr.url,

    role: {
      title: 'Estagiário',
      startDate: 'Mar/2022',
      endDate: 'Fev/2023',
      skills: [
        'Suporte',
        'Infraestrutura de TI',
        'Power BI',
        'SQL Server',
        'Excel',
        'Power Automate',
        'TOTVS - RM',
        'TOTVS - Protheus',
      ],
    },
  },
];
