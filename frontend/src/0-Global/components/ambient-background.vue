<script setup lang="ts">
  withDefaults(
    defineProps<{
      variant?: 'app' | 'auth' | 'hero';
      showOrbs?: boolean;
      showGrid?: boolean;
      showNoise?: boolean;
    }>(),
    {
      variant: 'app',
      showOrbs: true,
      showGrid: false,
      showNoise: false,
    },
  );
</script>

<template>
  <div :class="['ambient-bg', `ambient-bg--${variant}`]" aria-hidden="true">
    <template v-if="showOrbs">
      <div class="ambient-orb ambient-orb--cyan"></div>
      <div class="ambient-orb ambient-orb--indigo"></div>
    </template>
    <div v-if="showGrid" class="ambient-grid"></div>
    <div v-if="showNoise" class="ambient-noise"></div>
  </div>
</template>

<style scoped lang="scss">
  .ambient-bg {
    position: absolute;
    inset: 0;
    pointer-events: none;
    z-index: 0;
    overflow: hidden;
  }

  @keyframes ambient-orb-drift {
    from {
      transform: translate(0, 0) scale(1);
    }
    to {
      transform: translate(24px, -24px) scale(1.12);
    }
  }

  .ambient-orb {
    position: absolute;
    border-radius: 50%;
    filter: blur(90px);
    opacity: 0.18;

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  }

  .ambient-orb--cyan {
    background: radial-gradient(circle, rgba(13, 216, 228, 0.7) 0%, transparent 70%);
    animation: ambient-orb-drift 22s ease-in-out infinite alternate;
  }

  .ambient-orb--indigo {
    background: radial-gradient(circle, rgba(90, 60, 200, 0.6) 0%, transparent 70%);
    animation: ambient-orb-drift 28s ease-in-out infinite alternate-reverse;
  }

  .ambient-bg--app {
    .ambient-orb--cyan {
      width: 580px;
      height: 580px;
      top: -200px;
      right: -80px;
    }

    .ambient-orb--indigo {
      width: 500px;
      height: 500px;
      top: calc(100svh - 320px);
      left: -60px;
    }
  }

  .ambient-bg--auth {
    .ambient-orb {
      opacity: 0.22;
      filter: blur(80px);
    }

    .ambient-orb--cyan {
      width: 500px;
      height: 500px;
      top: -180px;
      right: -100px;
      animation-duration: 16s;
    }

    .ambient-orb--indigo {
      width: 400px;
      height: 400px;
      bottom: -150px;
      left: -80px;
      animation-duration: 20s;
    }
  }

  .ambient-bg--hero {
    .ambient-orb {
      display: none;
    }
  }

  .ambient-grid {
    position: absolute;
    inset: 0;
    background-image: radial-gradient(rgba(255, 255, 255, 0.05) 1px, transparent 1px);
    background-size: 32px 32px;
    mask-image: radial-gradient(ellipse 70% 70% at 50% 50%, black 20%, transparent 80%);
  }

  .ambient-bg--auth .ambient-grid {
    background-image: radial-gradient(rgba(255, 255, 255, 0.04) 1px, transparent 1px);
    mask-image: radial-gradient(ellipse 70% 70% at 50% 50%, black 10%, transparent 80%);
  }

  .ambient-noise {
    position: absolute;
    inset: 0;
    background-image: url("data:image/svg+xml,%3Csvg viewBox='0 0 256 256' xmlns='http://www.w3.org/2000/svg'%3E%3Cfilter id='noise'%3E%3CfeTurbulence type='fractalNoise' baseFrequency='0.9' numOctaves='4' stitchTiles='stitch'/%3E%3C/filter%3E%3Crect width='100%25' height='100%25' filter='url(%23noise)' opacity='0.035'/%3E%3C/svg%3E");
    background-repeat: repeat;
    background-size: 200px;
    opacity: 0.35;
    mix-blend-mode: overlay;
    pointer-events: none;
  }
</style>
