<template>
  <footer class="border-top-radius-16 footer-color">
    <div class="py-4">
      <div class="row justify-content-center gap-5">
        <div class="col-lg-4 col-md-12 text-lg-start text-center d-flex justify-content-center">
          <div class="d-flex flex-column justify-content-around w-auto">
            <div>
              <p class="h1">João Borba</p>
              <p>
                Desenvolvedor Web | .NET
              </p>
            </div>
            <p>
              &copy; Copyright. All rights reserved. <a class="fw-bold" href="https://joaoborba.com" target="_blank">joaoborba.com</a>
            </p>
          </div>
        </div>
        <div class="col-lg-2 col-md-12 text-lg-start text-center d-flex justify-content-center">
          <div class="w-auto">
            <p class="h4 mb-3">Quick links</p>
            <div class="d-flex flex-column gap-2">
              <a href="#" @click="handleClickFooter">
                <font-awesome-icon :icon="['fas', 'jet-fighter-up']" size="xl" />
                <span>Voltar ao início</span>
              </a>
              <a href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['fab', 'linkedin']" size="xl" /> 
                <span>LinkedIn</span>
              </a>
              <a href="https://github.com/J-Borba" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['fab', 'github']" size="xl" /> 
                <span>Github</span>
              </a>
              <a href="mailto:joao3borba3@gmail.com" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['far', 'envelope']" size="xl" /> 
                <span>Mail</span>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>
<script setup>
  function handleClickFooter() {
    document.documentElement.scrollTop = 0;
  }
</script>

<style lang="scss" scoped>
  .border-top-radius-16 {    
    border-top-left-radius: 16px;
    border-top-right-radius: 16px;
  }
  .footer-color {
    background-color: var(--secondary);

    * {
      color: var(--footer-text) !important;
    }
  }
  .footer-icon {
    font-size: 1.5rem !important;
  }
  a {
    width: fit-content;
    span {
      margin-left: 0.25rem;
    }
  }
</style>