<script setup lang="ts">
  import { ref } from 'vue';
  import { Jobs } from '../assets/utilities/jobs';

  const expanded = ref<Set<number>>(new Set());

  function toggle(index: number) {
    if (expanded.value.has(index)) {
      expanded.value.delete(index);
    } else {
      expanded.value.add(index);
    }
  }
</script>

<template>
  <section class="experience-page">
    <div class="page-container">
      <h1 class="section-title">Experiências Profissionais</h1>

      <div class="timeline">
        <div
          v-for="(job, index) in Jobs"
          :key="index"
          class="timeline-item">
          <div class="timeline-marker">
            <div class="marker-dot"></div>
          </div>

          <a :href="job.companyUrl" target="_blank" class="job-card">
            <div class="job-header">
              <span class="company-logo-link">
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
              <span
                v-for="(skill, i) in job.roles[0].skills"
                :key="i"
                class="skill-tag">
                {{ skill }}
              </span>
            </div>
          </a>

          <button
            v-if="job.roles.length > 1"
            class="prev-roles-toggle"
            @click="toggle(index)">
            <span class="toggle-icon" :class="{ rotated: expanded.has(index) }">›</span>
            {{ expanded.has(index) ? 'Ocultar' : job.roles.length - 1 === 1 ? '1 cargo anterior' : `${job.roles.length - 1} cargos anteriores` }}
          </button>

          <transition name="accordion">
            <div v-if="job.roles.length > 1 && expanded.has(index)" class="prev-roles">
              <div
                v-for="(role, ri) in job.roles.slice(1)"
                :key="ri"
                class="prev-role-card">
                <div class="prev-role-header">
                  <p class="job-role">{{ role.title }}</p>
                  <span class="job-dates">
                    {{ role.startDate }}&nbsp;→&nbsp;{{ role.endDate ?? 'Atualmente' }}
                  </span>
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
  .experience-page {
    flex: 1;
    padding: var(--space-16) var(--space-6);
  }

  .page-container {
    max-width: var(--container-max);
    margin-inline: auto;
    display: flex;
    flex-direction: column;
    gap: var(--space-12);
  }

  /* Timeline */

  .timeline {
    display: flex;
    flex-direction: column;
    gap: 0;
    position: relative;
  }

  .timeline-item {
    display: grid;
    grid-template-columns: 2rem 1fr;
    gap: var(--space-4);
    padding-bottom: var(--space-8);
    align-items: start;

    @media (max-width: bp.$bp-md) {
      grid-template-columns: 1fr;
    }

    &:last-child {
      padding-bottom: 0;

      .timeline-marker::after {
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

    &::after {
      content: '';
      flex: 1;
      width: 1px;
      background: var(--border);
      margin-top: var(--space-2);
    }
  }

  .marker-dot {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    background: var(--brand);
    box-shadow: 0 0 0 3px var(--brand-dim);
    flex-shrink: 0;
  }

  /* Job card */

  .job-card {
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
    transition: border-color 200ms ease, box-shadow 200ms ease;

    &:hover {
      border-color: var(--brand-border);
      box-shadow: var(--shadow-brand);
    }
  }

  .job-header {
    display: flex;
    align-items: center;
    gap: var(--space-4);

    @media (max-width: bp.$bp-md) {
      flex-wrap: wrap;
    }
  }

  .company-logo-link {
    flex-shrink: 0;
  }

  .company-logo {
    width: 3rem;
    height: 2.5rem;
    object-fit: contain;
    border-radius: var(--radius-sm);
  }

  .job-meta {
    display: flex;
    flex-direction: column;
    gap: var(--space-1);
    flex: 1;
  }

  .job-role {
    font-weight: 600;
    font-size: var(--text-base);
    color: var(--text-primary);
  }

  .company-name {
    font-size: var(--text-sm);
    color: var(--text-secondary);
    width: fit-content;
  }

  .job-dates {
    font-size: var(--text-sm);
    color: var(--text-secondary);
    background: var(--bg-elevated);
    border: 1px solid var(--border);
    padding: var(--space-2) var(--space-3);
    border-radius: var(--radius-sm);
    white-space: nowrap;
    flex-shrink: 0;

    @media (max-width: bp.$bp-md) {
      width: 100%;
      text-align: center;
    }
  }

  /* Skill tags */

  .skill-tags {
    display: flex;
    flex-wrap: wrap;
    gap: var(--space-2);
  }

  .skill-tag {
    font-size: var(--text-xs);
    color: var(--brand);
    background: var(--brand-dim);
    border: 1px solid var(--brand-border);
    padding: var(--space-1) var(--space-3);
    border-radius: 100px;
    font-weight: 500;
    letter-spacing: 0.02em;
  }

  /* Previous roles accordion */

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
    color: var(--text-secondary);
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
    transition: transform 200ms ease;
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

  /* Accordion transition */

  .accordion-enter-active,
  .accordion-leave-active {
    transition: opacity 200ms ease, transform 200ms ease;
  }

  .accordion-enter-from,
  .accordion-leave-to {
    opacity: 0;
    transform: translateY(-6px);
  }
</style>
