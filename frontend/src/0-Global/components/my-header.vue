<script setup lang="ts">
  import { ref, onMounted, onUnmounted } from 'vue';
  import { RouterLink } from 'vue-router';
  import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
  import { Navigations } from '@/0-Global/assets/utilities/navigations';
  import { useAuthStore } from '@/2-Auth/store/auth-store';
  import logo from '@/0-Global/assets/images/logo.png';

  const authStore = useAuthStore();

  const mobileOpen = ref(false);
  const scrolled = ref(false);
  const scrollProgress = ref(0);

  function toggleMobile() {
    mobileOpen.value = !mobileOpen.value;
  }

  function closeMobile() {
    mobileOpen.value = false;
  }

  function onScroll() {
    const y = window.scrollY;
    if (y > 8 !== scrolled.value) {
      scrolled.value = y > 8;
    }
    const docH = document.documentElement.scrollHeight - window.innerHeight;
    const nextProgress = docH > 0 ? (y / docH) * 100 : 0;
    if (nextProgress !== scrollProgress.value) {
      scrollProgress.value = nextProgress;
    }
  }

  onMounted(() => window.addEventListener('scroll', onScroll, { passive: true }));
  onUnmounted(() => window.removeEventListener('scroll', onScroll));
</script>

<template>
  <header :class="['site-nav', { scrolled }]">
    <div class="nav-progress">
      <div class="nav-progress-bar" :style="{ width: scrollProgress + '%' }"></div>
    </div>

    <div class="nav-inner">
      <RouterLink to="/" class="nav-logo" @click="closeMobile" aria-label="Início">
        <img :src="logo" alt="João Borba" />
      </RouterLink>

      <nav class="nav-menu" :class="{ open: mobileOpen }" aria-label="Navegação principal">
        <RouterLink v-for="nav in Navigations" :key="nav.rota" :to="nav.rota" class="nav-item" @click="closeMobile">
          {{ nav.title }}
        </RouterLink>

        <div class="nav-divider" aria-hidden="true"></div>

        <RouterLink v-if="authStore.isLoggedIn" to="/profile" class="nav-item nav-item--auth" @click="closeMobile">
          {{ authStore.user?.userName }}
        </RouterLink>

        <RouterLink v-else to="/login" class="nav-item nav-item--login" @click="closeMobile"> Login </RouterLink>
      </nav>

      <button class="nav-toggle" @click="toggleMobile" :aria-expanded="mobileOpen" aria-label="Menu">
        <font-awesome-icon :icon="mobileOpen ? faXmark : faBars" />
      </button>
    </div>
  </header>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/breakpoints' as bp;

  .site-nav {
    position: sticky;
    top: 0;
    z-index: 100;
    border-bottom: 1px solid transparent;
    transition:
      background 300ms ease,
      border-color 300ms ease,
      backdrop-filter 300ms ease;

    &.scrolled {
      background: rgba(7, 8, 12, 0.8);
      backdrop-filter: blur(20px) saturate(180%);
      -webkit-backdrop-filter: blur(20px) saturate(180%);
      border-bottom-color: var(--border);
    }
  }

  .nav-progress {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    height: 2px;
    overflow: hidden;
    z-index: 1;
  }

  .nav-progress-bar {
    height: 100%;
    background: linear-gradient(90deg, var(--brand), var(--brand-light));
    transition: width 80ms linear;
    box-shadow: 0 0 10px rgba(13, 216, 228, 0.6);
  }

  .nav-inner {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: var(--nav-height);
    max-width: var(--container-max);
    margin-inline: auto;
    padding-inline: var(--space-6);
    gap: var(--space-8);
  }

  .nav-logo {
    flex-shrink: 0;
    opacity: 1 !important;

    img {
      width: 5.5rem;
      display: block;
      transition:
        opacity 200ms ease,
        filter 200ms ease;

      &:hover {
        opacity: 0.7;
        filter: brightness(1.2);
      }
    }
  }

  .nav-menu {
    display: flex;
    align-items: center;
    gap: var(--space-1);
    margin-left: auto;

    @media (max-width: bp.$bp-md) {
      position: absolute;
      top: var(--nav-height);
      left: 0;
      right: 0;
      flex-direction: column;
      align-items: stretch;
      gap: 0;
      padding: 0 var(--space-4);
      background: rgba(7, 8, 12, 0.95);
      backdrop-filter: blur(20px);
      -webkit-backdrop-filter: blur(20px);
      border-bottom: 1px solid transparent;
      max-height: 0;
      overflow: hidden;
      transition:
        max-height 350ms cubic-bezier(0.4, 0, 0.2, 1),
        padding 350ms cubic-bezier(0.4, 0, 0.2, 1),
        border-color 350ms ease;

      &.open {
        max-height: 480px;
        padding: var(--space-3) var(--space-4) var(--space-4);
        border-bottom-color: var(--border);
      }
    }
  }

  .nav-divider {
    width: 1px;
    height: 1.2rem;
    background: var(--border-strong);
    margin-inline: var(--space-2);

    @media (max-width: bp.$bp-md) {
      display: none;
    }
  }

  .nav-item {
    position: relative;
    display: flex;
    align-items: center;
    padding: var(--space-2) var(--space-3);
    border-radius: var(--radius-md);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    font-weight: 500;
    letter-spacing: 0.01em;
    transition:
      color 150ms ease,
      background 150ms ease;
    white-space: nowrap;
    opacity: 1 !important;

    &:hover {
      color: var(--text-primary);
      background: var(--bg-elevated);
    }

    &.router-link-exact-active {
      color: var(--brand);
      background: var(--brand-dim);

      &::after {
        content: '';
        position: absolute;
        bottom: 4px;
        left: 50%;
        transform: translateX(-50%);
        width: 4px;
        height: 4px;
        border-radius: 50%;
        background: var(--brand);
      }
    }

    &--auth {
      color: var(--brand);
      font-family: 'Space Mono', monospace;
      font-size: var(--text-xs);
      text-transform: uppercase;
      letter-spacing: 0.06em;

      &:hover {
        background: var(--brand-dim);
      }
      &.router-link-exact-active::after {
        display: none;
      }
    }

    &--login {
      border: 1px solid var(--border-strong);
      padding: var(--space-2) var(--space-4);
      transition:
        border-color 150ms ease,
        color 150ms ease,
        background 150ms ease;

      &:hover {
        border-color: var(--brand);
        color: var(--brand);
        background: var(--brand-dim);
      }

      &.router-link-exact-active {
        border-color: var(--brand);
        &::after {
          display: none;
        }
      }
    }

    @media (max-width: bp.$bp-md) {
      font-size: var(--text-base);
      padding: var(--space-3) var(--space-4);
      min-height: 48px;
      width: 100%;
      white-space: normal;

      &::after {
        display: none !important;
      }
      &--login {
        margin-top: var(--space-2);
        justify-content: center;
      }
      &--auth {
        font-size: var(--text-sm);
        letter-spacing: 0.04em;
      }
    }
  }

  .nav-toggle {
    display: none;
    align-items: center;
    justify-content: center;
    padding: var(--space-2);
    border-radius: var(--radius-md);
    color: var(--text-secondary);
    font-size: 1.1rem;
    min-width: 44px;
    min-height: 44px;
    transition:
      background 150ms ease,
      color 150ms ease;

    &:hover {
      background: var(--bg-elevated);
      color: var(--text-primary);
    }

    @media (max-width: bp.$bp-md) {
      display: flex;
    }
  }
</style>
