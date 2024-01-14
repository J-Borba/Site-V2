<template>
  <div class="d-flex justify-content-center w-100">
    <footer class="footer-color border-top-radius-16 d-flex justify-content-center gap-3 p-3 w-50">
      <a @click="handleClickFooter">
        <font-awesome-icon class="footer-icon" :icon="['fas', 'turn-up']" size="lg" style="color: var(--secondary)"/>
      </a>
      <a href="https://github.com/J-Borba" target="_blank">
        <font-awesome-icon class="footer-icon" :icon="['fab', 'github']" size="xl"/>
      </a>
      <a href="mailto:joao3borba3@gmail.com" target="_blank">
        <font-awesome-icon class="footer-icon" :icon="['far', 'envelope']" size="xl"/>
      </a>
      <a href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
        <font-awesome-icon class="footer-icon" :icon="['fab', 'linkedin']" size="xl"/>
      </a>
    </footer>
  </div>
</template>

<script setup>
  function handleClickFooter() {
    document.documentElement.scrollTop = 0;
  }
</script>

<style lang="scss" scoped>
  p {
    line-height: 1.6;
    font-weight: bold;
  }
  .border-top-radius-16 {    
    border-top-left-radius: 16px;
    border-top-right-radius: 16px;
  }
  .footer-color {
    background-color: var(--primary);
    color: var(--footer-text) !important;
  }
  .footer-icon {
    font-size: 1.5rem !important;
  }
  a {
    color: var(--footer-text);
    cursor: pointer;
    border: 0;

    transition: transform 1.5s;
  }
  a:hover {
    transform: translateY(-15%);
    transition: transform 150ms;
  }
</style>