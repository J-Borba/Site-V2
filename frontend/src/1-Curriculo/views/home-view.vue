<script setup lang="ts">
  import { faHandPointDown } from '@fortawesome/free-regular-svg-icons';
</script>

<template>
  <article>
    <span class="d-flex justify-content-center align-items-center gap-3">
      <a id="my-foto" href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
        <img src="../../0-Global/assets/my-foto.png" alt="Foto Joao Borba" />
      </a>
      <h1 class="h2">João Victor Borba</h1>
    </span>

    <div class="d-flex flex-column w-75 gap-3">
      <p>Estudante de Ciências da Computação na <strong>Universidade do Estado do Rio de Janeiro (UERJ)</strong></p>

      <p>Desenvolvedor .NET na <a href="https://www.tiplan.com.br/" target="_blank">TIPLAN</a></p>

      <p>Amo desenvolver, e busco mais conhecimento a cada dia</p>

      <p>Confira meus <router-link to="/certificados">certificados</router-link> 😉</p>

      <p class="h5">
        Mais sobre mim <span><font-awesome-icon class="icon" :icon="faHandPointDown" bounce size="xl" /> </span>
      </p>
    </div>
  </article>
</template>

<style scoped lang="scss">
  article {
    display: flex;
    flex-direction: column;

    justify-content: center;
    align-items: center;

    gap: 3rem;
  }

  #my-foto {
    display: flex;
    border-radius: 50%;

    justify-content: center;

    & img {
      width: 10rem;
      border-radius: inherit;
    }
  }
</style>
