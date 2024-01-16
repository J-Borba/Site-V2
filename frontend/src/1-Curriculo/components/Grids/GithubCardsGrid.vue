<template>
  <section class="mb-lg-0 mb-5">
    <div>
      <p class="h2 text-center mb-5">Projetos</p>
      <div class="row w-75 m-auto justify-content-center gap-4">
        <div class="col-2 github-card" v-for="repo in repos" :key="repo.id">
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
              {{ repo.description ?? "Descrição vazia" }}
            </p>
            <p class="repo-lang" :id="repo.language">
              {{ repo.language }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
  import '../../style/global.scss'
  import { onMounted, ref } from 'vue';
  import api from '@/0-Global/services/api.js'

  const repos = ref([])

  async function getRepos() {
    const response = await api.get('/repos?sort=updated')

    if (response) {
      repos.value = JSON.parse(response.request.response)
    }
  }
  onMounted(() => {
    getRepos();
  })
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
    p{
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