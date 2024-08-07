<script setup lang="ts">
  import { faArrowDownShortWide, faCaretDown, faTerminal } from '@fortawesome/free-solid-svg-icons';
  import { RouterLink } from 'vue-router';
  import { Projetos } from '@/0-Global/assets/mock/projetos';
  import { Navigations } from '@/0-Global/assets/mock/navigations';
  import logo from '@/0-Global/assets/images/logo.png';
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-lg px-3">
      <RouterLink to="/" class="navbar-brand" href="#" tabindex="1">
        <img :src="logo" alt="Letras J e B cortadas no meio pelo meu nome escrito" />
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
        <ul class="navbar-nav gap-2 mt-4 mt-lg-0 pb-2 pb-lg-0">
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
  img {
    width: 7rem;
  }

  @media only screen and (max-width: 768px) {
    img {
      width: 5rem;
    }
  }
</style>
