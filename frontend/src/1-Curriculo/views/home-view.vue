<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
  import { faLinkedin } from '@fortawesome/free-brands-svg-icons';
  import foto from '@/0-Global/assets/images/my-foto.png';
  import { tiplan, uerj } from '@/0-Global/assets/utilities/_variables';

  const visible = ref(false);
  const nameEl = ref<HTMLElement | null>(null);
  const thisYear = new Date().getFullYear();

  function scrambleName(el: HTMLElement, finalText: string) {
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#&';
    const duration = 700;
    const start = performance.now();

    function frame(now: number) {
      const progress = Math.min((now - start) / duration, 1);
      const revealed = Math.floor(progress * finalText.length);

      el.textContent = finalText
        .split('')
        .map((char, i) => {
          if (char === ' ') return ' ';
          if (i < revealed) return char;
          return chars[Math.floor(Math.random() * chars.length)];
        })
        .join('');

      if (progress < 1) {
        requestAnimationFrame(frame);
      } else {
        el.textContent = finalText;
      }
    }

    requestAnimationFrame(frame);
  }

  onMounted(() => {
    requestAnimationFrame(() => {
      visible.value = true;
      if (nameEl.value) {
        scrambleName(nameEl.value, 'João Victor Borba');
      }
    });
  });
</script>

<template>
  <section class="hero" :class="{ visible }">
    <div class="hero-bg">
      <div class="hero-orb hero-orb--cyan"></div>
      <div class="hero-orb hero-orb--indigo"></div>
      <div class="hero-grid"></div>
    </div>

    <div class="hero-content">
      <div class="hero-text">
        <div class="hero-name-block">
          <p class="hero-greeting">Olá, sou</p>
          <h1 class="hero-name" ref="nameEl">João Victor Borba</h1>
        </div>

        <div class="hero-roles">
          <span class="role-chip role-chip--primary">.NET</span>
          <span class="role-sep">×</span>
          <span class="role-chip">Vue.js</span>
        </div>

        <p class="hero-bio">
          Estudante de Ciências da Computação na
          <a :href="uerj.url" target="_blank">{{ uerj.shortTitle }}</a> e desenvolvedor full-stack na
          <a :href="tiplan.url" target="_blank">{{ tiplan.title }}</a
          >. Apaixonado por código limpo e soluções que fazem a diferença.
        </p>

        <div class="hero-ctas">
          <router-link to="/experiencias" class="btn-cta btn-cta--filled">
            Ver experiência
            <font-awesome-icon :icon="faArrowRight" />
          </router-link>
          <router-link to="/certificados" class="btn-cta btn-cta--ghost"> Certificados </router-link>
        </div>

        <div class="hero-stats">
          <div class="stat-item">
            <span class="stat-num">{{ thisYear - tiplan.startYear! }}+</span>
            <span class="stat-label">Anos de exp.</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat-item">
            <span class="stat-num">Comp. Science</span>
            <span class="stat-label">UERJ</span>
          </div>
        </div>
      </div>

      <div class="hero-visual">
        <div class="hero-image-frame">
          <a
            href="https://www.linkedin.com/in/joao-borba27/"
            target="_blank"
            class="hero-image-link"
            aria-label="LinkedIn de João Borba">
            <div class="image-ring">
              <img :src="foto" alt="Foto João Borba" class="hero-photo" />
            </div>
            <div class="image-glow"></div>
          </a>

          <a
            href="https://www.linkedin.com/in/joao-borba27/"
            target="_blank"
            class="frame-badge"
            aria-label="LinkedIn de João Borba">
            <font-awesome-icon :icon="faLinkedin" />
            <span>LinkedIn</span>
          </a>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
  .hero {
    position: relative;
    display: flex;
    flex: 1;
    align-items: center;
    padding: var(--space-20) var(--space-6);
    overflow: hidden;
    opacity: 0;
    transform: translateY(18px);
    transition:
      opacity 0.7s ease,
      transform 0.7s ease;

    &.visible {
      opacity: 1;
      transform: translateY(0);
    }
  }

  /* Background */

  .hero-bg {
    position: absolute;
    inset: 0;
    pointer-events: none;
    z-index: 0;
  }

  @keyframes orb-drift {
    from {
      transform: translate(0, 0) scale(1);
    }
    to {
      transform: translate(24px, -24px) scale(1.12);
    }
  }

  .hero-orb {
    position: absolute;
    border-radius: 50%;
    filter: blur(90px);
    opacity: 0.3;

    &--cyan {
      width: 580px;
      height: 580px;
      top: -200px;
      right: -80px;
      background: radial-gradient(circle, rgba(13, 216, 228, 0.7) 0%, transparent 70%);
      animation: orb-drift 14s ease-in-out infinite alternate;

      @media (prefers-reduced-motion: reduce) {
        animation: none;
      }
    }

    &--indigo {
      width: 480px;
      height: 480px;
      bottom: -180px;
      left: -60px;
      background: radial-gradient(circle, rgba(90, 60, 200, 0.6) 0%, transparent 70%);
      animation: orb-drift 18s ease-in-out infinite alternate-reverse;

      @media (prefers-reduced-motion: reduce) {
        animation: none;
      }
    }
  }

  .hero-grid {
    position: absolute;
    inset: 0;
    background-image: radial-gradient(rgba(255, 255, 255, 0.05) 1px, transparent 1px);
    background-size: 32px 32px;
    mask-image: radial-gradient(ellipse 70% 70% at 50% 50%, black 20%, transparent 80%);
  }

  /* Content */

  .hero-content {
    position: relative;
    z-index: 1;
    display: grid;
    grid-template-columns: 1fr auto;
    align-items: center;
    gap: var(--space-16);
    max-width: var(--container-max);
    margin-inline: auto;
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

  /* Name */

  .hero-name-block {
    display: flex;
    flex-direction: column;
    gap: var(--space-1);
  }

  .hero-greeting {
    font-family: 'DM Sans', sans-serif;
    font-size: var(--text-lg);
    color: var(--text-secondary);
    font-weight: 400;
    letter-spacing: 0.02em;
  }

  .hero-name {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-5xl);
    font-weight: 800;
    line-height: 1;
    color: var(--text-primary);
    letter-spacing: -0.03em;
  }

  /* Role chips */

  .hero-roles {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    flex-wrap: wrap;

    @media (max-width: 768px) {
      justify-content: center;
    }
  }

  .role-chip {
    padding: var(--space-1) var(--space-4);
    border-radius: 100px;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    font-weight: 700;
    letter-spacing: 0.06em;
    border: 1px solid var(--border-strong);
    color: var(--text-secondary);
    text-transform: uppercase;

    &--primary {
      background: var(--brand-dim);
      border-color: var(--brand-border);
      color: var(--brand);
    }
  }

  .role-sep {
    color: var(--text-muted);
    font-size: var(--text-sm);
    font-weight: 300;
  }

  /* Bio */

  .hero-bio {
    font-size: var(--text-base);
    color: var(--text-secondary);
    line-height: 1.8;
    max-width: 52ch;

    a {
      color: var(--text-primary);
      font-weight: 500;
      background-image: linear-gradient(var(--brand), var(--brand));
      background-repeat: no-repeat;
      background-size: 0% 1px;
      background-position: bottom left;
      padding-bottom: 1px;
      width: auto;
      transition:
        background-size 220ms ease,
        color 220ms ease;

      &:hover {
        background-size: 100% 1px;
        color: var(--brand);
        opacity: 1 !important;
      }
    }

    @media (max-width: 768px) {
      max-width: 100%;
    }
  }

  /* CTAs */

  .hero-ctas {
    display: flex;
    align-items: center;
    gap: var(--space-4);
    flex-wrap: wrap;

    @media (max-width: 768px) {
      justify-content: center;
    }
  }

  .btn-cta {
    display: inline-flex;
    align-items: center;
    gap: var(--space-2);
    padding: var(--space-3) var(--space-6);
    border-radius: var(--radius-md);
    font-size: var(--text-sm);
    font-weight: 600;
    letter-spacing: 0.02em;
    width: fit-content;
    transition: all 220ms ease;

    &--filled {
      background: var(--brand);
      color: var(--bg-base);
      border: 1px solid var(--brand);

      &:hover {
        background: var(--brand-light);
        border-color: var(--brand-light);
        box-shadow: var(--shadow-brand);
        transform: translateY(-2px);
        opacity: 1 !important;
        color: var(--bg-base);
      }
    }

    &--ghost {
      background: transparent;
      color: var(--text-secondary);
      border: 1px solid var(--border-strong);

      &:hover {
        border-color: var(--brand);
        color: var(--brand);
        transform: translateY(-2px);
        opacity: 1 !important;
      }
    }
  }

  /* Stats */

  .hero-stats {
    display: flex;
    align-items: center;
    gap: var(--space-6);
    padding-top: var(--space-4);
    border-top: 1px solid var(--border);

    @media (max-width: 768px) {
      justify-content: center;
    }
  }

  .stat-item {
    display: flex;
    flex-direction: column;
    gap: 3px;
  }

  .stat-num {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-xl);
    font-weight: 800;
    color: var(--brand);
    line-height: 1;
  }

  .stat-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    text-transform: uppercase;
    letter-spacing: 0.08em;
  }

  .stat-divider {
    width: 1px;
    height: 2.2rem;
    background: var(--border);
    flex-shrink: 0;
  }

  /* Visual side */

  .hero-visual {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-4);

    @media (max-width: 768px) {
      order: -1;
    }
  }

  .hero-image-frame {
    position: relative;
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-4);
  }

  .hero-image-link {
    position: relative;
    display: block;
    width: fit-content;
    opacity: 1 !important;
  }

  @keyframes ring-spin {
    to {
      transform: rotate(360deg);
    }
  }

  @keyframes ring-unspin {
    to {
      transform: rotate(-360deg);
    }
  }

  .image-ring {
    padding: 3px;
    border-radius: 50%;
    background: conic-gradient(
      var(--brand) 0deg,
      var(--brand-light) 80deg,
      transparent 140deg,
      transparent 220deg,
      var(--brand-light) 280deg,
      var(--brand) 360deg
    );
    box-shadow: var(--brand-glow);
    animation: ring-spin 8s linear infinite;
    transition: box-shadow 300ms ease;

    &:hover {
      box-shadow: 0 0 52px rgba(13, 216, 228, 0.45);
      animation-play-state: paused;
    }

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  }

  .hero-photo {
    width: 12rem;
    height: 12rem;
    border-radius: 50%;
    object-fit: cover;
    background: var(--bg-surface);
    display: block;
    animation: ring-unspin 8s linear infinite;

    .image-ring:hover & {
      animation-play-state: paused;
    }

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }

    @media (max-width: 768px) {
      width: 9rem;
      height: 9rem;
    }
  }

  .image-glow {
    position: absolute;
    inset: -20px;
    border-radius: 50%;
    background: radial-gradient(circle, rgba(13, 216, 228, 0.1) 0%, transparent 70%);
    pointer-events: none;
    z-index: -1;
  }

  .frame-badge {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    padding: var(--space-2) var(--space-4);
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: 100px;
    font-size: var(--text-xs);
    color: var(--text-secondary);
    font-weight: 500;
    transition:
      border-color 150ms ease,
      color 150ms ease;
    opacity: 1 !important;

    svg {
      color: #0077b5;
      font-size: 0.9rem;
    }

    &:hover {
      border-color: #0077b5;
      color: var(--text-primary);
    }
  }
</style>
