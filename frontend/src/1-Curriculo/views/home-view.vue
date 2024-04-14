<script setup lang="ts">
  import { faFaceSmileWink, faHandPointRight, faIdCard } from '@fortawesome/free-regular-svg-icons';
  import foto from '@/0-Global/assets/images/my-foto.png';
  import { tiplan, uerj } from '@/0-Global/assets/utilities/_variables';
</script>

<template>
  <section>
    <article class="d-flex align-items-center gap-3">
      <span id="my-foto-container">
        <a id="my-foto" href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
          <img :src="foto" alt="Foto Joao Borba" />
        </a>
      </span>
      <h1 class="h2 color-secondary">João Victor Borba</h1>
    </article>

    <article class="d-flex flex-column gap-3 px-2">
      <p>
        Estudante de Ciências da Computação na
        <a class="fw-bold" :href="uerj.url" target="_blank"> {{ uerj.title }} ({{ uerj.shortTitle }}) </a>
      </p>

      <p>
        Desenvolvedor .NET na
        <a class="fw-bold" :href="tiplan.url" target="_blank"> {{ tiplan.title }} </a>
      </p>

      <p>Amo desenvolver, e busco mais conhecimento a cada dia</p>

      <p>
        Confira meus
        <router-link to="certificados" class="fw-bold"> certificados </router-link>
        <font-awesome-icon class="icon" :icon="faFaceSmileWink" style="color: var(--text)" />
      </p>
    </article>

    <article class="d-flex flex-column align-items-center gap-3">
      <p class="h5">Mais sobre mim</p>
      <span>
        <font-awesome-icon class="icon" :icon="faHandPointRight" size="xl" />
        <router-link to="about-me">
          <font-awesome-icon id="id-card" :icon="faIdCard" size="lg" />
        </router-link>
      </span>
    </article>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex-direction: column;
    flex: 1;

    width: 100%;

    justify-content: space-around;
    align-items: center;

    gap: 3rem;
  }

  span {
    display: flex;

    justify-content: center;
    align-items: center;

    gap: 1rem;
  }

  .icon {
    color: var(--secondary);
  }

  #my-foto-container {
    padding: 0.2rem;
    border-radius: 50%;

    position: relative;

    overflow: hidden;
    &::before {
      content: '';

      position: absolute;
      z-index: -1;
      inset: -10px 40px;
      background: linear-gradient(100deg, var(--secondary), var(--primary));

      transition: 1s;
      animation: spin 10s linear infinite;
    }
    &:hover::before {
      transition: 300ms;
      inset: -10px 0;
    }
  }

  #my-foto {
    display: flex;
    border-radius: 50%;
    width: 10rem;

    justify-content: center;

    opacity: 1 !important;

    & img {
      width: 100%;
      border-radius: inherit;
    }
  }

  #id-card {
    padding: 0.125rem;
    &:hover {
      animation: flip 1s ease forwards;
      animation-delay: 200ms;
    }
  }

  @keyframes spin {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(360deg);
    }
  }

  @keyframes flip {
    0% {
      transform: rotateY(0deg);
    }
    50% {
      transform: rotateY(180deg);
    }
    100% {
      transform: rotateY(0deg);
    }
  }

  @media only screen and (max-width: 576px) {
    #my-foto-container {
      width: 7rem;
      &::before {
        inset: -10px 20px;
      }
    }
  }
</style>
@/0-Global/assets/utilities/_variables
