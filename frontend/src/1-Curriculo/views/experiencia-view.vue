<script setup lang="ts">
  import { Jobs } from '../assets/utilities/jobs';
  import { useMultiToggle } from '@/0-Global/composables/useToggle';

  const { expanded, toggle } = useMultiToggle();
</script>

<template>
  <section class="experience-page">
    <div class="page-container">
      <div class="page-header">
        <span class="page-num">01</span>
        <h1 class="section-title">Experiências<br />Profissionais</h1>
      </div>

      <div class="timeline" v-fade-up>
        <div v-for="(job, index) in Jobs" :key="index" class="timeline-item">
          <div class="timeline-marker">
            <div class="marker-num">{{ String(index + 1).padStart(2, '0') }}</div>
            <div class="marker-line"></div>
          </div>

          <a :href="job.companyUrl" target="_blank" class="job-card">
            <div class="job-header">
              <span class="company-logo-wrap">
                <img :src="job.companyLogo" :alt="job.companyName" class="company-logo" />
              </span>
              <div class="job-meta">
                <p class="job-role">{{ job.roles[0].title }}</p>
                <span class="company-name">{{ job.companyName }}</span>
              </div>
              <span class="job-dates">
                {{ job.roles[job.roles.length - 1].startDate }}&nbsp;→&nbsp;{{ job.roles[0].endDate ?? 'Atualmente' }}
              </span>
            </div>

            <div class="skill-tags" v-if="job.roles[0].skills?.length">
              <span v-for="(skill, i) in job.roles[0].skills" :key="i" class="skill-tag">
                {{ skill }}
              </span>
            </div>
          </a>

          <button v-if="job.roles.length > 1" class="prev-roles-toggle" @click="toggle(index)">
            <span class="toggle-icon" :class="{ rotated: expanded.has(index) }">›</span>
            {{
              expanded.has(index)
                ? 'Ocultar'
                : job.roles.length - 1 === 1
                  ? '1 cargo anterior'
                  : `${job.roles.length - 1} cargos anteriores`
            }}
          </button>

          <transition name="accordion">
            <div v-if="job.roles.length > 1 && expanded.has(index)" class="prev-roles">
              <div v-for="(role, ri) in job.roles.slice(1)" :key="ri" class="prev-role-card">
                <div class="prev-role-header">
                  <p class="job-role">{{ role.title }}</p>
                  <span class="job-dates"> {{ role.startDate }}&nbsp;→&nbsp;{{ role.endDate ?? 'Atualmente' }} </span>
                </div>
                <div class="skill-tags" v-if="role.skills?.length">
                  <span v-for="(skill, si) in role.skills" :key="si" class="skill-tag">
                    {{ skill }}
                  </span>
                </div>
              </div>
            </div>
          </transition>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/breakpoints' as bp;
  @use '@/0-Global/style/utilities/mixins' as mx;

  .experience-page {
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

  .timeline {
    display: flex;
    flex-direction: column;
    position: relative;
  }

  .timeline-item {
    display: grid;
    grid-template-columns: 3.5rem 1fr;
    gap: var(--space-5);
    padding-bottom: var(--space-10);
    align-items: start;

    @media (max-width: bp.$bp-md) {
      grid-template-columns: 1fr;
    }

    &:last-child {
      padding-bottom: 0;

      .marker-line {
        display: none;
      }
    }
  }

  .timeline-marker {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: var(--space-5);
    position: relative;
    grid-row: 1 / -1;

    @media (max-width: bp.$bp-md) {
      display: none;
    }
  }

  .marker-num {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    font-weight: 700;
    color: var(--brand);
    letter-spacing: 0.06em;
    line-height: 1;
    flex-shrink: 0;
  }

  .marker-line {
    flex: 1;
    width: 1px;
    margin-top: var(--space-3);
    background: linear-gradient(to bottom, var(--brand-border), transparent);
    min-height: 3rem;
  }

  .job-card {
    @include mx.card-interactive;
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    padding: var(--space-5) var(--space-6);
    display: flex;
    flex-direction: column;
    gap: var(--space-4);
    width: 100%;
    text-decoration: none;
    color: inherit;
  }

  .job-header {
    display: flex;
    align-items: center;
    gap: var(--space-4);

    @media (max-width: bp.$bp-md) {
      flex-wrap: wrap;
    }
  }

  .company-logo-wrap {
    flex-shrink: 0;
    width: 3rem;
    height: 2.5rem;
    display: flex;
    align-items: center;
    justify-content: center;
    background: var(--bg-elevated);
    border: 1px solid var(--border);
    border-radius: var(--radius-sm);
    overflow: hidden;
  }

  .company-logo {
    width: 2.4rem;
    height: 2rem;
    object-fit: contain;
  }

  .job-meta {
    display: flex;
    flex-direction: column;
    gap: var(--space-1);
    flex: 1;
  }

  .job-role {
    font-family: 'Syne', sans-serif;
    font-weight: 700;
    font-size: var(--text-base);
    color: var(--text-primary);
  }

  .company-name {
    font-size: var(--text-sm);
    color: var(--text-secondary);
    width: fit-content;
  }

  .job-dates {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-secondary);
    background: var(--bg-elevated);
    border: 1px solid var(--border);
    padding: var(--space-2) var(--space-3);
    border-radius: var(--radius-sm);
    white-space: nowrap;
    flex-shrink: 0;
    letter-spacing: 0.04em;

    @media (max-width: bp.$bp-md) {
      width: 100%;
      text-align: center;
    }
  }

  .skill-tags {
    display: flex;
    flex-wrap: wrap;
    gap: var(--space-2);
  }

  .skill-tag {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--brand);
    background: var(--brand-dim);
    border: 1px solid var(--brand-border);
    padding: var(--space-1) var(--space-3);
    border-radius: 100px;
    font-weight: 400;
    letter-spacing: 0.04em;
  }

  .prev-roles-toggle {
    grid-column: 2;

    @media (max-width: bp.$bp-md) {
      grid-column: 1;
    }

    display: flex;
    align-items: center;
    gap: var(--space-2);
    background: none;
    border: none;
    color: var(--text-muted);
    font-size: var(--text-sm);
    cursor: pointer;
    padding: var(--space-1) 0;
    width: fit-content;
    transition: color 150ms ease;

    &:hover {
      color: var(--brand);
    }
  }

  .toggle-icon {
    font-size: 1rem;
    line-height: 1;
    display: inline-block;
    transition: transform 220ms ease;
    transform: rotate(0deg);

    &.rotated {
      transform: rotate(90deg);
    }
  }

  .prev-roles {
    grid-column: 2;

    @media (max-width: bp.$bp-md) {
      grid-column: 1;
    }

    display: flex;
    flex-direction: column;
    gap: var(--space-3);
  }

  .prev-role-card {
    background: var(--bg-elevated);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    padding: var(--space-4) var(--space-6);
    display: flex;
    flex-direction: column;
    gap: var(--space-3);
    border-left: 2px solid var(--brand-border);
  }

  .prev-role-header {
    display: flex;
    align-items: center;
    gap: var(--space-4);

    .job-role {
      flex: 1;
    }

    @media (max-width: bp.$bp-md) {
      flex-wrap: wrap;
    }
  }

  .accordion-enter-active,
  .accordion-leave-active {
    transition:
      opacity 220ms ease,
      transform 220ms ease;
  }

  .accordion-enter-from,
  .accordion-leave-to {
    opacity: 0;
    transform: translateY(-8px);
  }
</style>
