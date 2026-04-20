<script setup lang="ts">
  withDefaults(
    defineProps<{
      label: string;
      visible?: boolean;
    }>(),
    {
      visible: true,
    },
  );
</script>

<template>
  <div :class="['terminal-card', { 'terminal-card--visible': visible }]">
    <div class="terminal-card-bar">
      <span class="terminal-dot terminal-dot--red"></span>
      <span class="terminal-dot terminal-dot--yellow"></span>
      <span class="terminal-dot terminal-dot--green"></span>
      <span class="terminal-card-label">{{ label }}</span>
    </div>
    <slot />
  </div>
</template>

<style scoped lang="scss">
  .terminal-card {
    width: 100%;
    max-width: min(480px, 100%);
    justify-self: center;
    border-radius: var(--radius-lg);
    overflow: hidden;
    background: var(--bg-card);
    border: 1px solid var(--border-strong);
    box-shadow:
      var(--shadow-lg),
      0 0 0 1px rgba(13, 216, 228, 0.06);
    opacity: 0;
    transform: translateY(-10px);
    transition:
      opacity 0.4s ease,
      transform 0.4s ease;

    &--visible {
      opacity: 1;
      transform: translateY(0);
    }
  }

  .terminal-card-bar {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    padding: var(--space-3) var(--space-5);
    background: rgba(255, 255, 255, 0.03);
    border-bottom: 1px solid var(--border);
  }

  .terminal-dot {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    flex-shrink: 0;

    &--red {
      background: #ff5f57;
    }
    &--yellow {
      background: #febc2e;
    }
    &--green {
      background: #28c840;
    }
  }

  .terminal-card-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    margin-left: auto;
    letter-spacing: 0.08em;
  }
</style>
