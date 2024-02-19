<script setup lang="ts">
  import {
    faArrowDownShortWide,
    faCaretDown,
    faFileContract,
    faHome,
    faTerminal,
    faUserGraduate,
  } from '@fortawesome/free-solid-svg-icons';
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-lg px-3">
      <a class="navbar-brand" href="#">MyImgHere</a>
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
        <ul class="navbar-nav gap-2">
          <li class="nav-item">
            <a class="" href="#">
              <font-awesome-icon class="icon" :icon="faHome" size="sm" />
              <span>Início</span>
            </a>
          </li>
          <li class="nav-item">
            <a class="" href="#">
              <font-awesome-icon class="icon" :icon="faUserGraduate" size="sm" />
              <span>Certificados</span>
            </a>
          </li>
          <li class="nav-item">
            <a class="" href="#">
              <font-awesome-icon class="icon" :icon="faFileContract" size="sm" />
              <span>Experiências</span>
            </a>
          </li>
          <li class="dropdown">
            <a
              class=""
              href="#"
              id="navbarDropdownMenuLink"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false">
              <font-awesome-icon class="icon" :icon="faTerminal" size="sm" />
              <span>Meus Projetos</span>
              <font-awesome-icon class="icon" :icon="faCaretDown" />
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
              <li><a class="dropdown-item" href="#">Financeiro</a></li>
              <li><a class="dropdown-item" href="#">Futuro Projeto</a></li>
            </ul>
          </li>
        </ul>
      </div>
    </nav>
  </header>
</template>

<style scoped lang="scss">
  nav {
    background-color: var(--primary);
  }
</style>
