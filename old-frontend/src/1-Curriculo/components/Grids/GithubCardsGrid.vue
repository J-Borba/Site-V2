<template>
  <section class="mb-lg-0 mb-5">
    <div class="d-flex flex-column align-items-center">
      <p class="h2 primary text-center mb-5">Projetos</p>
      <div class="d-flex flex-wrap justify-content-center w-75 gap-4">
        <div v-for="repo in filteredRepos" :key="repo.id" class="github-card">
          <div class="github-card-content">
            <p class="repo-title">
              <a :href="repo.html_url" target="_blank">
                <div class="title-wrapper">
                  <font-awesome-icon :icon="['fas', 'book-bookmark']" class="book-icon" />
                  <span>
                    {{ repo.name }}
                  </span>
                </div>
              </a>
            </p>
            <p class="repo-desc">
              {{ repo.description ?? 'Descrição vazia' }}
            </p>
            <p :id="repo.language" class="repo-lang">
              {{ repo.language }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
  import '../../style/global.scss';
  import { computed, onMounted, ref } from 'vue';
  import { githubApi } from '@/0-Global/services/api.js';

  const repos = ref([]);

  async function getRepos() {
    await githubApi
      .get('repos?sort=updated')
      .then((response) => {
        repos.value = JSON.parse(response.request.response);
      })
      .catch((error) => {
        console.log(error);
      });
  }
  onMounted(() => {
    getRepos();
  });

  const filteredRepos = computed(() => {
    return repos.value.filter((repo) => repo.name != 'J-Borba');
  });
</script>

<style lang="scss" scoped>
  $title-clr: #0969da;
  $vue-clr: #41b883;
  $ts-clr: #3178c6;
  $js-clr: #f1e05a;
  $java-clr: #b07219;
  $csharp-clr: #178600;
  $border-clr: #b4bac0;
  $book-icon-clr: #768390;

  .github-card {
    padding: 0.5rem;
    width: 17rem;
    height: 8rem;

    border-radius: 8px;
    border: 1px solid $border-clr;

    box-shadow: 1px 1px 8px 1px var(--primary);
  }
  .github-card-content {
    display: flex;
    flex-direction: column;
    justify-content: space-around;

    height: 100%;
    line-height: 1;
    p {
      font-size: 0.9rem;
    }
  }
  .repo-title {
    span {
      font-size: 1.1rem;
      color: $title-clr;
      font-weight: 400;
      &:hover {
        text-decoration: underline;
      }
    }
  }
  .book-icon {
    font-size: 0.9rem;
    color: $book-icon-clr;
  }
  .repo-lang {
    &::before {
      content: '● ';
      font-size: 1.1rem;
    }
  }
  .title-wrapper {
    display: flex;
    align-items: center;
    gap: 0.5rem;
  }
  a:hover {
    opacity: 1 !important;
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
