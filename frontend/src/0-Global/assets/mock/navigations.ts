import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faFileContract } from '@fortawesome/free-solid-svg-icons/faFileContract';
import { faUserGraduate } from '@fortawesome/free-solid-svg-icons/faUserGraduate';

interface iNavigation {
  title: string;
  rota: string;
  icon: IconDefinition;
}

export const Navigations: iNavigation[] = [
  {
    title: 'Certificados',
    rota: 'certificados',
    icon: faUserGraduate,
  },
  {
    title: 'Experiências',
    rota: 'experiencias',
    icon: faFileContract,
  },
];
