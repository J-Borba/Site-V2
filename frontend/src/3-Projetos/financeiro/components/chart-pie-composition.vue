<script setup lang="ts">
  import { ref, computed } from 'vue';
  import VueApexCharts from 'vue3-apexcharts';
  import type { ApexOptions } from 'apexcharts';
  import { formatBRL } from '../utils/financeiro-format';

  const props = defineProps<{
    porCodigo: { label: string; value: number }[];
    porTipo: { label: string; value: number }[];
    porSetor: { label: string; value: number }[];
  }>();

  type Key = 'codigo' | 'tipo' | 'setor';

  const active = ref<Key>('tipo');

  const toggles: { key: Key; label: string }[] = [
    { key: 'tipo', label: 'por tipo' },
    { key: 'setor', label: 'por setor' },
    { key: 'codigo', label: 'por ativo' },
  ];

  const dataMap = computed(() => ({
    codigo: props.porCodigo,
    tipo: props.porTipo,
    setor: props.porSetor,
  }));

  const current = computed(() => dataMap.value[active.value]);
  const labels = computed(() => current.value.map((d) => d.label));
  const series = computed(() => current.value.map((d) => d.value));
  const hasData = computed(() => series.value.length > 0 && series.value.some((v) => v > 0));

  const PALETTE = ['#0dd8e4', '#e8b84b', '#42b883', '#a855f7', '#f85149', '#38e8f2', '#3178c6', '#f1e05a'];

  const options = computed((): ApexOptions => ({
    chart: {
      type: 'donut',
      background: 'transparent',
      fontFamily: "'Space Mono', monospace",
      foreColor: '#40495a',
      toolbar: { show: false },
      animations: { enabled: true, speed: 350 },
    },
    colors: PALETTE,
    labels: labels.value,
    legend: {
      position: 'bottom',
      fontFamily: "'Space Mono', monospace",
      fontSize: '11px',
      labels: { colors: '#8892a4' },
      markers: { size: 7 },
    },
    plotOptions: {
      pie: {
        donut: {
          size: '68%',
          labels: {
            show: true,
            total: {
              show: true,
              label: 'portfólio',
              fontFamily: "'Space Mono', monospace",
              fontSize: '11px',
              color: '#40495a',
              formatter: (w: { globals: { seriesTotals: number[] } }) => {
                const total = w.globals.seriesTotals.reduce((a, b) => a + b, 0);
                return formatBRL(total);
              },
            },
            value: {
              fontFamily: "'Syne', sans-serif",
              fontSize: '18px',
              fontWeight: 800,
              color: '#ecf0f6',
              formatter: (v: string) => formatBRL(Number(v)),
            },
          },
        },
      },
    },
    dataLabels: {
      enabled: true,
      formatter: (val: number) => `${val.toFixed(1)}%`,
      style: { fontFamily: "'Space Mono', monospace", fontSize: '10px', fontWeight: '400' },
      dropShadow: { enabled: false },
    },
    stroke: { colors: ['#141827'], width: 2 },
    tooltip: {
      theme: 'dark',
      style: { fontFamily: "'Space Mono', monospace", fontSize: '11px' },
      y: { formatter: (v: number) => formatBRL(v) },
    },
  }));
</script>

<template>
  <div class="chart-card">
    <div class="chart-bar-header">
      <span class="dot dot--red"></span>
      <span class="dot dot--yellow"></span>
      <span class="dot dot--green"></span>
      <span class="chart-title">composição do portfólio</span>
      <div class="toggle-group">
        <button
          v-for="t in toggles"
          :key="t.key"
          class="toggle-btn"
          :class="{ active: active === t.key }"
          @click="active = t.key"
        >
          {{ t.label }}
        </button>
      </div>
    </div>

    <div class="chart-body">
      <div v-if="!hasData" class="no-data">$ sem dados cadastrados.</div>
      <VueApexCharts v-else type="donut" height="300" :options="options" :series="series" />
    </div>
  </div>
</template>

<style scoped lang="scss">
  .chart-card {
    background: var(--bg-card);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-lg);
    overflow: hidden;
  }

  .chart-bar-header {
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    gap: var(--space-2);
    padding: var(--space-3) var(--space-5);
    background: rgba(255, 255, 255, 0.03);
    border-bottom: 1px solid var(--border);
  }

  .dot {
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

  .chart-title {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    margin-left: var(--space-1);
    letter-spacing: 0.06em;
    flex: 1;
  }

  .toggle-group {
    display: flex;
    gap: var(--space-1);
  }

  .toggle-btn {
    font-family: 'Space Mono', monospace;
    font-size: 10px;
    color: var(--text-muted);
    padding: 2px var(--space-3);
    border: 1px solid var(--border);
    border-radius: var(--radius-sm);
    letter-spacing: 0.04em;
    transition:
      color 150ms ease,
      border-color 150ms ease,
      background 150ms ease;

    &:hover {
      color: var(--text-secondary);
      border-color: var(--border-strong);
    }

    &.active {
      color: var(--brand);
      border-color: var(--brand-border);
      background: var(--brand-dim);
    }
  }

  .chart-body {
    padding: var(--space-4) var(--space-2);
  }

  .no-data {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 300px;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
  }
</style>
