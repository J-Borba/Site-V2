<script setup lang="ts">
  import { faGithub, faLinkedin } from '@fortawesome/free-brands-svg-icons';
  import { faEnvelope } from '@fortawesome/free-regular-svg-icons';
  import { faJetFighterUp } from '@fortawesome/free-solid-svg-icons';
  const handleClickFooter = () => {
    document.documentElement.scrollTop = 0;
  };
</script>

<template>
  <footer>
    <aside class="d-flex flex-column justify-content-between gap-3">
      <div>
        <p class="h1">João Borba</p>
        <p>Desenvolvedor Web | .NET</p>
      </div>
      <div class="copyright">
        <p>&copy; Copyright. All rights reserved.</p>
        <a class="ms-1 fw-bold" href="https://joaoborba.com" target="_blank">joaoborba.com</a>
      </div>
    </aside>
    <div class="text-center">
      <p class="h4 mb-2">Quick links</p>
      <nav class="d-flex flex-column gap-1">
        <a href="#" @click="handleClickFooter">
          <font-awesome-icon class="icon" :icon="faJetFighterUp" size="lg" />
          <span>Voltar ao início</span>
        </a>
        <a href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
          <font-awesome-icon class="icon" :icon="faLinkedin" size="lg" />
          <span>LinkedIn</span>
        </a>
        <a href="https://github.com/J-Borba" target="_blank">
          <font-awesome-icon class="icon" :icon="faGithub" size="lg" />
          <span>Github</span>
        </a>
        <a href="mailto:joao3borba3@gmail.com" target="_blank">
          <font-awesome-icon class="icon" :icon="faEnvelope" size="lg" />
          <span>Mail</span>
        </a>
      </nav>
    </div>
  </footer>
</template>

<style scoped lang="scss">
  footer {
    display: flex;
    justify-content: space-around;

    padding: 1rem 0;

    background-color: var(--primary);

    border-top-left-radius: 16px;
    border-top-right-radius: 16px;
  }

  a {
    display: flex;
    gap: 0.25rem;

    color: inherit;
  }

  .copyright {
    display: flex;
  }

  @media only screen and (max-width: 576px) {
    footer {
      flex-direction: column;
      align-items: center;
      gap: 2rem;

      text-align: center;
    }
    .copyright {
      flex-direction: column;
      align-items: center;
    }
  }

  @media (prefers-color-scheme: light) {
    footer {
      color: var(--bg-emphasis);
    }
  }
</style>
