<script setup lang="ts">
  import { ref, onMounted, onUnmounted, nextTick } from 'vue';
  import { faArrowRight, faBriefcase, faMedal, faUser } from '@fortawesome/free-solid-svg-icons';
  import { faLinkedin } from '@fortawesome/free-brands-svg-icons';
  import foto from '@/0-Global/assets/images/my-foto.png';
  import { tiplan, uerj } from '@/0-Global/assets/utilities/_variables';
  import { totalCertCount } from '@/1-Curriculo/assets/utilities/certificateCompanies';

  const terminalVisible = ref(false);
  const terminalLines = ref<Array<{ text: string; type?: string }>>([]);
  const typingDone = ref(false);
  const identityVisible = ref(false);
  const photoVisible = ref(false);
  const ctasVisible = ref(false);
  const heroReady = ref(false);
  const statsRowEl = ref<HTMLElement | null>(null);
  let activeInterval: ReturnType<typeof setInterval> | null = null;
  let statsObserver: IntersectionObserver | null = null;

  const thisYear = new Date().getFullYear();
  const anosExp = thisYear - tiplan.startYear!;

  const BOOT_SHOWN_KEY = 'home_boot_shown';

  const BOOT_LINES = [
    { full: '> system_init: loading portfolio.exe...', type: 'comment' },
    { full: '> coffee_dependency: critical ⚠', type: 'warn' },
    { full: '> WARNING: CSS broke 2 laws of physics today', type: 'error' },
    { full: '> git status: everything is fine (probably)', type: 'comment' },
    { full: '> STATUS: ready_to_ship ✓', type: 'ok' },
  ] as const;

  const CHAR_SPEED = 28;
  const LINE_GAP = 350;

  const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

  function runTypingSequence(): Promise<void> {
    return new Promise((resolve) => {
      let lineIndex = 0;

      function typeLine() {
        if (lineIndex >= BOOT_LINES.length) {
          typingDone.value = true;
          setTimeout(resolve, 800);
          return;
        }

        const { full, type } = BOOT_LINES[lineIndex];
        terminalLines.value.push({ text: '', type });
        const idx = terminalLines.value.length - 1;
        let charIndex = 0;

        activeInterval = setInterval(() => {
          charIndex++;
          terminalLines.value[idx].text = full.slice(0, charIndex);
          if (charIndex >= full.length) {
            clearInterval(activeInterval!);
            activeInterval = null;
            lineIndex++;
            setTimeout(typeLine, LINE_GAP);
          }
        }, CHAR_SPEED);
      }

      typeLine();
    });
  }

  function animateCounters(container: HTMLElement) {
    container.querySelectorAll<HTMLElement>('.stat-counter').forEach((el) => {
      const target = parseInt(el.dataset.target ?? '0', 10);
      const duration = 1400;
      const start = performance.now();

      function tick(now: number) {
        const progress = Math.min((now - start) / duration, 1);
        const eased = 1 - Math.pow(1 - progress, 3);
        el.textContent = String(Math.round(eased * target));
        if (progress < 1) {
          requestAnimationFrame(tick);
        }
      }

      requestAnimationFrame(tick);
    });
  }

  function showInstant() {
    terminalLines.value = BOOT_LINES.map(({ full, type }) => ({
      text: full,
      type,
    }));
    typingDone.value = true;
    terminalVisible.value = true;
    identityVisible.value = true;
    photoVisible.value = true;
    ctasVisible.value = true;
    heroReady.value = true;
  }

  onMounted(async () => {
    await nextTick();

    const alreadySeen = sessionStorage.getItem(BOOT_SHOWN_KEY);

    if (prefersReducedMotion || alreadySeen) {
      showInstant();
    } else {
      sessionStorage.setItem(BOOT_SHOWN_KEY, '1');
      setTimeout(() => { terminalVisible.value = true; }, 300);
      await new Promise<void>((r) => setTimeout(r, 700));
      await runTypingSequence();
      await new Promise<void>((r) => setTimeout(r, 300));
      identityVisible.value = true;
      await new Promise<void>((r) => setTimeout(r, 300));
      photoVisible.value = true;
      await new Promise<void>((r) => setTimeout(r, 400));
      ctasVisible.value = true;
      heroReady.value = true;
    }

    if (statsRowEl.value) {
      statsObserver = new IntersectionObserver(
        ([entry]) => {
          if (entry.isIntersecting) {
            animateCounters(statsRowEl.value!);
            statsObserver?.disconnect();
            statsObserver = null;
          }
        },
        { threshold: 0.25 },
      );
      statsObserver.observe(statsRowEl.value);
    }
  });

  onUnmounted(() => {
    if (activeInterval !== null) {
      clearInterval(activeInterval);
    }
    statsObserver?.disconnect();
  });
</script>

<template>
  <div class="home">
    <section class="hero">
      <div class="hero-bg" aria-hidden="true">
        <div class="hero-grid"></div>
        <div class="hero-noise"></div>
      </div>

      <div class="hero-deco" :class="{ 'hero-deco--visible': heroReady }" aria-hidden="true">{ }</div>

      <div class="hero-content">
        <div class="terminal" :class="{ 'terminal--visible': terminalVisible }">
          <div class="terminal-titlebar" aria-hidden="true">
            <span class="terminal-dot terminal-dot--red"></span>
            <span class="terminal-dot terminal-dot--yellow"></span>
            <span class="terminal-dot terminal-dot--green"></span>
            <span class="terminal-title">portfolio.exe — bash</span>
          </div>
          <div class="terminal-body" aria-live="polite" aria-label="Terminal de inicialização">
            <p
              v-for="(line, i) in terminalLines"
              :key="i"
              class="terminal-line"
              :class="line.type ? `terminal-line--${line.type}` : ''">
              {{ line.text
              }}<span
                v-if="i === terminalLines.length - 1 && !typingDone"
                class="terminal-cursor"
                aria-hidden="true"
                >█</span
              >
            </p>
            <span v-if="typingDone" class="terminal-cursor terminal-cursor--final" aria-hidden="true">█</span>
          </div>
        </div>

        <div class="hero-identity" :class="{ 'hero-identity--visible': identityVisible }">
          <p class="hero-greeting">Olá, sou</p>
          <h1 class="hero-name">João Victor <span class="hero-name--accent">Borba</span></h1>
          <div class="hero-roles">
            <span class="role-chip role-chip--primary">.NET</span>
            <span class="role-sep">×</span>
            <span class="role-chip">Vue.js</span>
            <span class="role-sep">×</span>
            <span class="role-chip role-chip--gold">Backend Dev</span>
          </div>
          <p class="hero-bio">
            Estudante de Ciências da Computação na
            <a :href="uerj.url" target="_blank">{{ uerj.shortTitle }}</a> e desenvolvedor na
            <a :href="tiplan.url" target="_blank">{{ tiplan.title }}</a
            >. Apaixonado por código limpo — e por debugar às 23h.
          </p>
        </div>

        <div class="hero-photo-wrap" :class="{ 'hero-photo-wrap--visible': photoVisible }">
          <a
            href="https://www.linkedin.com/in/joao-borba27/"
            target="_blank"
            class="hero-image-link"
            aria-label="LinkedIn de João Borba">
            <div class="image-ring">
              <img :src="foto" alt="Foto de João Borba" class="hero-photo" />
            </div>
            <div class="image-glow"></div>
          </a>
          <a
            href="https://www.linkedin.com/in/joao-borba27/"
            target="_blank"
            class="frame-badge"
            aria-label="Perfil LinkedIn">
            <font-awesome-icon :icon="faLinkedin" />
            <span>LinkedIn</span>
          </a>
        </div>

        <div class="hero-ctas" :class="{ 'hero-ctas--visible': ctasVisible }">
          <router-link to="/experiencias" class="btn-cta btn-cta--filled">
            Ver experiência
            <font-awesome-icon :icon="faArrowRight" />
          </router-link>
          <router-link to="/certificados" class="btn-cta btn-cta--ghost">Certificados</router-link>
        </div>
      </div>

      <div
        class="hero-scroll-hint"
        :class="{ 'hero-scroll-hint--visible': ctasVisible }"
        aria-hidden="true">
        <span class="scroll-arrow"></span>
      </div>
    </section>

    <section class="below-fold">
      <div class="below-fold-inner">
        <div class="stats-row" ref="statsRowEl" v-fade-up>
          <div class="stat-item">
            <span class="stat-num">
              <span class="stat-counter" :data-target="anosExp">0</span>+
            </span>
            <span class="stat-label">Anos de exp.</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat-item">
            <span class="stat-num">
              <span class="stat-counter" :data-target="totalCertCount">0</span>+
            </span>
            <span class="stat-label">Certificados</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat-item">
            <span class="stat-num">
              <span class="stat-counter" data-target="10">0</span>+
            </span>
            <span class="stat-label">Projetos</span>
          </div>
        </div>

        <div class="nav-cards" v-fade-up>
          <router-link to="/experiencias" class="nav-card">
            <p class="nav-card-num">01</p>
            <div class="nav-card-icon">
              <font-awesome-icon :icon="faBriefcase" />
            </div>
            <h3 class="nav-card-title">Experiência</h3>
            <p class="nav-card-desc">Histórico profissional, empresas e stack utilizada.</p>
            <span class="nav-card-arrow">→</span>
          </router-link>

          <router-link to="/certificados" class="nav-card">
            <p class="nav-card-num">02</p>
            <div class="nav-card-icon">
              <font-awesome-icon :icon="faMedal" />
            </div>
            <h3 class="nav-card-title">Certificados</h3>
            <p class="nav-card-desc">Cursos e formações: Alura, Udemy, Rocketseat.</p>
            <span class="nav-card-arrow">→</span>
          </router-link>

          <router-link to="/about-me" class="nav-card">
            <p class="nav-card-num">03</p>
            <div class="nav-card-icon">
              <font-awesome-icon :icon="faUser" />
            </div>
            <h3 class="nav-card-title">Sobre Mim</h3>
            <p class="nav-card-desc">Formação, GitHub, localização e contato.</p>
            <span class="nav-card-arrow">→</span>
          </router-link>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
  .home {
    display: flex;
    flex-direction: column;
    flex: 1;
  }

  .hero {
    position: relative;
    min-height: calc(100svh - var(--nav-height));
    display: flex;
    align-items: center;
    padding: var(--space-20) var(--space-6) var(--space-16);
  }

  .hero-bg {
    position: absolute;
    inset: 0;
    pointer-events: none;
    z-index: 0;
  }

  .hero-grid {
    position: absolute;
    inset: 0;
    background-image: radial-gradient(rgba(255, 255, 255, 0.05) 1px, transparent 1px);
    background-size: 32px 32px;
    mask-image: radial-gradient(ellipse 70% 70% at 50% 50%, black 20%, transparent 80%);
  }

  .hero-noise {
    position: absolute;
    inset: 0;
    background-image: url("data:image/svg+xml,%3Csvg viewBox='0 0 256 256' xmlns='http://www.w3.org/2000/svg'%3E%3Cfilter id='noise'%3E%3CfeTurbulence type='fractalNoise' baseFrequency='0.9' numOctaves='4' stitchTiles='stitch'/%3E%3C/filter%3E%3Crect width='100%25' height='100%25' filter='url(%23noise)' opacity='0.035'/%3E%3C/svg%3E");
    background-repeat: repeat;
    background-size: 200px;
    opacity: 0.35;
    mix-blend-mode: overlay;
    pointer-events: none;
  }

  @keyframes deco-fade {
    from { opacity: 0; }
    to   { opacity: 0.04; }
  }

  .hero-deco {
    position: absolute;
    inset: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    font-family: 'Space Mono', monospace;
    font-size: clamp(8rem, 22vw, 20rem);
    font-weight: 700;
    color: var(--text-primary);
    opacity: 0;
    letter-spacing: -0.05em;
    user-select: none;
    pointer-events: none;
    z-index: 1;

    &--visible {
      animation: deco-fade 1.5s ease 0.3s forwards;
    }
  }

  .hero-content {
    position: relative;
    z-index: 2;
    display: grid;
    grid-template-areas:
      'terminal terminal'
      'identity photo'
      'ctas     photo';
    grid-template-columns: 1fr auto;
    align-items: start;
    gap: var(--space-8) var(--space-12);
    max-width: var(--container-max);
    margin-inline: auto;
    width: 100%;

    @media (max-width: 768px) {
      grid-template-areas:
        'terminal'
        'photo'
        'identity'
        'ctas';
      grid-template-columns: 1fr;
      gap: var(--space-6);
      text-align: center;
    }
  }

  .terminal {
    grid-area: terminal;
    background: var(--bg-card);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-lg);
    overflow: hidden;
    box-shadow: var(--shadow-lg), 0 0 0 1px rgba(13, 216, 228, 0.06);
    opacity: 0;
    transform: translateY(-10px);
    transition: opacity 0.4s ease, transform 0.4s ease;
    max-width: 640px;
    width: 100%;
    justify-self: center;

    &--visible {
      opacity: 1;
      transform: translateY(0);
    }
  }

  .terminal-titlebar {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    padding: var(--space-3) var(--space-4);
    background: var(--bg-elevated);
    border-bottom: 1px solid var(--border);
  }

  .terminal-dot {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    flex-shrink: 0;

    &--red    { background: #ff5f56; }
    &--yellow { background: #ffbd2e; }
    &--green  { background: #27c93f; }
  }

  .terminal-title {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    margin-left: auto;
    letter-spacing: 0.04em;
  }

  .terminal-body {
    padding: var(--space-5);
    min-height: 180px;
    max-height: 240px;
    overflow-y: auto;
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
    scrollbar-width: none;
    text-align: left;

    &::-webkit-scrollbar { display: none; }
  }

  .terminal-line {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    line-height: 1.7;
    color: var(--text-secondary);
    white-space: pre-wrap;
    word-break: break-all;
    margin: 0;

    &--warn    { color: var(--gold); }
    &--error   { color: #e05c5c; }
    &--ok      { color: var(--brand); }
    &--comment { color: var(--text-muted); }
  }

  @keyframes cursor-blink {
    0%, 100% { opacity: 1; }
    50%       { opacity: 0; }
  }

  .terminal-cursor {
    display: inline-block;
    color: var(--brand);
    animation: cursor-blink 0.7s step-end infinite;
    margin-left: 1px;

    &--final {
      animation: cursor-blink 1s step-end infinite;
    }
  }

  .hero-identity {
    grid-area: identity;
    display: flex;
    flex-direction: column;
    gap: var(--space-4);
    opacity: 0;
    transform: translateY(20px);
    transition: opacity 0.5s ease, transform 0.5s ease;

    &--visible {
      opacity: 1;
      transform: translateY(0);
    }
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

    &--accent {
      color: var(--brand);
    }
  }

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

    &--gold {
      background: var(--gold-dim);
      border-color: var(--gold-border);
      color: var(--gold);
    }
  }

  .role-sep {
    color: var(--text-muted);
    font-size: var(--text-sm);
    font-weight: 300;
  }

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
      transition: background-size 220ms ease, color 220ms ease;

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

  .hero-photo-wrap {
    grid-area: photo;
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-4);
    align-self: center;
    opacity: 0;
    transform: scale(0.88);
    transition: opacity 0.5s ease, transform 0.5s cubic-bezier(0.34, 1.56, 0.64, 1);

    &--visible {
      opacity: 1;
      transform: scale(1);
    }
  }

  .hero-image-link {
    position: relative;
    display: block;
    width: fit-content;
    opacity: 1 !important;
  }

  @keyframes ring-spin {
    to { transform: rotate(360deg); }
  }

  @keyframes ring-unspin {
    to { transform: rotate(-360deg); }
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
    transition: border-color 150ms ease, color 150ms ease;
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

  .hero-ctas {
    grid-area: ctas;
    display: flex;
    align-items: center;
    gap: var(--space-4);
    flex-wrap: wrap;
    opacity: 0;
    transform: translateY(12px);
    transition: opacity 0.45s ease, transform 0.45s ease;

    &--visible {
      opacity: 1;
      transform: translateY(0);
    }

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

  @keyframes scroll-bounce {
    0%, 100% { transform: rotate(45deg) translate(0, 0); }
    50%       { transform: rotate(45deg) translate(3px, 3px); }
  }

  .hero-scroll-hint {
    position: absolute;
    bottom: var(--space-8);
    left: 50%;
    transform: translateX(-50%);
    opacity: 0;
    transition: opacity 0.5s ease 0.3s;

    &--visible { opacity: 0.35; }

    @media (max-width: 768px) { display: none; }
  }

  .scroll-arrow {
    display: block;
    width: 18px;
    height: 18px;
    border-right: 2px solid var(--brand);
    border-bottom: 2px solid var(--brand);
    transform: rotate(45deg);
    animation: scroll-bounce 1.8s ease-in-out infinite;

    @media (prefers-reduced-motion: reduce) { animation: none; }
  }

  .below-fold {
    padding: var(--space-20) var(--space-6);
    background: var(--bg-surface);
    border-top: 1px solid var(--border);
    position: relative;

    &::before {
      content: '';
      position: absolute;
      top: -1px;
      left: 0;
      right: 0;
      height: 120px;
      background: linear-gradient(to bottom, var(--bg-base), transparent);
      pointer-events: none;
      z-index: 1;
    }
  }

  .below-fold-inner {
    max-width: var(--container-max);
    margin-inline: auto;
    display: flex;
    flex-direction: column;
    gap: var(--space-16);
    position: relative;
    z-index: 2;
  }

  .stats-row {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: var(--space-10);
    padding: var(--space-8) var(--space-10);
    background: var(--bg-card);
    border: 1px solid var(--border);
    border-radius: var(--radius-xl);
    box-shadow: var(--shadow-md);

    @media (max-width: 640px) {
      flex-direction: column;
      gap: var(--space-6);
      padding: var(--space-6);
    }
  }

  .stat-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-2);
    text-align: center;
  }

  .stat-num {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-4xl);
    font-weight: 800;
    color: var(--brand);
    line-height: 1;
    font-variant-numeric: tabular-nums;
  }

  .stat-counter {
    display: inline-block;
    min-width: 2ch;
    text-align: right;
  }

  .stat-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    text-transform: uppercase;
    letter-spacing: 0.1em;
  }

  .stat-divider {
    width: 1px;
    height: 3rem;
    background: var(--border-strong);
    flex-shrink: 0;

    @media (max-width: 640px) {
      width: 3rem;
      height: 1px;
    }
  }

  .nav-cards {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: var(--space-5);
    width: 100%;

    @media (max-width: 768px) {
      grid-template-columns: 1fr;
      gap: var(--space-4);
    }
  }

  .nav-card {
    position: relative;
    display: flex;
    flex-direction: column;
    gap: var(--space-3);
    padding: var(--space-8) var(--space-6);
    background: var(--bg-card);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    overflow: hidden;
    text-decoration: none;
    opacity: 1 !important;
    width: 100%;
    box-sizing: border-box;
    transition: transform 200ms ease, border-color 200ms ease, box-shadow 200ms ease;

    &::before {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      height: 2px;
      background: linear-gradient(90deg, var(--brand), var(--brand-light), transparent);
      opacity: 0;
      transition: opacity 200ms ease;
    }

    &:hover {
      transform: translateY(-4px);
      border-color: var(--brand-border);
      box-shadow: var(--shadow-brand), var(--shadow-lg);

      &::before { opacity: 1; }
      .nav-card-num { color: var(--brand-dim); }
      .nav-card-arrow { transform: translateX(4px); color: var(--brand); }
    }
  }

  .nav-card-num {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-4xl);
    font-weight: 800;
    color: var(--border);
    line-height: 1;
    letter-spacing: -0.05em;
    transition: color 200ms ease;
    margin: 0;
  }

  .nav-card-icon {
    font-size: 1.4rem;
    color: var(--brand);
    margin-bottom: var(--space-1);
  }

  .nav-card-title {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-xl);
    font-weight: 700;
    color: var(--text-primary);
  }

  .nav-card-desc {
    font-size: var(--text-sm);
    color: var(--text-secondary);
    line-height: 1.7;
    flex: 1;
    margin: 0;
  }

  .nav-card-arrow {
    font-size: var(--text-xl);
    color: var(--text-muted);
    transition: transform 200ms ease, color 200ms ease;
    align-self: flex-start;
  }
</style>
