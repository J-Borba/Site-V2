<template>
  <header id="MyHeader">
    <nav class="navbar sticky-top mx-3">
      <div class="container-fluid">
        <!-- Icone para centralizar logo com spacebetween -->
        <button class="navbar-toggler opacity-0" type="button" aria-label="disabled button" disabled>
          <span class="baseThemeBtn">
            <font-awesome-icon :icon="['fas', 'circle-chevron-down']" class="offcanvas-btn" />
          </span>
        </button>
        <!-- End - Icone para centralizar logo com spacebetween -->
        <router-link id="logoHeader" to="/">
          <img src="../assets/myLogoNoBg.png" class="logoRouter" alt="Logo Joao Borba" />
        </router-link>
        <button
          aria-label="Offcanvas button"
          class="navbar-toggler shadow-none"
          type="button"
          data-bs-toggle="offcanvas"
          data-bs-target="#offcanvasDarkNavbar"
          aria-controls="offcanvasDarkNavbar">
          <span class="baseThemeBtn">
            <font-awesome-icon :icon="['fas', 'circle-chevron-down']" size="xl" class="offcanvas-btn" />
          </span>
        </button>
        <div
          class="offcanvas offcanvas-end"
          tabindex="-1"
          id="offcanvasDarkNavbar"
          aria-labelledby="offcanvasDarkNavbarLabel">
          <div class="offcanvas-header">
            <router-link to="/">
              <font-awesome-icon :icon="['fas', 'house']" data-bs-dismiss="offcanvas" />
            </router-link>
            <p class="h5 offcanvas-title" id="offcanvasDarkNavbarLabel">Navegação</p>
            <a href="#" class="pe-auto" data-bs-dismiss="offcanvas" aria-label="Close">
              <font-awesome-icon :icon="['fas', 'xmark']" size="xl" />
            </a>
          </div>
          <div class="offcanvas-body">
            <nav
              class="navbar-nav justify-content-end flex-grow-1 pe-3"
              v-for="(item, index) in offcanvasItems"
              :key="index">
              <div class="nav-item">
                <div class="d-flex gap-2">
                  <router-link class="nav-link" :to="{ path: item.route }">
                    <p data-bs-dismiss="offcanvas">{{ item.title }}</p>
                  </router-link>
                  <a
                    v-if="item.children.length"
                    class="nav-link dropdown-toggle dropdown-toggle-split"
                    href="#"
                    role="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false">
                    <span class="visually-hidden">Toggle Dropdown</span>
                  </a>
                  <div class="dropdown-menu p-3" data-bs-dismiss="offcanvas">
                    <div v-for="(child, childIndex) in item.children" :key="childIndex">
                      <router-link :to="{ path: child.route }"> {{ child.title }} </router-link>
                      <hr v-if="childIndex < item.children.length" class="dropdown-divider mt-2" />
                    </div>
                  </div>
                </div>
              </div>
            </nav>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<script setup>
  const offcanvasItems = [
    {
      title: 'Currículo',
      route: '/curriculo/home',
      children: [
        {
          title: 'Experiência',
          route: '/curriculo/experiencia',
        },
        {
          title: 'Conhecimentos',
          route: '/curriculo/conhecimentos',
        },
        {
          title: 'Certificados',
          route: '/curriculo/certificados',
        },
      ],
    },
    {
      title: 'Financeiro',
      route: '/financeiro/home',
      children: [],
    },
  ];
</script>

<style lang="scss" scoped>
  #logoHeader {
    &:hover {
      opacity: 1 !important;
    }
    &:focus {
      opacity: 1 !important;
    }
  }
  nav {
    a {
      color: var(--text);
      &:hover {
        color: var(--text);
      }
      &:not(.router-link-exact-active) {
        &:hover,
        &:focus {
          color: var(--secondary);
          opacity: 1;
        }
      }
      &.router-link-exact-active:not(#logoHeader) {
        opacity: 0.7;
        p {
          border-bottom: 1px solid var(--secondary);
        }
        &:hover {
          cursor: not-allowed;
        }
      }
    }
    button {
      border: none;
    }
  }
  .baseThemeBtn {
    background-color: transparent;
    color: var(--primary);
    border: none;
    line-height: 0;

    transition: 0.2s ease;

    &:hover {
      color: var(--secondary);
    }
  }
  .logoRouter {
    min-width: 6rem;
    width: 7dvw;
    aspect-ratio: 1.7;
  }
  .offcanvas {
    font-size: 1.4rem !important;
    background-color: var(--bg);
    color: var(--text);
  }
  .dropdown-menu {
    font-size: 1.2rem !important;
    background-color: var(--bg-emphasis);
    text-align: center;
  }
</style>
