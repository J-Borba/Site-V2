<script setup lang="ts"></script>

<template>
  <div class="loader">
    <span class="loader-ring" role="status" aria-label="Carregando..."></span>
    <span class="loader-text">carregando...</span>
  </div>
</template>

<style scoped lang="scss">
  .loader {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    color: var(--text-secondary);
    font-size: var(--text-sm);
  }

  .loader-ring {
    width: 1.25rem;
    height: 1.25rem;
    border: 2px solid var(--border);
    border-top-color: var(--brand);
    border-radius: 50%;
    flex-shrink: 0;
    animation: spin 0.7s linear infinite;
  }

  @keyframes spin {
    to {
      transform: rotate(360deg);
    }
  }
</style>
