<script setup lang="ts">
  import { faWhatsapp } from '@fortawesome/free-brands-svg-icons';
</script>

<template>
  <a href="https://wa.me/+5521981920082" target="_blank">
    <font-awesome-icon :icon="faWhatsapp" size="2xl" />
  </a>
</template>

<style scoped lang="scss">
  a {
    display: flex;
    justify-content: center;
    align-items: center;

    background: linear-gradient(var(--bg-whatsapp-top), var(--bg-whatsapp-bottom));
    color: var(--white);

    position: fixed;
    bottom: 20px;
    right: 20px;

    border-radius: 50%;
    width: 3.275rem !important;
    aspect-ratio: 1;

    animation: whatsapp-button-animation-pulse 1.25s ease-out infinite;
    animation-delay: 500ms;
  }
  @keyframes whatsapp-button-animation-pulse {
    0% {
      box-shadow: 0 0 0 0 var(--bg-whatsapp-top);
    }
    80% {
      box-shadow: 0 0 0 14px var(--bg-whatsapp-no-opacity);
    }
  }
</style>
