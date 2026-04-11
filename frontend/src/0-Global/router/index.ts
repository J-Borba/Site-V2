import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';

const routes: RouteRecordRaw[] = [
  { path: '/', component: () => import('@/1-Curriculo/views/home-view.vue') },
  { path: '/experiencias', component: () => import('@/1-Curriculo/views/experiencia-view.vue') },
  { path: '/certificados', component: () => import('@/1-Curriculo/views/certificados-view.vue') },
  { path: '/about-me', component: () => import('@/1-Curriculo/views/about-me-view.vue') },
  { path: '/:catchAll(.*)', component: () => import('@/0-Global/views/not-found-view.vue') },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
