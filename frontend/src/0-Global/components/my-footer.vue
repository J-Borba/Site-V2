<script setup lang="ts">
  import { faGithub, faLinkedin, faWhatsapp } from '@fortawesome/free-brands-svg-icons';
  import { faEnvelope, faArrowUp } from '@fortawesome/free-solid-svg-icons';

  function scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
</script>

<template>
  <footer class="site-footer">
    <div class="footer-inner">
      <div class="footer-brand">
        <p class="footer-name">João Victor Borba</p>
        <p class="footer-role">Desenvolvedor .NET · Vue.js</p>
        <p class="footer-copy">&copy; {{ new Date().getFullYear() }} · joaoborba.com</p>
      </div>

      <div class="footer-links">
        <p class="footer-links-title">Acesso rápido</p>
        <nav>
          <router-link to="/">Início</router-link>
          <router-link to="/experiencias">Experiências</router-link>
          <router-link to="/certificados">Certificados</router-link>
          <router-link to="/about-me">Sobre mim</router-link>
        </nav>
      </div>

      <div class="footer-social">
        <p class="footer-links-title">Contato</p>
        <div class="social-list">
          <a href="https://www.linkedin.com/in/joao-borba27/" target="_blank" aria-label="LinkedIn">
            <font-awesome-icon :icon="faLinkedin" />
            <span>LinkedIn</span>
          </a>
          <a href="https://github.com/J-Borba" target="_blank" aria-label="GitHub">
            <font-awesome-icon :icon="faGithub" />
            <span>GitHub</span>
          </a>
          <a href="mailto:jvfborba+contato@gmail.com" aria-label="Email">
            <font-awesome-icon :icon="faEnvelope" />
            <span>jvfborba+contato@gmail.com</span>
          </a>
          <a href="https://wa.me/+5521981920082" target="_blank" aria-label="WhatsApp">
            <font-awesome-icon :icon="faWhatsapp" />
            <span>WhatsApp</span>
          </a>
        </div>
      </div>
    </div>

    <div class="footer-bottom">
      <button class="back-to-top" @click="scrollToTop" aria-label="Voltar ao início">
        <font-awesome-icon :icon="faArrowUp" />
        <span>Voltar ao início</span>
      </button>
    </div>
  </footer>
</template>

<style scoped lang="scss">
  .site-footer {
    background: var(--bg-surface);
    border-top: 1px solid var(--brand-border);
    padding: var(--space-12) var(--space-6) var(--space-6);
    margin-top: auto;
  }

  .footer-inner {
    display: grid;
    grid-template-columns: 1.5fr 1fr 1.5fr;
    gap: var(--space-10);
    max-width: var(--container-max);
    margin-inline: auto;
    padding-bottom: var(--space-10);
    border-bottom: 1px solid var(--border);

    @media (max-width: 768px) {
      grid-template-columns: 1fr;
      gap: var(--space-8);
    }
  }

  .footer-name {
    font-family: 'Space Grotesk', sans-serif;
    font-size: var(--text-xl);
    font-weight: 700;
    color: var(--text-primary);
    margin-bottom: var(--space-1);
  }

  .footer-role {
    font-size: var(--text-sm);
    color: var(--brand);
    margin-bottom: var(--space-4);
  }

  .footer-copy {
    font-size: var(--text-xs);
    color: var(--text-muted);
  }

  .footer-links-title {
    font-size: var(--text-xs);
    font-weight: 600;
    color: var(--text-muted);
    text-transform: uppercase;
    letter-spacing: 0.08em;
    margin-bottom: var(--space-4);
  }

  .footer-links nav {
    display: flex;
    flex-direction: column;
    gap: var(--space-3);

    a {
      font-size: var(--text-sm);
      color: var(--text-secondary);
      width: fit-content;
      transition: color 150ms ease;

      &:hover,
      &.router-link-exact-active {
        color: var(--brand);
        opacity: 1 !important;
      }
    }
  }

  .social-list {
    display: flex;
    flex-direction: column;
    gap: var(--space-3);

    a {
      display: flex;
      align-items: center;
      gap: var(--space-3);
      font-size: var(--text-sm);
      color: var(--text-secondary);
      width: fit-content;
      transition: color 150ms ease;

      svg {
        font-size: 1rem;
        color: var(--brand);
        flex-shrink: 0;
      }

      &:hover {
        color: var(--brand);
        opacity: 1 !important;
      }
    }
  }

  .footer-bottom {
    max-width: var(--container-max);
    margin-inline: auto;
    padding-top: var(--space-6);
    display: flex;
    justify-content: flex-end;
  }

  .back-to-top {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    font-size: var(--text-sm);
    color: var(--text-secondary);
    padding: var(--space-2) var(--space-3);
    border-radius: var(--radius-sm);
    transition: color 150ms ease, background 150ms ease;
    min-height: 44px;

    &:hover {
      color: var(--brand);
      background: var(--brand-dim);
    }
  }
</style>
