import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faFileContract } from '@fortawesome/free-solid-svg-icons/faFileContract';
import { faUserGraduate } from '@fortawesome/free-solid-svg-icons/faUserGraduate';
import { faIdCard } from '@fortawesome/free-regular-svg-icons/faIdCard';

interface iNavigation {
  title: string;
  rota: string;
  icon: IconDefinition;
}

export const Navigations: iNavigation[] = [
  {
    title: 'Experiências',
    rota: '/experiencias',
    icon: faFileContract,
  },
  {
    title: 'Certificados',
    rota: '/certificados',
    icon: faUserGraduate,
  },
  {
    title: 'Sobre mim',
    rota: '/about-me',
    icon: faIdCard,
  },
];
