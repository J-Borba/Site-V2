<script setup lang="ts">
  interface Props {
    src: string;
  }

  defineProps<Props>();
</script>

<template>
  <iframe
    class="maps-frame"
    :src="src"
    width="400"
    height="200"
    style="border: 0"
    loading="lazy"
    referrerpolicy="no-referrer-when-downgrade" />
</template>

<style scoped lang="scss">
  .maps-frame {
    border-radius: var(--radius-md);
    width: min(100%, 560px);
    max-width: 100%;
  }
</style>
