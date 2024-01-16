<template>
  <section>
    <div class="d-flex flex-column gap-4 my-5">
      <h1 class="h3 primary text-center">Experiências Profissionais</h1>
      <div class="bg-emphasis border border-secondary rounded-2 p-4">
        <div :id="job.sigla" v-for="job in jobs" :key="job.id">
          <p class="secondary fw-bold mb-2">{{ job.empresa }}</p>
          <div class="" v-for="experiencia in job.experiencias" :key="experiencia.cargo">
            <div class="d-flex justify-content-between primary small">
              <p>{{ experiencia.cargo }}</p>
              <p>{{ experiencia.periodo }}</p>
            </div>
            <div class="d-flex flex-column gap-2 mb-3 ms-3 small">
              <p v-for="line in experiencia.descricao" :key="line">{{ line }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
  import '../style/global.scss'

  const jobs = [
  {
      id: 2,
      empresa: "TIPLAN",
      sigla: 'TIPLAN',
      experiencias: [
        {
          cargo: "Estagiário - Desenvolvimento",
          periodo: "Out/2023 - Atualmente",
          descricao: [
            'Backend C# .NET',
            'Frontend Vue.js 2',
          ]
        },
      ]
    },
    {
      id: 1,
      empresa: "Associação Brasileira Beneficente de Reabilitação (ABBR)",
      sigla: 'ABBR',
      experiencias: [
        {
          cargo: "Estagiário - Analista",
          periodo: "Mar/2022 - Fev/2023",
          descricao: [
            'Suporte ao cliente',
            'Infraestrutura de TI',
            'Desenvolvimento e manutenção de dashboards/indicadores - Power BI',
            'Configurações Report Server do Power BI',
            'Querys em SQL',
            'Manutenção de planilhas em Excel',
            'Automatização de processos e serviços - Power Automate',
            'Uso do pacote Microsoft 365 (Antigo Office 365)',
            'Criação de metadados para sistema TOTVS Linha RM',
            'Gerenciar configurações dos sistemas TOTVS - Linha RM e Linha Protheus'
          ]
        },
        {
          cargo: "Analista de Sistemas",
          periodo: "Fev/2023 - Jun/2023",
          descricao: [
            'Todas as funções citadas acima como estagiário',
            'Desenvolvimento de ChatBot com TakeBlip',
            'Desenvolvimento de Scripts em JS para ChatBot',
            'Desenvolvimento de Sites com Vue.js 3',
          ]
        },
      ]
    }
  ]

</script>

<style lang="scss" scoped>
</style>