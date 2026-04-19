<script setup lang="ts">
  import MyFooter from './0-Global/components/my-footer.vue';
  import MyHeader from './0-Global/components/my-header.vue';
  import { RouterView } from 'vue-router';
</script>

<template>
  <div class="app-shell">
    <div class="app-orbs" aria-hidden="true">
      <div class="app-orb app-orb--cyan"></div>
      <div class="app-orb app-orb--indigo"></div>
    </div>
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
    position: relative;
  }

  .app-orbs {
    position: absolute;
    inset: 0;
    pointer-events: none;
    z-index: 0;
    overflow: hidden;
  }

  @keyframes orb-drift {
    from { transform: translate(0, 0) scale(1); }
    to   { transform: translate(24px, -24px) scale(1.12); }
  }

  .app-orb {
    position: absolute;
    border-radius: 50%;
    filter: blur(90px);
    opacity: 0.18;

    &--cyan {
      width: 580px;
      height: 580px;
      top: -200px;
      right: -80px;
      background: radial-gradient(circle, rgba(13, 216, 228, 0.7) 0%, transparent 70%);
      animation: orb-drift 22s ease-in-out infinite alternate;

      @media (prefers-reduced-motion: reduce) {
        animation: none;
      }
    }

    &--indigo {
      width: 500px;
      height: 500px;
      top: calc(100svh - 320px);
      left: -60px;
      background: radial-gradient(circle, rgba(90, 60, 200, 0.6) 0%, transparent 70%);
      animation: orb-drift 28s ease-in-out infinite alternate-reverse;

      @media (prefers-reduced-motion: reduce) {
        animation: none;
      }
    }
  }

  main {
    flex: 1;
    display: flex;
    flex-direction: column;
  }
</style>
