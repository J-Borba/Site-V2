<template>
  <header id="MyHeader">
    <nav class="navbar sticky-top">
      <div class="container-fluid">
        <!-- Icone para centralizar logo com spacebetween -->
        <button class="navbar-toggler opacity-0" type="button" disabled>
          <span class="baseThemeBtn">
            <font-awesome-icon :icon="['fas', 'circle-chevron-down']" />
          </span>
        </button>
        <!-- End - Icone para centralizar logo com spacebetween -->
        <router-link class="logoRouter" to="/">
          <img src="../assets/myLogoNoBg.png" class="logo" alt="">
        </router-link>
        <button class="navbar-toggler shadow-none" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasDarkNavbar" aria-controls="offcanvasDarkNavbar">
          <span class="baseThemeBtn">
            <font-awesome-icon :icon="['fas', 'circle-chevron-down']" />
          </span>
        </button>
        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasDarkNavbar"
          aria-labelledby="offcanvasDarkNavbarLabel">
          <div class="offcanvas-header">
            <router-link to="/">
              <font-awesome-icon :icon="['fas', 'house']" data-bs-dismiss="offcanvas"/>
            </router-link>
            <h5 class="offcanvas-title" id="offcanvasDarkNavbarLabel">Navegação</h5>
            <a href="#" class="pe-auto" data-bs-dismiss="offcanvas" aria-label="Close">
              <font-awesome-icon :icon="['fas', 'xmark']" size="xl"/>
            </a>
          </div>
          <div class="offcanvas-body">
            <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
              <li class="nav-item">
                <div class="d-flex gap-2">
                  <router-link class="nav-link" to="/curriculo/home">
                    <p data-bs-dismiss="offcanvas">
                      Currículo
                    </p>
                  </router-link>
                  <a class="nav-link dropdown-toggle dropdown-toggle-split" href="#" role="button"
                    data-bs-toggle="dropdown" aria-expanded="false">
                    <span class="visually-hidden">Toggle Dropdown</span>
                  </a>
                  <ul class="dropdown-menu" data-bs-dismiss="offcanvas">
                    <li>
                      <router-link to="/curriculo/experiencia">
                        Experiência
                      </router-link>
                    </li>
                    <li>
                      <hr class="dropdown-divider">
                    </li>
                    <li>
                      <router-link to="/curriculo/conhecimentos">
                        Conhecimentos
                      </router-link>
                    </li>
                  </ul>
                </div>
              </li>
              <li class="nav-item">
                <div class="d-flex gap-2">
                  <router-link class="nav-link" to="/financeiro/home">
                    <p data-bs-dismiss="offcanvas">
                      Financeiro
                    </p>
                  </router-link>
                  <a class="nav-link dropdown-toggle dropdown-toggle-split" href="#" role="button"
                    data-bs-toggle="dropdown" aria-expanded="false">
                    <span class="visually-hidden">Toggle Dropdown</span>
                  </a>
                  <ul class="dropdown-menu dropdown-menu-dark" data-bs-dismiss="offcanvas">
                    <li>
                      <router-link to="/financeiro/home">
                        Home
                      </router-link>
                    </li>
                  </ul>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<style lang="scss" scoped>
  nav {
    a {
      &:not(.router-link-exact-active) {
        &:hover {
          color: var(--primary);
          opacity: 1;
        }
      }
      &.router-link-exact-active {
        p {
          border-bottom: 1px solid var(--secondary);
        }
        
        &:hover {
          cursor: not-allowed;
          color: var(--text);
        }
      }
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
  .logo {
    width: 6rem;
  }
  .logoRouter {
    border: none;
    &.router-link-exact-active:hover {
      cursor: pointer;
    }
  }
  .offcanvas {
    background-color: var(--bg);
    color: var(--text);

    a {
      &.router-link-exact-active {
        opacity: 0.7;
      }
    }
  }
  .dropdown-menu {
    background-color: var(--bg-emphasis);
    li {
      margin-left: 1rem;
    }
  }

  @media (max-width: 1168px) {
    .logo {
      width: 4rem;
    }
  }
</style>