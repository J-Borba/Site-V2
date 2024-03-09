<script lang="ts" setup>
  import { faMedal, faUserGraduate } from '@fortawesome/free-solid-svg-icons';
  import { companies } from '../assets/mock/companies';
</script>

<template>
  <section>
    <h1 class="h3 color-secondary">Certificados</h1>
    <div class="certificados-container">
      <span v-for="(company, index) in companies" :key="index">
        <span class="d-flex gap-2 mb-2">
          <img :src="company.icon" alt="company icon" />
          <p class="h5 color-blue-100">{{ company.name }}</p>
        </span>
        <div class="d-flex flex-column gap-2" v-for="(degree, index) in company.degrees" :key="index">
          <span v-if="!!degree.title && !!degree.url">
            <a class="color-secondary" :href="degree.url" target="_blank">
              <font-awesome-icon class="icon" :icon="faUserGraduate" />
              {{ degree.title }}
            </a>
          </span>
          <span class="d-flex flex-column px-3" v-for="(certificate, index) in degree.certificates" :key="index">
            <a v-if="!!certificate.title && !!certificate.url" :href="certificate.url" target="_blank">
              <font-awesome-icon class="icon" :icon="faMedal" />
              {{ certificate.title }}
            </a>
          </span>
        </div>
      </span>
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

    border: 1px solid var(--blue-100);

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
