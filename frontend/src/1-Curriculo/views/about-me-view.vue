<script setup lang="ts">
  import { computed, onMounted, ref } from 'vue';
  import { AxiosError } from 'axios';
  import { faBookBookmark, faCircleInfo, faWarning } from '@fortawesome/free-solid-svg-icons';
  import { faGitAlt, faHtml5, faReact, faSass, faSquareJs, faVuejs } from '@fortawesome/free-brands-svg-icons';
  import { githubApi } from '@/0-Global/services/api.js';
  import MyLoading from '@/0-Global/components/my-loading.vue';
  import GoogleMaps from '../components/google-maps.vue';

  interface IRepos {
    id: number;
    name: string;
    private: boolean;
    html_url: string;
    description: string;
    language: string;
  }

  const loading = ref<boolean>(true);
  const repos = ref<IRepos[]>([]);
  const error = ref<AxiosError>();

  async function getRepos() {
    await githubApi
      .get('repos?sort=updated')
      .then((api) => {
        repos.value = JSON.parse(api.request.response);
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
    return repos.value.filter((repo) => repo.name != 'J-Borba');
  });
</script>

<template>
  <section>
    <span class="d-flex justify-content-center align-items-center gap-2">
      <p class="h2 color-secondary">Última Experiência</p>
      <router-link to="/experiencias">
        <font-awesome-icon class="icon" :icon="faCircleInfo" size="lg" />
      </router-link>
    </span>

    <GoogleMaps
      src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14700.985908340814!2d-43.178263!3d-22.904278!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997f5fdc3e9565%3A0xe09773af0e23b78e!2sTIPLAN!5e0!3m2!1spt-BR!2sbr!4v1709422911840!5m2!1spt-BR!2sbr" />

    <div>
      <a class="fs-5 color-secondary" href="https://www.tiplan.com.br/" target="_blank"> TIPLAN </a>
      <p class="color-blue-100">Desenvolvedor de Software</p>
      <p>Out/2023 - Atualmente</p>
    </div>
  </section>
  <section>
    <p class="h2 color-secondary">Formação Acadêmica</p>

    <GoogleMaps
      src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d58800.952966794444!2d-43.31234995136716!3d-22.9111739!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997e66c5f330ad%3A0x874ed5a98cf472d1!2sUniversidade%20do%20Estado%20do%20Rio%20de%20Janeiro!5e0!3m2!1spt-BR!2sbr!4v1709481881887!5m2!1spt-BR!2sbr" />

    <div>
      <a class="fs-5 color-secondary" href="https://www.uerj.br/" target="_blank"> UERJ </a>
      <p>Set/2020 - Cursando</p>
    </div>
  </section>
  <section>
    <p class="h2 color-secondary">Hard Skills</p>

    <div class="d-flex justify-content-center flex-wrap gap-4">
      <font-awesome-icon :icon="faVuejs" size="2xl" />
      <font-awesome-icon :icon="faReact" size="2xl" />
      <font-awesome-icon :icon="faSquareJs" size="2xl" />
      <font-awesome-icon :icon="faHtml5" size="2xl" />
      <font-awesome-icon :icon="faSass" size="2xl" />
      <font-awesome-icon :icon="faGitAlt" size="2xl" />
    </div>
    <div class="d-flex gap-1">
      <span>veja mais aqui</span>
      <router-link to="/experiencias">
        <font-awesome-icon class="icon" :icon="faCircleInfo" size="lg" />
      </router-link>
    </div>
  </section>
  <section>
    <p class="h2 color-secondary">Projetos</p>

    <MyLoading v-if="loading" />

    <div class="alert alert-danger px-5" role="alert" v-else-if="!!error">
      <span class="d-flex gap-1 align-items-center">
        <font-awesome-icon class="icon" :icon="faWarning" size="lg" />
        <p class="h5 alert-heading">
          {{ error.code }}
        </p>
      </span>
      <p>
        {{ error.message }}
      </p>
    </div>

    <div class="d-flex flex-wrap text-start justify-content-center w-75 gap-4" v-else>
      <span v-for="repo in filteredRepos" :key="repo.id" class="github-card">
        <a :href="repo.html_url" class="repo-title" target="_blank">
          <font-awesome-icon class="book-icon" :icon="faBookBookmark" />
          {{ repo.name }}
        </a>
        <p class="repo-desc">
          {{ repo.description ?? 'Descrição vazia' }}
        </p>
        <p :id="repo.language" class="repo-lang">
          {{ repo.language }}
        </p>
      </span>
    </div>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex-direction: column;

    padding: 5rem 0;

    justify-content: center;
    align-items: center;
    text-align: center;

    gap: 2rem;
  }

  $vue-clr: #41b883;
  $ts-clr: #3178c6;
  $js-clr: #f1e05a;
  $java-clr: #b07219;
  $csharp-clr: #178600;
  $border-clr: #b4bac0;
  $book-icon-clr: #768390;

  .github-card {
    display: flex;
    flex-direction: column;
    justify-content: space-around;

    padding: 0.5rem;
    width: 17rem;
    height: 8rem;

    border-radius: 8px;
    border: 1px solid $border-clr;

    box-shadow: 1px 1px 8px 1px var(--primary);

    line-height: 1.25;

    & p {
      font-size: 0.8rem;
    }
  }

  .repo-title {
    display: flex;
    align-items: center;

    gap: 0.25rem;
  }

  .book-icon {
    color: $book-icon-clr;
  }

  .repo-lang {
    display: flex;
    gap: 0.2rem;

    &::before {
      content: '●';
    }
  }

  #Vue {
    &::before {
      color: $vue-clr;
    }
  }
  #TypeScript {
    &::before {
      color: $ts-clr;
    }
  }
  #JavaScript {
    &::before {
      color: $js-clr;
    }
  }
  #Java {
    &::before {
      color: $java-clr;
    }
  }
  #C\# {
    &::before {
      color: $csharp-clr;
    }
  }
</style>
