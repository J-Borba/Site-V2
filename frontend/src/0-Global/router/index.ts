import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from '@/2-Auth/store/auth-store';

declare module 'vue-router' {
  interface RouteMeta {
    requiresAuth?: boolean;
    guestOnly?: boolean;
  }
}

const routes: RouteRecordRaw[] = [
  { path: '/', component: () => import('@/1-Curriculo/views/home-view.vue') },
  { path: '/experiencias', component: () => import('@/1-Curriculo/views/experiencia-view.vue') },
  { path: '/certificados', component: () => import('@/1-Curriculo/views/certificados-view.vue') },
  { path: '/about-me', component: () => import('@/1-Curriculo/views/about-me-view.vue') },
  { path: '/login', component: () => import('@/2-Auth/views/login-view.vue'), meta: { guestOnly: true } },
  { path: '/register', component: () => import('@/2-Auth/views/register-view.vue'), meta: { guestOnly: true } },
  { path: '/profile', component: () => import('@/2-Auth/views/profile-view.vue'), meta: { requiresAuth: true } },
  { path: '/:catchAll(.*)', component: () => import('@/0-Global/views/not-found-view.vue') },
];

const router = createRouter({
  scrollBehavior(to, from, savedPosition) {
    return { top: 0 };
  },
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to) => {
  const authStore = useAuthStore();

  await authStore.initSession();

  if (to.meta.requiresAuth && !authStore.isLoggedIn) {
    return '/login';
  }

  if (to.meta.guestOnly && authStore.isLoggedIn) {
    return '/profile';
  }
});

export default router;
