import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from '@/2-Auth/store/auth-store';

declare module 'vue-router' {
  interface RouteMeta {
    requiresAuth?: boolean;
    guestOnly?: boolean;
  }
}

const routes: RouteRecordRaw[] = [
  { path: '/', component: () => import('@/0-Global/views/home-view.vue') },
  { path: '/experiencias', component: () => import('@/1-Curriculo/views/experiencia-view.vue') },
  { path: '/certificados', component: () => import('@/1-Curriculo/views/certificados-view.vue') },
  { path: '/about-me', component: () => import('@/1-Curriculo/views/about-me-view.vue') },
  { path: '/login', component: () => import('@/2-Auth/views/login-view.vue'), meta: { guestOnly: true } },
  { path: '/register', component: () => import('@/2-Auth/views/register-view.vue'), meta: { guestOnly: true } },
  { path: '/profile', component: () => import('@/2-Auth/views/profile-view.vue'), meta: { requiresAuth: true } },
  { path: '/projetos', component: () => import('@/3-Projetos/views/projetos-index-view.vue') },
  {
    path: '/projetos/financeiro',
    component: () => import('@/3-Projetos/financeiro/views/financeiro-shell-view.vue'),
    meta: { requiresAuth: true },
    children: [
      { path: '', component: () => import('@/3-Projetos/financeiro/views/financeiro-overview-view.vue') },
      { path: 'posicoes', component: () => import('@/3-Projetos/financeiro/views/financeiro-posicoes-view.vue') },
      { path: 'proventos', component: () => import('@/3-Projetos/financeiro/views/financeiro-proventos-view.vue') },
    ],
  },
  { path: '/:catchAll(.*)', component: () => import('@/0-Global/views/not-found-view.vue') },
];

const router = createRouter({
  scrollBehavior() {
    return { top: 0 };
  },
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to) => {
  const authStore = useAuthStore();

  await authStore.initSession();

  if (to.meta.requiresAuth && !authStore.isLoggedIn) {
    return { path: '/login', query: { redirect: to.fullPath } };
  }

  if (to.meta.guestOnly && authStore.isLoggedIn) {
    return '/profile';
  }
});

export default router;
