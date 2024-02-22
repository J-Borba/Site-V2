<script setup lang="ts">
  import MyFooter from './0-Global/components/my-footer.vue';
  import MyHeader from './0-Global/components/my-header.vue';
  import WhatsappButton from './0-Global/components/whatsapp-button.vue';
  import { RouterView } from 'vue-router';
</script>

<template>
  <main>
    <MyHeader />
    <RouterView />
  </main>
  <WhatsappButton />
  <MyFooter />
</template>

<style scoped lang="scss">
  main {
    display: flex;
    flex-direction: column;

    justify-content: center;
    align-items: center;
  }
</style>
