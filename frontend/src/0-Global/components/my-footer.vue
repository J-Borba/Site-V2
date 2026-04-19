<script setup lang="ts">
  import { faGithub, faLinkedin, faWhatsapp } from '@fortawesome/free-brands-svg-icons';
  import { faEnvelope, faArrowUp } from '@fortawesome/free-solid-svg-icons';

  function scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
</script>

<template>
  <footer class="site-footer">
    <div class="footer-top-line"></div>

    <div class="footer-inner">
      <div class="footer-brand">
        <p class="footer-big-name">João Victor</p>
        <p class="footer-big-name footer-big-name--accent">Borba</p>
        <p class="footer-role">Desenvolvedor .NET · Vue.js</p>
      </div>

      <div class="footer-links">
        <p class="footer-col-title">Acesso rápido</p>
        <nav>
          <router-link to="/">Início</router-link>
          <router-link to="/experiencias">Experiências</router-link>
          <router-link to="/certificados">Certificados</router-link>
          <router-link to="/about-me">Sobre mim</router-link>
        </nav>
      </div>

      <div class="footer-social">
        <p class="footer-col-title">Contato</p>
        <div class="social-list">
          <a
            href="https://www.linkedin.com/in/joao-borba27/"
            target="_blank"
            aria-label="LinkedIn"
            class="social-link social-link--linkedin">
            <font-awesome-icon :icon="faLinkedin" />
            <span>LinkedIn</span>
          </a>
          <a
            href="https://github.com/J-Borba"
            target="_blank"
            aria-label="GitHub"
            class="social-link social-link--github">
            <font-awesome-icon :icon="faGithub" />
            <span>GitHub</span>
          </a>
          <a href="mailto:jvfborba+contato@gmail.com" aria-label="Email" class="social-link social-link--email">
            <font-awesome-icon :icon="faEnvelope" />
            <span>jvfborba+contato@gmail.com</span>
          </a>
          <a
            href="https://wa.me/+5521981920082"
            target="_blank"
            aria-label="WhatsApp"
            class="social-link social-link--whatsapp">
            <font-awesome-icon :icon="faWhatsapp" />
            <span>WhatsApp</span>
          </a>
        </div>
      </div>
    </div>

    <div class="footer-bottom">
      <p class="footer-copy">&copy; {{ new Date().getFullYear() }} João Victor Borba</p>
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
    border-top: 1px solid var(--border);
    padding: var(--space-12) var(--space-6) var(--space-6);
    margin-top: auto;
    position: relative;
    overflow: hidden;
  }

  .footer-top-line {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    height: 1px;
    background: linear-gradient(90deg, transparent, var(--brand), var(--brand-light), transparent);
    opacity: 0.6;
  }

  .footer-inner {
    display: grid;
    grid-template-columns: 1.8fr 1fr 1.5fr;
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

  .footer-brand {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
  }

  .footer-big-name {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-3xl);
    font-weight: 800;
    color: var(--text-primary);
    letter-spacing: -0.03em;
    line-height: 1.1;

    &--accent {
      color: var(--brand);
    }
  }

  .footer-role {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-secondary);
    text-transform: uppercase;
    letter-spacing: 0.08em;
    margin-top: var(--space-2);
  }

  .footer-col-title {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    font-weight: 700;
    color: var(--text-muted);
    text-transform: uppercase;
    letter-spacing: 0.1em;
    margin-bottom: var(--space-5);
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
      position: relative;

      &::before {
        content: '';
        position: absolute;
        bottom: -1px;
        left: 0;
        width: 0;
        height: 1px;
        background: var(--brand);
        transition: width 200ms ease;
      }

      &:hover,
      &.router-link-exact-active {
        color: var(--brand);
        opacity: 1 !important;

        &::before {
          width: 100%;
        }
      }
    }
  }

  .social-list {
    display: flex;
    flex-direction: column;
    gap: var(--space-3);
  }

  .social-link {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    font-size: var(--text-sm);
    color: var(--text-secondary);
    width: fit-content;
    transition:
      color 150ms ease,
      transform 150ms ease;
    opacity: 1 !important;

    svg {
      font-size: 1rem;
      flex-shrink: 0;
      transition: color 150ms ease;
      color: var(--text-muted);
    }

    &:hover {
      transform: translateX(3px);
    }

    &--linkedin:hover {
      color: #0077b5;
      svg {
        color: #0077b5;
      }
    }

    &--github:hover {
      color: var(--text-primary);
      svg {
        color: var(--text-primary);
      }
    }

    &--email:hover {
      color: var(--brand);
      svg {
        color: var(--brand);
      }
    }

    &--whatsapp:hover {
      color: var(--bg-whatsapp-top);
      svg {
        color: var(--bg-whatsapp-top);
      }
    }
  }

  .footer-bottom {
    max-width: var(--container-max);
    margin-inline: auto;
    padding-top: var(--space-6);
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  .footer-copy {
    font-size: var(--text-xs);
    color: var(--text-muted);
    font-family: 'Space Mono', monospace;
  }

  .back-to-top {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    font-size: var(--text-sm);
    font-family: 'Space Mono', monospace;
    color: var(--text-muted);
    padding: var(--space-2) var(--space-3);
    border-radius: var(--radius-sm);
    border: 1px solid transparent;
    transition:
      color 150ms ease,
      background 150ms ease,
      border-color 150ms ease;
    min-height: 44px;
    letter-spacing: 0.04em;

    &:hover {
      color: var(--brand);
      background: var(--brand-dim);
      border-color: var(--brand-border);
    }
  }
</style>
