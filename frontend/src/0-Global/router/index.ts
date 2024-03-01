import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';

const routes: RouteRecordRaw[] = [
  // Home
  {
    path: '/',
    component: () => import('../../1-Curriculo/views/home-view.vue'),
  },
  // Sobre Mim
  // {
  //   path: '/about-me',
  //   component:
  // },
  // Experiencias
  {
    path: '/experiencias',
    component: () => import('../../1-Curriculo/views/experiencia-view.vue'),
  },
  // Certificados
  {
    path: '/certificados',
    component: () => import('../../1-Curriculo/views/certificados-view.vue'),
  },
  // Financeiro
  // {
  //   path: '/financeiro/',
  //   component: () => import('../../2-Financeiro/views/HomeView.vue'),
  // },
  // Not Found
  {
    path: '/:catchAll(.*)',
    component: () => import('../../0-Global/views/not-found-view.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
