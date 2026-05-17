<script setup lang="ts">
  import { formatBRL, formatPercent } from '../utils/financeiro-format';

  defineProps<{
    totalInvestido: number;
    valorAtual: number;
    ganhoPercent: number;
    proventosYTD: number;
  }>();
</script>

<template>
  <div class="summary-grid">
    <div class="kpi-card">
      <span class="kpi-label">$ total_investido</span>
      <span class="kpi-value">{{ formatBRL(totalInvestido) }}</span>
      <span class="kpi-sub">sum(qty × preço médio)</span>
    </div>

    <div class="kpi-card">
      <span class="kpi-label">$ valor_atual</span>
      <span class="kpi-value">{{ formatBRL(valorAtual) }}</span>
      <span class="kpi-sub">qty × último preço</span>
    </div>

    <div class="kpi-card" :class="ganhoPercent >= 0 ? 'kpi-card--up' : 'kpi-card--down'">
      <span class="kpi-label">$ rendimento</span>
      <span class="kpi-value gain">{{ formatPercent(ganhoPercent) }}</span>
      <span class="kpi-sub">{{ formatBRL(valorAtual - totalInvestido) }} bruto</span>
    </div>

    <div class="kpi-card kpi-card--gold">
      <span class="kpi-label">$ proventos_ytd</span>
      <span class="kpi-value gold">{{ formatBRL(proventosYTD) }}</span>
      <span class="kpi-sub">dividendos + JCP + rendimentos</span>
    </div>
  </div>
</template>

<style scoped lang="scss">
  .summary-grid {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: var(--space-4);

    @media (max-width: 900px) {
      grid-template-columns: repeat(2, 1fr);
    }

    @media (max-width: 480px) {
      grid-template-columns: 1fr;
    }
  }

  .kpi-card {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
    padding: var(--space-5) var(--space-6);
    background: var(--bg-card);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-lg);
    box-shadow: var(--shadow-md);
    transition:
      border-color 220ms ease,
      box-shadow 220ms ease;

    &:hover {
      border-color: var(--brand-border);
      box-shadow: var(--shadow-brand);
    }

    &--up {
      border-left: 2px solid var(--vue);
    }

    &--down {
      border-left: 2px solid var(--error);
    }

    &--gold {
      border-left: 2px solid var(--gold);
    }
  }

  .kpi-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    letter-spacing: 0.08em;
  }

  .kpi-value {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-2xl);
    font-weight: 800;
    color: var(--text-primary);
    line-height: 1.1;

    &.gain {
      .kpi-card--up & {
        color: var(--vue);
      }

      .kpi-card--down & {
        color: var(--error);
      }
    }

    &.gold {
      color: var(--gold);
    }
  }

  .kpi-sub {
    font-size: var(--text-xs);
    color: var(--text-muted);
  }
</style>
