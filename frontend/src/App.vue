<script setup lang="ts">
  import MyFooter from './0-Global/components/my-footer.vue';
  import MyHeader from './0-Global/components/my-header.vue';
  import { RouterView } from 'vue-router';
</script>

<template>
  <div class="app-shell">
    <MyHeader />
    <main>
      <RouterView />
    </main>
    <MyFooter />
  </div>
</template>

<style lang="scss">
  .app-shell {
    display: flex;
    flex-direction: column;
    min-height: 100svh;
  }

  main {
    flex: 1;
    display: flex;
    flex-direction: column;
  }
</style>
