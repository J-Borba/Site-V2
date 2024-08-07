<script setup lang="ts">
  import { faBuilding } from '@fortawesome/free-regular-svg-icons';
  import { Jobs } from '../assets/mock/jobs';
  import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
</script>

<template>
  <section class="gap-5 gap-lg-4">
    <h1 class="h3 color-secondary mb-5">Experiências Profissionais</h1>

    <div class="jobs-container" v-for="(job, index) in Jobs" :key="index">
      <a :href="job.companyUrl" class="company-image" :title="job.companyName">
        <img :src="job.companyLogo" alt="company logo" />
      </a>

      <div class="d-flex flex-column flex-lg-row justify-content-between w-100 gap-2 gap-lg-0">
        <div class="d-flex flex-column">
          <p class="fw-bold color-blue-100">{{ job.role.title }}</p>

          <a
            class="d-flex align-items-center text-muted"
            :href="job.companyUrl"
            target="_blank"
            :title="job.companyName">
            <font-awesome-icon class="icon" :icon="faBuilding" size="sm" />
            {{ job.companyName }}
          </a>

          <p class="d-flex flex-wrap gap-2 small">
            <span v-for="(skill, index) in job.role.skills" :key="index">
              {{ skill }}
            </span>
          </p>
        </div>
        <span class="d-flex align-items-center align-self-start align-self-lg-center bg-emphasis rounded p-2 h-50">
          {{ job.role.startDate }}
          <font-awesome-icon class="icon" :icon="faArrowRight" size="sm" />
          {{ job.role.endDate ?? 'Atualmente' }}
        </span>
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
  }

  .jobs-container {
    display: flex;

    width: 85%;

    gap: 0.2rem;
  }

  .company-image {
    display: flex;

    width: 5rem;
    height: 3rem;

    align-self: center;

    & > img {
      width: 100%;
      object-fit: contain;
    }
  }

  @media only screen and (max-width: 768px) {
    section {
      font-size: 0.875rem;
    }
    .jobs-container {
      width: 100%;
    }
  }
</style>
