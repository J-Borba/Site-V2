<script setup lang="ts">
  import { computed } from 'vue';
  import VueApexCharts from 'vue3-apexcharts';
  import type { ApexOptions } from 'apexcharts';
  import { formatBRL } from '../utils/financeiro-format';

  const props = defineProps<{
    data: { mes: string; valor: number }[];
  }>();

  const MESES = ['jan', 'fev', 'mar', 'abr', 'mai', 'jun', 'jul', 'ago', 'set', 'out', 'nov', 'dez'];

  function fmtMes(key: string) {
    const [y, m] = key.split('-');
    return `${MESES[Number(m) - 1]}/${y.slice(2)}`;
  }

  const categories = computed(() => props.data.map((d) => fmtMes(d.mes)));
  const values = computed(() => props.data.map((d) => d.valor));
  const hasData = computed(() => values.value.length > 0);

  const options = computed((): ApexOptions => ({
    chart: {
      type: 'bar',
      background: 'transparent',
      fontFamily: "'Space Mono', monospace",
      foreColor: '#40495a',
      toolbar: { show: false },
      animations: { enabled: true, speed: 350 },
    },
    colors: ['#0dd8e4'],
    plotOptions: {
      bar: {
        borderRadius: 4,
        columnWidth: '55%',
      },
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        type: 'vertical',
        gradientToColors: ['rgba(13, 216, 228, 0.3)'],
        stops: [0, 100],
      },
    },
    xaxis: {
      type: 'category',
      categories: categories.value,
      labels: {
        style: { fontFamily: "'Space Mono', monospace", fontSize: '10px', colors: '#40495a' },
      },
      axisBorder: { color: 'rgba(255,255,255,0.07)' },
      axisTicks: { color: 'rgba(255,255,255,0.07)' },
    },
    yaxis: {
      labels: {
        style: { fontFamily: "'Space Mono', monospace", fontSize: '10px', colors: '#40495a' },
        formatter: (v: number) => formatBRL(v),
      },
    },
    grid: {
      borderColor: 'rgba(255,255,255,0.05)',
      strokeDashArray: 4,
    },
    tooltip: {
      theme: 'dark',
      style: { fontFamily: "'Space Mono', monospace", fontSize: '11px' },
      y: { formatter: (v: number) => formatBRL(v) },
    },
    dataLabels: { enabled: false },
  }));

  const series = computed(() => [{ name: 'Proventos', data: values.value }]);
</script>

<template>
  <div class="chart-card">
    <div class="chart-bar-header">
      <span class="dot dot--red"></span>
      <span class="dot dot--yellow"></span>
      <span class="dot dot--green"></span>
      <span class="chart-title">proventos mensais</span>
    </div>

    <div class="chart-body">
      <div v-if="!hasData" class="no-data">$ nenhum provento registrado.</div>
      <VueApexCharts v-else :key="categories.join()" type="bar" height="280" :options="options" :series="series" />
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
  }

  .chart-body {
    padding: var(--space-4) var(--space-2);
  }

  .no-data {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 280px;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
  }
</style>
