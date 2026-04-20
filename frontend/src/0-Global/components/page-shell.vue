<script setup lang="ts">
  defineProps<{
    num: string;
    title: string;
    gap?: string;
  }>();
</script>

<template>
  <section class="page-shell">
    <div class="page-container" :style="gap ? { gap } : {}">
      <div class="page-header">
        <span class="page-num">{{ num }}</span>
        <h1 class="section-title" v-html="title" />
      </div>

      <slot />
    </div>
  </section>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/mixins' as mx;

  .page-shell {
    flex: 1;
    padding: var(--space-16) var(--space-6);
  }

  .page-container {
    @include mx.page-container;
    gap: var(--space-12);
  }

  .page-header {
    position: relative;
  }

  .page-num {
    @include mx.page-num-style;
  }
</style>
