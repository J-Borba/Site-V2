<script setup lang="ts">
  interface Props {
    src: string;
  }

  defineProps<Props>();
</script>

<template>
  <iframe
    class="rounded-2 w-75"
    :src="src"
    width="400"
    height="200"
    style="border: 0"
    loading="lazy"
    referrerpolicy="no-referrer-when-downgrade" />
</template>

<style scoped lang="scss"></style>
