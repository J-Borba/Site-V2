<template>
  <div id="loading" v-if="isLoading">
    <span class="loader"></span>
  </div>
  <slot v-else></slot>
</template>

<style lang="scss" scoped>
  #loading {
    display: flex;
    width: 100%;
    height: 90svh;

    justify-content: center;
  }
  .loader {
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    max-width: 6rem;
    margin-top: 3rem;
    margin-bottom: 3rem;
  }
  .loader:before,
  .loader:after {
    content: '';
    position: absolute;
    border-radius: 50%;
    animation: pulsOut 1.5s ease-in-out infinite;
    filter: drop-shadow(0 0 1rem var(--secondary));
  }
  .loader:before {
    width: 100%;
    padding-bottom: 100%;
    box-shadow: inset 0 0 0 1rem var(--secondary);
    animation-name: pulsIn;
  }
  .loader:after {
    width: calc(100% - 2rem);
    padding-bottom: calc(100% - 2rem);
    box-shadow: 0 0 0 0 var(--secondary);
  }

  @keyframes pulsIn {
    0% {
      box-shadow: inset 0 0 0 1rem var(--secondary);
      opacity: 1;
    }
    50%,
    100% {
      box-shadow: inset 0 0 0 0 var(--secondary);
      opacity: 0;
    }
  }

  @keyframes pulsOut {
    0%,
    50% {
      box-shadow: 0 0 0 0 var(--secondary);
      opacity: 0;
    }
    100% {
      box-shadow: 0 0 0 1rem var(--secondary);
      opacity: 1;
    }
  }
</style>

<script setup>
  import { ref } from 'vue';

  let isLoading = ref(true);
  setTimeout(() => {
    isLoading.value = false;
  }, 800);
</script>
