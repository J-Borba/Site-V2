<script setup lang="ts">
  import { onMounted } from 'vue';
  import { RouterView, RouterLink } from 'vue-router';
  import PageShell from '@/0-Global/components/page-shell.vue';
  import { useFinanceiroStore } from '../store/financeiro-store';

  const store = useFinanceiroStore();

  onMounted(() => {
    store.loadAll();
  });

  const tabs = [
    { label: '~/overview', to: '/projetos/financeiro', exact: true },
    { label: '~/posicoes', to: '/projetos/financeiro/posicoes', exact: false },
    { label: '~/proventos', to: '/projetos/financeiro/proventos', exact: false },
  ];
</script>

<template>
  <PageShell num="04" title="Financeiro" gap="var(--space-6)">
    <nav class="fin-tabs">
      <RouterLink
        v-for="tab in tabs"
        :key="tab.to"
        :to="tab.to"
        class="fin-tab"
        :exact-active-class="'fin-tab--active'"
        :active-class="tab.exact ? '' : 'fin-tab--active'"
      >
        {{ tab.label }}
      </RouterLink>
    </nav>

    <div v-if="store.loading" class="loading-line">
      <span class="loading-prompt">$ carregando portfólio...</span>
    </div>

    <RouterView v-else />
  </PageShell>
</template>

<style scoped lang="scss">
  .fin-tabs {
    display: flex;
    gap: 0;
    border-bottom: 1px solid var(--border-strong);
    overflow-x: auto;

    &::-webkit-scrollbar {
      display: none;
    }
  }

  .fin-tab {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    padding: var(--space-3) var(--space-5);
    border-bottom: 2px solid transparent;
    margin-bottom: -1px;
    white-space: nowrap;
    letter-spacing: 0.06em;
    transition:
      color 150ms ease,
      border-color 150ms ease,
      background 150ms ease;

    &:hover {
      color: var(--text-secondary);
      background: var(--brand-dim);
    }

    &--active {
      color: var(--brand) !important;
      border-bottom-color: var(--brand);
      background: var(--brand-dim);
    }
  }

  .loading-line {
    padding: var(--space-10) 0;
  }

  .loading-prompt {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
    animation: blink 1s step-end infinite;
  }

  @keyframes blink {
    50% {
      opacity: 0.3;
    }
  }
</style>
