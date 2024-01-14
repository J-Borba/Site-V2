<template>
  <footer class="w-100 border-top-radius-16 footer-color">
    <div class="py-4">
      <div class="row justify-content-center gap-5">
        <div class="col-lg-4 col-md-12 text-lg-start text-center d-flex flex-column justify-content-around">
          <h1 class="">João Borba</h1>
          <p class="small text-muted">
            Desenvolvedor Web | .NET
          </p>
          <p class="small text-muted mb-0">
            &copy; Copyrights. All rights reserved. <a href="https://joaoborba.com" target="_blank">joaoborba.com</a>
          </p>
        </div>
        <div class="col-lg-2 col-md-12 text-lg-start text-center">
          <h5 class="mb-3">Quick links</h5>
          <ul class="list-unstyled">
            <li>
              <a href="#" @click="handleClickFooter">
                <font-awesome-icon :icon="['fas', 'jet-fighter-up']" size="xl"/> Voltar ao início
              </a>
            </li>
            <li>
              <a href="https://github.com/J-Borba" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['fab', 'github']" size="xl" /> Github
              </a>
            </li>
            <li>
              <a href="mailto:joao3borba3@gmail.com" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['far', 'envelope']" size="xl"/> Mail
              </a>
            </li>
            <li>
              <a href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
                <font-awesome-icon class="footer-icon" :icon="['fab', 'linkedin']" size="xl"/> LinkedIn
              </a>
            </li>
          </ul>
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
  ul {
    display: flex;
    flex-direction: column;
    
    gap: 0.5rem;
    
    li{
      a{
        font-weight: 500;
        font-size: small;
      }
    }
  }
</style>