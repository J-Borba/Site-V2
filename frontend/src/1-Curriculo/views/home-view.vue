<script setup lang="ts">
  import { faFaceSmileWink, faHandPointRight, faIdCard } from '@fortawesome/free-regular-svg-icons';
</script>

<template>
  <article>
    <div class="d-flex align-items-center gap-3">
      <span id="my-foto-container">
        <a id="my-foto" href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
          <img src="../../0-Global/assets/my-foto.png" alt="Foto Joao Borba" />
        </a>
      </span>
      <h1 class="h2 color-blue-100">João Victor Borba</h1>
    </div>

    <div class="d-flex flex-column gap-3 px-2">
      <p>
        Estudante de Ciências da Computação na
        <a class="fw-bold" href="https://www.uerj.br/" target="_blank">
          Universidade do Estado do Rio de Janeiro (UERJ)
        </a>
      </p>

      <p>
        Desenvolvedor .NET na
        <a class="fw-bold" href="https://www.tiplan.com.br/" target="_blank"> TIPLAN </a>
      </p>

      <p>Amo desenvolver, e busco mais conhecimento a cada dia</p>

      <p>
        Confira meus
        <router-link to="certificados" class="fw-bold"> certificados </router-link>
        <font-awesome-icon class="icon" :icon="faFaceSmileWink" style="color: var(--text)" />
      </p>
    </div>

    <div class="d-flex flex-column align-items-center gap-3">
      <p class="h5">Mais sobre mim</p>
      <span>
        <font-awesome-icon class="icon" :icon="faHandPointRight" size="xl" />
        <router-link to="about-me">
          <font-awesome-icon id="id-card" :icon="faIdCard" size="lg" />
        </router-link>
      </span>
    </div>
  </article>
</template>

<style scoped lang="scss">
  article {
    display: flex;
    flex-direction: column;

    width: 100%;
    flex: 1;

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
      background: linear-gradient(100deg, var(--blue-100), var(--primary));

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
