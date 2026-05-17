<script setup lang="ts">
  import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
  import type { ProjetoEntry, ProjectStatus } from '../assets/utilities/projetos-catalog';

  defineProps<{ projeto: ProjetoEntry }>();

  const statusLabel: Record<ProjectStatus, string> = {
    ativo: 'ativo',
    'em-breve': 'em breve',
    arquivado: 'arquivado',
  };
</script>

<template>
  <RouterLink :to="projeto.route" class="project-card" :class="`project-card--${projeto.status}`">
    <div class="card-bar">
      <span class="dot dot--red"></span>
      <span class="dot dot--yellow"></span>
      <span class="dot dot--green"></span>
      <span class="card-label">~/projetos/{{ projeto.slug }}</span>
      <span class="status-badge">{{ statusLabel[projeto.status] }}</span>
    </div>

    <div class="card-body">
      <div class="card-icon">
        <FontAwesomeIcon :icon="projeto.icon" />
      </div>
      <div class="card-info">
        <h3 class="card-title">{{ projeto.title }}</h3>
        <p class="card-desc">{{ projeto.description }}</p>
        <div class="tech-list">
          <span v-for="t in projeto.tech" :key="t" class="tech-tag">{{ t }}</span>
        </div>
      </div>
    </div>

    <div class="card-footer">
      <span class="cta">{{ projeto.status === 'ativo' ? 'Abrir →' : 'Em breve' }}</span>
    </div>
  </RouterLink>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/mixins' as mx;

  .project-card {
    display: flex;
    flex-direction: column;
    width: 100%;
    border-radius: var(--radius-lg);
    overflow: hidden;
    background: var(--bg-card);
    border: 1px solid var(--border-strong);
    box-shadow: var(--shadow-lg), 0 0 0 1px rgba(13, 216, 228, 0.06);
    color: var(--text-primary);
    text-decoration: none;
    @include mx.card-interactive;

    &--em-breve {
      opacity: 0.6;
      pointer-events: none;
    }

    &--arquivado {
      opacity: 0.5;
      filter: grayscale(0.4);
    }
  }

  .card-bar {
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

  .card-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    margin-left: var(--space-1);
    letter-spacing: 0.06em;
  }

  .status-badge {
    margin-left: auto;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--vue);
    letter-spacing: 0.06em;

    .project-card--em-breve & {
      color: var(--gold);
    }

    .project-card--arquivado & {
      color: var(--text-muted);
    }
  }

  .card-body {
    display: flex;
    align-items: flex-start;
    gap: var(--space-6);
    padding: var(--space-6);
    flex: 1;
  }

  .card-icon {
    font-size: var(--text-3xl);
    color: var(--brand);
    flex-shrink: 0;
    padding-top: var(--space-1);
    filter: drop-shadow(0 0 12px rgba(13, 216, 228, 0.3));
  }

  .card-info {
    display: flex;
    flex-direction: column;
    gap: var(--space-3);
  }

  .card-title {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-2xl);
    font-weight: 800;
    color: var(--text-primary);
  }

  .card-desc {
    font-size: var(--text-sm);
    color: var(--text-secondary);
    line-height: 1.65;
  }

  .tech-list {
    display: flex;
    flex-wrap: wrap;
    gap: var(--space-2);
  }

  .tech-tag {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--brand);
    background: var(--brand-dim);
    border: 1px solid var(--brand-border);
    border-radius: var(--radius-sm);
    padding: 2px var(--space-2);
    letter-spacing: 0.04em;
  }

  .card-footer {
    padding: var(--space-4) var(--space-6);
    border-top: 1px solid var(--border);
  }

  .cta {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--brand);
    letter-spacing: 0.06em;
    transition: letter-spacing 200ms ease;

    .project-card:hover & {
      letter-spacing: 0.12em;
    }
  }
</style>
