<script setup lang="ts">
  import { Ref, computed, onMounted, ref } from 'vue';
  import { AxiosError } from 'axios';
  import {
    faBookBookmark,
    faCircleInfo,
    faTriangleExclamation,
    faArrowUpRightFromSquare,
  } from '@fortawesome/free-solid-svg-icons';
  import { githubApi } from '@/0-Global/services/api.js';
  import MyLoading from '@/0-Global/components/my-loading.vue';
  import GoogleMaps from '../components/google-maps.vue';
  import { myGithub, tiplan, uerj } from '@/0-Global/assets/utilities/_variables';

  interface IRepos {
    id: number;
    name: string;
    private: boolean;
    html_url: string;
    description?: string;
    language?: string;
  }

  const repos: Ref<IRepos[]> = ref<IRepos[]>([]);
  const loading: Ref<boolean> = ref<boolean>(true);
  const error: Ref<AxiosError | undefined> = ref<AxiosError>();
  const notInformedText: string = 'Não informado';

  async function getRepos() {
    await githubApi
      .get('repos?sort=updated')
      .then((api) => {
        repos.value = api.data;
      })
      .catch((apiError) => {
        error.value = apiError;
      })
      .finally(() => {
        loading.value = false;
      });
  }

  onMounted(() => {
    getRepos();
  });

  const filteredRepos = computed(() => {
    return repos.value.filter((repo) => repo.name !== 'J-Borba').slice(0, 6);
  });
</script>

<template>
  <div class="about-page">
    <div class="page-container">
      <div class="page-header">
        <span class="page-num">03</span>
        <h1 class="section-title">Sobre Mim</h1>
      </div>

      <section v-fade-up class="info-section">
        <div class="section-header">
          <h2 class="section-subtitle">Última Experiência</h2>
          <router-link to="/experiencias" class="section-more">
            <font-awesome-icon :icon="faCircleInfo" />
            <span>Ver todas</span>
          </router-link>
        </div>

        <div class="info-card">
          <GoogleMaps
            src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14700.985908340814!2d-43.178263!3d-22.904278!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997f5fdc3e9565%3A0xe09773af0e23b78e!2sTIPLAN!5e0!3m2!1spt-BR!2sbr!4v1709422911840!5m2!1spt-BR!2sbr" />
          <div class="card-body">
            <a :href="tiplan.url" target="_blank" class="card-company">{{ tiplan.title }}</a>
            <p class="card-role">Desenvolvedor de Software Jr</p>
            <p class="card-date">Jan/2025 — Atualmente</p>
          </div>
        </div>
      </section>

      <section v-fade-up class="info-section">
        <h2 class="section-subtitle">Formação Acadêmica</h2>

        <div class="info-card">
          <GoogleMaps
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d58800.952966794444!2d-43.31234995136716!3d-22.9111739!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997e66c5f330ad%3A0x874ed5a98cf472d1!2sUniversidade%20do%20Estado%20do%20Rio%20de%20Janeiro!5e0!3m2!1spt-BR!2sbr!4v1709481881887!5m2!1spt-BR!2sbr" />
          <div class="card-body">
            <a :href="uerj.url" target="_blank" class="card-company">{{ uerj.title }} ({{ uerj.shortTitle }})</a>
            <p class="card-role">Ciências da Computação</p>
            <p class="card-date">Set/2020 — Cursando</p>
          </div>
        </div>
      </section>

      <section v-fade-up class="info-section">
        <h2 class="section-subtitle">Projetos</h2>

        <MyLoading v-if="loading" />

        <div v-else-if="error" class="error-banner">
          <font-awesome-icon :icon="faTriangleExclamation" />
          <div>
            <p class="error-code">{{ error.code }}</p>
            <p class="error-msg">{{ error.message }}</p>
          </div>
        </div>

        <div v-else class="repos-grid">
          <a v-for="repo in filteredRepos" :key="repo.id" :href="repo.html_url" target="_blank" class="repo-card">
            <div class="repo-top">
              <font-awesome-icon :icon="faBookBookmark" class="repo-book-icon" />
              <span class="repo-name">{{ repo.name }}</span>
              <font-awesome-icon :icon="faArrowUpRightFromSquare" class="repo-ext-icon" />
            </div>
            <p class="repo-desc">{{ repo.description ?? notInformedText }}</p>
            <p :data-lang="repo.language" class="repo-lang">
              {{ repo.language ?? notInformedText }}
            </p>
          </a>
        </div>

        <a :href="myGithub.url" target="_blank" class="btn-brand"> Ver todos os projetos </a>
      </section>
    </div>
  </div>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/mixins' as mx;

  .about-page {
    flex: 1;
    padding: var(--space-16) var(--space-6);
  }

  .page-container {
    @include mx.page-container;
    gap: var(--space-16);
  }

  .page-header {
    position: relative;
  }

  .page-num {
    @include mx.page-num-style;
  }

  .info-section {
    display: flex;
    flex-direction: column;
    gap: var(--space-6);
  }

  .section-header {
    display: flex;
    align-items: flex-end;
    justify-content: space-between;
    gap: var(--space-4);
  }

  .section-subtitle {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-2xl);
    font-weight: 700;
    color: var(--text-primary);
    letter-spacing: -0.02em;
  }

  .section-more {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    font-size: var(--text-sm);
    color: var(--text-muted);
    width: fit-content;
    transition: color 150ms ease;
    padding-bottom: var(--space-1);

    &:hover {
      color: var(--brand);
      opacity: 1 !important;
    }
  }

  .info-card {
    @include mx.card-interactive(0px);
    display: flex;
    align-items: flex-start;
    gap: var(--space-6);
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    padding: var(--space-5) var(--space-6);

    @media (max-width: 640px) {
      flex-direction: column;
    }
  }

  .card-body {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
    padding-top: var(--space-2);
  }

  .card-company {
    font-family: 'Syne', sans-serif;
    font-weight: 700;
    font-size: var(--text-lg);
    color: var(--text-primary);
    width: fit-content;
    transition: color 150ms ease;

    &:hover {
      color: var(--brand);
      opacity: 1 !important;
    }
  }

  .card-role {
    font-size: var(--text-base);
    color: var(--brand);
    font-weight: 500;
  }

  .card-date {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-secondary);
    letter-spacing: 0.04em;
  }

  .repos-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: var(--space-4);
    width: 100%;

    @media (max-width: 640px) {
      grid-template-columns: repeat(2, 1fr);
    }

    @media (max-width: 400px) {
      grid-template-columns: 1fr;
    }
  }

  .repo-card {
    @include mx.card-interactive(-3px);
    display: flex;
    flex-direction: column;
    gap: var(--space-3);
    padding: var(--space-5);
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: var(--radius-md);
    width: 100%;
    opacity: 1 !important;

    .repo-ext-icon {
      opacity: 0;
      transition: opacity 150ms ease;
    }

    &:hover .repo-ext-icon {
      opacity: 1;
    }
  }

  .repo-top {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    min-width: 0;
  }

  .repo-book-icon {
    color: var(--text-muted);
    font-size: 0.9rem;
    flex-shrink: 0;
  }

  .repo-name {
    font-family: 'Space Mono', monospace;
    font-weight: 700;
    font-size: var(--text-xs);
    color: var(--brand);
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    flex: 1;
    letter-spacing: 0.02em;
  }

  .repo-ext-icon {
    color: var(--text-muted);
    font-size: 0.7rem;
    flex-shrink: 0;
  }

  .repo-desc {
    font-size: var(--text-xs);
    color: var(--text-secondary);
    line-height: 1.5;
    flex: 1;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
  }

  .repo-lang {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    display: flex;
    align-items: center;
    gap: var(--space-2);
    letter-spacing: 0.02em;

    &::before {
      content: '●';
      font-size: 0.55em;
    }
  }

  [data-lang='Vue'] {
    &::before {
      color: var(--lang-vue);
    }
  }
  [data-lang='TypeScript'] {
    &::before {
      color: var(--lang-ts);
    }
  }
  [data-lang='JavaScript'] {
    &::before {
      color: var(--lang-js);
    }
  }
  [data-lang='C#'] {
    &::before {
      color: var(--lang-cs);
    }
  }

  .error-banner {
    display: flex;
    align-items: flex-start;
    gap: var(--space-3);
    padding: var(--space-5);
    background: rgba(227, 179, 65, 0.08);
    border: 1px solid rgba(227, 179, 65, 0.25);
    border-radius: var(--radius-md);
    color: var(--warning);
    font-size: var(--text-sm);
  }

  .error-code {
    font-family: 'Space Mono', monospace;
    font-weight: 700;
    font-size: var(--text-xs);
  }

  .error-msg {
    color: var(--text-secondary);
    margin-top: var(--space-1);
  }
</style>
