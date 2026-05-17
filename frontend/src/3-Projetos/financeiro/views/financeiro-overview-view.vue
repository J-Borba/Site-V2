<script setup lang="ts">
  import { useFinanceiroStore } from '../store/financeiro-store';
  import SummaryCards from '../components/summary-cards.vue';
  import ChartPieComposition from '../components/chart-pie-composition.vue';
  import ChartBarProventos from '../components/chart-bar-proventos.vue';

  const store = useFinanceiroStore();
</script>

<template>
  <div class="overview">
    <SummaryCards
      v-fade-up
      :total-investido="store.kpis.totalInvestido"
      :valor-atual="store.kpis.valorAtual"
      :ganho-percent="store.kpis.ganhoPercent"
      :proventos-y-t-d="store.kpis.proventosYTD"
    />

    <div class="charts-top" v-fade-up>
      <ChartPieComposition
        :por-codigo="store.kpis.composicaoPorCodigo"
        :por-tipo="store.kpis.composicaoPorTipo"
        :por-setor="store.kpis.composicaoPorSetor"
      />
      <ChartBarProventos :data="store.kpis.proventosMensais" />
    </div>
  </div>
</template>

<style scoped lang="scss">
  .overview {
    display: flex;
    flex-direction: column;
    gap: var(--space-6);
  }

  .charts-top {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: var(--space-4);

    @media (max-width: 700px) {
      grid-template-columns: 1fr;
    }
  }
</style>
