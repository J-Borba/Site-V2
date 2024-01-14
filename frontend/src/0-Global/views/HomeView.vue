<template>
  <section class="text-center flex-column justify-content-around">  
    <div class="jumbotron rounded-4 p-5">
        <h1 class="display-4">Seja bem vindo!</h1>
        <p class="lead">
          Este é meu portfólio repaginado! 
        </p>
        <hr class="my-4">
        <p>Aqui você pode acessar minhas informações curriculares e profissionais</p>
        <p>assim como meus futuros projetos!</p>
        <p class="lead">
          <a class="btn btn-lg" href="#" 
          data-bs-toggle="offcanvas" data-bs-target="#offcanvasDarkNavbar" aria-controls="offcanvasDarkNavbar">
            Começe a Navegar
          </a>
        </p>
    </div>
  </section>
</template>

<script setup>
</script>

<style lang="scss" scoped>
  .jumbotron {
    background-color: var(--bg-emphasis);
  }
  h1, h2, h3, h4, h5, h6 {
    color: var(--primary);
  }
  .btn {
    background-color: var(--secondary);
    color: var(--footer-text);
  }
</style>
