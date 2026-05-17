<script setup lang="ts">
  import PageShell from '@/0-Global/components/page-shell.vue';
  import ProjectCard from '../components/project-card.vue';
  import { ProjetosCatalog } from '../assets/utilities/projetos-catalog';
</script>

<template>
  <PageShell num="04" title="Meus<br />Projetos">
    <p class="intro" v-fade-up>
      Projetos pessoais e side-projects. Ferramentas que construí para resolver problemas reais — ou só porque queria aprender.
    </p>

    <div class="projects-grid" v-fade-up>
      <ProjectCard v-for="projeto in ProjetosCatalog" :key="projeto.slug" :projeto="projeto" />
    </div>
  </PageShell>
</template>

<style scoped lang="scss">
  .intro {
    font-size: var(--text-lg);
    color: var(--text-secondary);
    max-width: 60ch;
    line-height: 1.7;
  }

  .projects-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(360px, 1fr));
    gap: var(--space-6);

    @media (max-width: 480px) {
      grid-template-columns: 1fr;
    }
  }
</style>
