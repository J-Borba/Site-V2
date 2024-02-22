import { createRouter, createWebHistory } from 'vue-router';

const baseTitle: string = 'João Borba';

const routes = [
  // Home Page
  {
    path: '/',
    name: 'home',
    meta: {
      title: `${baseTitle} : Home`,
    },
    component: () => import('../../1-Curriculo/views/home-view.vue'),
  },
  {
    path: '/experiencias',
    name: 'experiencia',
    meta: {
      title: `${baseTitle} : Experiência`,
    },
    component: () => import('../../1-Curriculo/views/experiencia-view.vue'),
  },
  {
    path: '/certificados',
    name: 'certificados',
    meta: {
      title: `${baseTitle} : Certificados`,
    },
    component: () => import('../../1-Curriculo/views/certificados-view.vue'),
  },
  // Financeiro
  // {
  //   path: '/financeiro/',
  //   name: 'financeiro curriculo',
  //   meta: {
  //     title: `Financeiro : Home`,
  //   },
  //   component: () => import('../../2-Financeiro/views/HomeView.vue'),
  // },
  // Not Found
  {
    path: '/:catchAll(.*)',
    name: 'notfound',
    meta: {
      title: `Not Found`,
    },
    component: () => import('../../0-Global/views/not-found-view.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
