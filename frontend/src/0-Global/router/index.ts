import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import HomeView from '../../1-Curriculo/views/home-view.vue';
import ExperienciaView from '../../1-Curriculo/views/experiencia-view.vue';
import CertificadosView from '../../1-Curriculo/views/certificados-view.vue';
import NotFoundView from '../views/not-found-view.vue';
import AboutMeView from '../../1-Curriculo/views/about-me-view.vue';

const routes: RouteRecordRaw[] = [
  { path: '/', component: HomeView },
  { path: '/experiencias', component: ExperienciaView },
  { path: '/certificados', component: CertificadosView },
  { path: '/about-me', component: AboutMeView },
  { path: '/:catchAll(.*)', component: NotFoundView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
