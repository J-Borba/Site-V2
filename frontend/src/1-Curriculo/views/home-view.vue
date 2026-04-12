<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { faIdCard } from '@fortawesome/free-regular-svg-icons';
  import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
  import foto from '@/0-Global/assets/images/my-foto.png';
  import { tiplan, uerj } from '@/0-Global/assets/utilities/_variables';

  const visible = ref(false);

  onMounted(() => {
    requestAnimationFrame(() => {
      visible.value = true;
    });
  });
</script>

<template>
  <section class="hero" :class="{ visible }">
    <div class="hero-content">
      <div class="hero-text">
        <p class="hero-greeting">Olá, sou</p>
        <h1 class="hero-name">João Victor Borba</h1>
        <p class="hero-role">Desenvolvedor .NET <span class="divider">·</span> Vue.js</p>

        <p class="hero-bio">
          Estudante de Ciências da Computação na
          <a :href="uerj.url" target="_blank">{{ uerj.shortTitle }}</a> e desenvolvedor
          full-stack na <a :href="tiplan.url" target="_blank">{{ tiplan.title }}</a>.
          Apaixonado por código limpo e soluções que fazem a diferença.
        </p>

        <div class="hero-ctas">
          <router-link to="/experiencias" class="btn-brand">
            Ver experiência
            <font-awesome-icon :icon="faArrowRight" />
          </router-link>
          <router-link to="/certificados" class="btn-ghost">
            Certificados
          </router-link>
        </div>
      </div>

      <div class="hero-image-wrap">
        <a
          class="hero-image-link"
          href="https://www.linkedin.com/in/joao-borba27/"
          target="_blank"
          aria-label="LinkedIn de João Borba">
          <div class="hero-image-ring">
            <img :src="foto" alt="Foto João Borba" class="hero-image" />
          </div>
        </a>

        <div class="hero-card-hint">
          <span class="hint-label">Sobre mim</span>
          <router-link to="/about-me" class="hint-link" aria-label="Sobre mim">
            <font-awesome-icon :icon="faIdCard" size="lg" />
          </router-link>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  .hero {
    display: flex;
    flex: 1;
    align-items: center;
    padding: var(--space-20) var(--space-6);
    max-width: var(--container-max);
    margin-inline: auto;
    width: 100%;
    opacity: 0;
    transform: translateY(16px);
    transition: opacity 0.6s ease, transform 0.6s ease;

    &.visible {
      opacity: 1;
      transform: translateY(0);
    }
  }

  .hero-content {
    display: grid;
    grid-template-columns: 1fr auto;
    align-items: center;
    gap: var(--space-16);
    width: 100%;

    @media (max-width: 768px) {
      grid-template-columns: 1fr;
      text-align: center;
      gap: var(--space-10);
    }
  }

  .hero-text {
    display: flex;
    flex-direction: column;
    gap: var(--space-5);
  }

  .hero-greeting {
    font-size: var(--text-lg);
    color: var(--brand);
    font-weight: 500;
    letter-spacing: 0.03em;
  }

  .hero-name {
    font-size: var(--text-4xl);
    font-weight: 700;
    line-height: 1.1;
    color: var(--text-primary);
  }

  .hero-role {
    font-size: var(--text-xl);
    color: var(--text-secondary);
    font-weight: 400;

    .divider {
      margin-inline: var(--space-2);
      opacity: 0.4;
    }
  }

  .hero-bio {
    font-size: var(--text-base);
    color: var(--text-secondary);
    line-height: 1.7;
    max-width: 48ch;

    a {
      color: var(--text-primary);
      font-weight: 500;
      border-bottom: 1px solid var(--border-strong);
      width: auto;
      transition: border-color 150ms ease, color 150ms ease;

      &:hover {
        color: var(--brand);
        border-bottom-color: var(--brand);
        opacity: 1 !important;
      }
    }

    @media (max-width: 768px) {
      max-width: 100%;
    }
  }

  .hero-ctas {
    display: flex;
    align-items: center;
    gap: var(--space-4);
    flex-wrap: wrap;

    @media (max-width: 768px) {
      justify-content: center;
    }
  }

  .btn-ghost {
    display: inline-flex;
    align-items: center;
    gap: var(--space-2);
    padding: var(--space-3) var(--space-6);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-md);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    font-weight: 500;
    letter-spacing: 0.02em;
    width: fit-content;
    transition: border-color 200ms ease, color 200ms ease;

    &:hover {
      border-color: var(--brand);
      color: var(--brand);
      opacity: 1 !important;
    }
  }

  /* Profile image */

  .hero-image-wrap {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-4);

    @media (max-width: 768px) {
      order: -1;
    }
  }

  .hero-image-link {
    width: fit-content;
    opacity: 1 !important;
  }

  @keyframes ring-spin {
    to { transform: rotate(360deg); }
  }

  @keyframes ring-unspin {
    to { transform: rotate(-360deg); }
  }

  @keyframes card-beckon {
    0%, 50%, 100% { transform: rotate(0deg) translateY(0); }
    55%           { transform: rotate(-18deg) translateY(-4px); }
    63%           { transform: rotate(14deg)  translateY(-4px); }
    70%           { transform: rotate(-9deg)  translateY(-2px); }
    77%           { transform: rotate(5deg)   translateY(-1px); }
    84%           { transform: rotate(-2deg)  translateY(0);    }
  }

  .hero-image-ring {
    padding: 3px;
    border-radius: 50%;
    background: conic-gradient(
      var(--brand) 0deg,
      var(--brand-light) 90deg,
      transparent 150deg,
      transparent 210deg,
      var(--brand-light) 270deg,
      var(--brand) 360deg
    );
    box-shadow: var(--brand-glow);
    transition: box-shadow 300ms ease;
    animation: ring-spin 8s linear infinite;

    &:hover {
      box-shadow: 0 0 36px rgba(0, 173, 181, 0.4);
      animation-play-state: paused;
    }

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  }

  .hero-image {
    width: 11rem;
    height: 11rem;
    border-radius: 50%;
    object-fit: cover;
    background: var(--bg-surface);
    animation: ring-unspin 8s linear infinite;

    .hero-image-ring:hover & {
      animation-play-state: paused;
    }

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }

    @media (max-width: 768px) {
      width: 8rem;
      height: 8rem;
    }
  }

  .hero-card-hint {
    display: flex;
    align-items: center;
    gap: var(--space-2);
  }

  .hint-label {
    font-size: var(--text-sm);
    color: var(--text-muted);
  }

  .hint-link {
    color: var(--brand);
    font-size: 1.1rem;
    width: fit-content;
    opacity: 1 !important;
    display: inline-flex;
    transform-origin: bottom center;
    animation: card-beckon 4s ease-in-out infinite;
    transition: color 150ms ease;

    &:hover {
      color: var(--brand-light);
    }

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  }
</style>
