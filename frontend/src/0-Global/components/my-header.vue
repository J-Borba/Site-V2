<script setup lang="ts">
  import { ref, onMounted, onUnmounted } from 'vue';
  import { RouterLink } from 'vue-router';
  import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
  import { Navigations } from '@/0-Global/assets/utilities/navigations';
  import logo from '@/0-Global/assets/images/logo.png';

  const mobileOpen = ref(false);
  const scrolled = ref(false);

  function toggleMobile() {
    mobileOpen.value = !mobileOpen.value;
  }
  function closeMobile() {
    mobileOpen.value = false;
  }

  function onScroll() {
    const next = window.scrollY > 8;
    if (next !== scrolled.value) scrolled.value = next;
  }

  onMounted(() => window.addEventListener('scroll', onScroll, { passive: true }));
  onUnmounted(() => window.removeEventListener('scroll', onScroll));
</script>

<template>
  <header :class="['site-nav', { scrolled }]">
    <div class="nav-inner">
      <RouterLink to="/" class="nav-logo" @click="closeMobile">
        <img :src="logo" alt="João Borba" />
      </RouterLink>

      <nav class="nav-links" aria-label="Navegação principal">
        <RouterLink
          v-for="nav in Navigations"
          :key="nav.rota"
          :to="nav.rota"
          class="nav-link">
          <font-awesome-icon :icon="nav.icon" />
          <span>{{ nav.title }}</span>
        </RouterLink>
      </nav>

      <button
        class="nav-toggle"
        @click="toggleMobile"
        :aria-expanded="mobileOpen"
        aria-label="Menu">
        <font-awesome-icon :icon="mobileOpen ? faXmark : faBars" />
      </button>
    </div>

    <nav class="nav-mobile" :class="{ open: mobileOpen }" aria-label="Menu mobile">
      <RouterLink
        v-for="nav in Navigations"
        :key="nav.rota"
        :to="nav.rota"
        class="nav-mobile-link"
        @click="closeMobile">
        <font-awesome-icon :icon="nav.icon" />
        <span>{{ nav.title }}</span>
      </RouterLink>
    </nav>
  </header>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/breakpoints' as bp;

  .site-nav {
    position: sticky;
    top: 0;
    z-index: 100;
    border-bottom: 1px solid transparent;
    transition: background 200ms ease, border-color 200ms ease;

    &.scrolled {
      background: rgba(13, 17, 23, 0.82);
      backdrop-filter: blur(14px);
      -webkit-backdrop-filter: blur(14px);
      border-bottom-color: var(--border);
    }
  }

  .nav-inner {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: var(--nav-height);
    max-width: var(--container-max);
    margin-inline: auto;
    padding-inline: var(--space-6);
  }

  .nav-logo {
    width: fit-content;
    opacity: 1 !important;

    img {
      width: 6rem;
      transition: opacity 150ms ease;

      &:hover {
        opacity: 0.8;
      }
    }
  }

  .nav-links {
    display: flex;
    align-items: center;
    gap: var(--space-6);

    @media (max-width: bp.$bp-md) {
      display: none;
    }
  }

  .nav-link {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    font-weight: 500;
    padding-bottom: 3px;
    border-bottom: 2px solid transparent;
    transition: color 150ms ease, border-color 150ms ease;
    width: fit-content;

    svg {
      font-size: 0.8em;
    }

    &:hover {
      color: var(--text-primary);
      opacity: 1 !important;
    }

    &.router-link-exact-active {
      color: var(--brand) !important;
      border-bottom-color: var(--brand);
    }
  }

  .nav-toggle {
    display: none;
    align-items: center;
    justify-content: center;
    padding: var(--space-2);
    border-radius: var(--radius-sm);
    color: var(--text-primary);
    font-size: 1.2rem;
    transition: background 150ms ease;
    min-width: 44px;
    min-height: 44px;

    &:hover {
      background: var(--bg-elevated);
    }

    @media (max-width: bp.$bp-md) {
      display: flex;
    }
  }

  .nav-mobile {
    display: flex;
    flex-direction: column;
    gap: var(--space-1);
    overflow: hidden;
    max-height: 0;
    padding: 0 var(--space-4);
    background: var(--bg-surface);
    border-bottom: 1px solid transparent;
    transition: max-height 300ms ease, padding 300ms ease, border-color 300ms ease;

    &.open {
      max-height: 400px;
      padding: var(--space-4);
      border-bottom-color: var(--border);
    }

    @media (min-width: bp.$bp-md) {
      display: none;
    }
  }

  .nav-mobile-link {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    color: var(--text-secondary);
    font-size: var(--text-base);
    font-weight: 500;
    padding: var(--space-3) var(--space-2);
    border-radius: var(--radius-sm);
    min-height: 44px;
    transition: color 150ms ease, background 150ms ease;
    width: 100%;

    &:hover,
    &.router-link-exact-active {
      color: var(--brand);
      background: var(--brand-dim);
      opacity: 1 !important;
    }
  }
</style>
