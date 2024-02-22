<script setup lang="ts">
  import {
    IconDefinition,
    faArrowDownShortWide,
    faCaretDown,
    faFileContract,
    faTerminal,
    faUserGraduate,
  } from '@fortawesome/free-solid-svg-icons';
  import { RouterLink } from 'vue-router';

  interface iProjetos {
    title: string;
    rota: string;
  }
  const Projetos: iProjetos[] = [
    // {
    //   title: 'Financeiro',
    //   rota: 'financeiro',
    // },
  ];

  interface iNavigation {
    title: string;
    rota: string;
    icon: IconDefinition;
  }
  const Navigations: iNavigation[] = [
    {
      title: 'Certificados',
      rota: 'certificados',
      icon: faUserGraduate,
    },
    {
      title: 'Experiências',
      rota: 'experiencias',
      icon: faFileContract,
    },
  ];
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-lg px-3">
      <RouterLink to="/" class="navbar-brand" href="#">
        <img src="../assets/logo.svg" alt="Letras J e B cortadas no meio pelo meu nome escrito" />
      </RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNavDropdown"
        aria-controls="navbarNavDropdown"
        aria-expanded="false"
        aria-label="Toggle navigation">
        <font-awesome-icon class="icon" :icon="faArrowDownShortWide" size="2xl" />
      </button>
      <div class="collapse navbar-collapse" id="navbarNavDropdown">
        <ul class="navbar-nav gap-4 gap-md-2 mt-4 mt-md-0 pb-md-0 pb-2">
          <li class="dropdown" v-if="Projetos.length > 0">
            <a id="navbarDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              <font-awesome-icon class="icon" :icon="faTerminal" size="sm" />
              <span
                >Meus Projetos
                <font-awesome-icon :icon="faCaretDown" />
              </span>
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
              <li v-for="(projeto, index) in Projetos" :key="index">
                <RouterLink :to="projeto.rota" class="dropdown-item" href="#">
                  <span>{{ projeto.title }}</span>
                </RouterLink>
              </li>
            </ul>
          </li>
          <li class="nav-item" v-for="(navigation, index) in Navigations" :key="index">
            <RouterLink :to="navigation.rota" class="" href="#">
              <font-awesome-icon class="icon" :icon="navigation.icon" size="sm" />
              <span>{{ navigation.title }}</span>
            </RouterLink>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<style scoped lang="scss">
  header {
    width: 100%;
    position: absolute;
    top: 0;
  }
  img {
    filter: invert(82%) sepia(19%) saturate(333%) hue-rotate(147deg) brightness(86%) contrast(93%);
  }
</style>
