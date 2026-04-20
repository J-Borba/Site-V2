<script lang="ts" setup>
  import { faMedal, faUserGraduate, faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';
  import { CertificateCompanies } from '../assets/utilities/certificateCompanies';
  import { useSingleToggle } from '@/0-Global/composables/useToggle';

  const { active: expandedWarning, toggle: toggleWarning } = useSingleToggle<string>();
</script>

<template>
  <section class="certs-page">
    <div class="page-container">
      <div class="page-header">
        <span class="page-num">02</span>
        <h1 class="section-title">Certificados</h1>
      </div>

      <div class="companies-grid" v-fade-up>
        <div v-for="(company, ci) in CertificateCompanies" :key="ci" class="company-card">
          <div class="company-header">
            <div class="company-icon-wrap">
              <img :src="company.icon" :alt="company.name" class="company-icon" />
            </div>
            <h2 class="company-name">{{ company.name }}</h2>
          </div>

          <div class="degrees-list">
            <div v-for="(degree, di) in company.degrees" :key="di" class="degree-block">
              <div class="degree-title-row" v-if="degree.title">
                <a v-if="degree.url" :href="degree.url" target="_blank" class="degree-title">
                  <font-awesome-icon :icon="faUserGraduate" class="deg-icon" />
                  {{ degree.title }}
                </a>
                <span v-else class="degree-title incomplete">
                  <font-awesome-icon :icon="faUserGraduate" class="deg-icon" />
                  {{ degree.title }}
                  <button class="warn-btn" :title="'Formação em andamento'" @click="toggleWarning(`${ci}-${di}`)">
                    <font-awesome-icon :icon="faTriangleExclamation" />
                  </button>
                </span>
                <p v-if="expandedWarning === `${ci}-${di}`" class="warn-msg">Esta formação ainda está em andamento.</p>
              </div>

              <ul class="cert-list">
                <li v-for="(cert, i) in degree.certificates" :key="i" class="cert-item">
                  <a v-if="cert.title && cert.url" :href="cert.url" target="_blank" class="cert-link">
                    <font-awesome-icon :icon="faMedal" class="cert-icon" />
                    {{ cert.title }}
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/mixins' as mx;

  .certs-page {
    flex: 1;
    padding: var(--space-16) var(--space-6);
  }

  .page-container {
    @include mx.page-container;
    gap: var(--space-12);
  }

  .page-header {
    position: relative;
  }

  .page-num {
    @include mx.page-num-style;
  }

  .companies-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
    gap: var(--space-6);
  }

  .company-card {
    @include mx.card-interactive;
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    padding: var(--space-6);
    display: flex;
    flex-direction: column;
    gap: var(--space-5);
  }

  .company-header {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    padding-bottom: var(--space-4);
    border-bottom: 1px solid var(--border);
  }

  .company-icon-wrap {
    width: 2.2rem;
    height: 2.2rem;
    display: flex;
    align-items: center;
    justify-content: center;
    background: var(--bg-elevated);
    border: 1px solid var(--border);
    border-radius: var(--radius-sm);
    flex-shrink: 0;
    overflow: hidden;
  }

  .company-icon {
    width: 1.8rem;
    height: 1.8rem;
    object-fit: contain;
  }

  .company-name {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-lg);
    font-weight: 700;
    color: var(--text-primary);
  }

  .degrees-list {
    display: flex;
    flex-direction: column;
    gap: var(--space-5);
  }

  .degree-block {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
  }

  .degree-title-row {
    display: flex;
    flex-direction: column;
    gap: var(--space-1);
  }

  .degree-title {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    font-size: var(--text-sm);
    font-weight: 600;
    color: var(--text-primary);
    width: fit-content;
    transition: color 150ms ease;

    &:hover {
      color: var(--brand);
      opacity: 1 !important;
    }

    &.incomplete {
      color: var(--text-secondary);
      cursor: default;
      width: 100%;
    }
  }

  .deg-icon {
    color: var(--brand);
    font-size: 0.85em;
  }

  .warn-btn {
    color: var(--warning);
    font-size: 0.85em;
    padding: 0 var(--space-1);
    border-radius: var(--radius-sm);
    transition: opacity 150ms ease;
    min-height: 44px;
    min-width: 44px;
    display: inline-flex;
    align-items: center;
    justify-content: center;

    &:hover {
      opacity: 0.7;
    }
  }

  .warn-msg {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--warning);
    padding-left: var(--space-5);
    letter-spacing: 0.02em;
  }

  .cert-list {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
    padding-left: var(--space-5);
  }

  .cert-item {
    list-style: none;
  }

  .cert-link {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    font-size: var(--text-sm);
    color: var(--text-secondary);
    width: fit-content;
    transition:
      color 150ms ease,
      transform 150ms ease;

    &:hover {
      color: var(--brand);
      transform: translateX(3px);
      opacity: 1 !important;
    }
  }

  .cert-icon {
    color: var(--gold);
    font-size: 0.8em;
    flex-shrink: 0;
  }
</style>
