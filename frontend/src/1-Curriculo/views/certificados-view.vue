<script lang="ts" setup>
  import { faMedal, faUserGraduate, faWarning } from '@fortawesome/free-solid-svg-icons';
  import { CertificateCompanies } from '../assets/utilities/certificateCompanies';

  function warningAlert() {
    window.alert('Esta formação ainda está em andamento.'); //TODO - trocar por um modal
  }
</script>

<template>
  <section>
    <h1 class="h3 color-secondary">Certificados</h1>
    <div class="certificados-container">
      <div v-for="(company, index) in CertificateCompanies" :key="index">
        <!-- <Informacoes da Empresa> -->
        <span class="d-flex gap-2 mb-2">
          <img :src="company.icon" alt="company icon" />
          <p class="h5 color-secondary">{{ company.name }}</p>
        </span>
        <!-- </Informacoes da Empresa> -->

        <!-- <Formacoes e Certificados> -->
        <div class="d-flex flex-column gap-2">
          <div class="d-flex flex-column gap-1" v-for="(degree, index) in company.degrees" :key="index">
            <!-- <Formacoes> -->
            <span v-if="!!degree.title">
              <a class="text-muted" :href="degree.url ?? '#'" target="_blank">
                <font-awesome-icon class="icon" :icon="faUserGraduate" />
                {{ degree.title }}
              </a>
              <font-awesome-icon
                v-if="!degree.url"
                class="icon warning"
                :icon="faWarning"
                title="Formação em andamento."
                @click="warningAlert" />
            </span>
            <!-- </Formacoes> -->

            <!-- <Certificados> -->
            <span class="d-flex flex-column px-3" v-for="(certificate, index) in degree.certificates" :key="index">
              <a v-if="!!certificate.title && !!certificate.url" :href="certificate.url" target="_blank">
                <font-awesome-icon class="icon" :icon="faMedal" />
                {{ certificate.title }}
              </a>
            </span>
            <!-- </Certificados> -->
          </div>
        </div>
        <!-- </Formacoes e Certificados> -->
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex-direction: column;
    flex: 1;

    justify-content: center;
    align-items: center;

    gap: 1rem;
  }

  .certificados-container {
    display: flex;
    flex-direction: column;
    gap: 2rem;

    border: 1px solid var(--secondary);

    border-radius: 6px;

    padding: 1rem;

    overflow-y: auto !important;
    max-height: 60svh;
  }

  img {
    width: 1.5rem;
    aspect-ratio: 1;
  }
</style>
../assets/utilities/certificateCompanies ../assets/utilities/certificateCompanies
