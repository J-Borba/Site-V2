<script setup lang="ts">
  import { ref, onMounted, onUnmounted } from 'vue';
  import { RouterLink } from 'vue-router';
  import { faBars, faXmark, faRightToBracket, faUser } from '@fortawesome/free-solid-svg-icons';
  import { Navigations } from '@/0-Global/assets/utilities/navigations';
  import { useAuthStore } from '@/2-Auth/store/auth-store';
  import logo from '@/0-Global/assets/images/logo.png';

  const authStore = useAuthStore();

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
    if (next !== scrolled.value) {
      scrolled.value = next;
    }
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

      <nav class="nav-menu" :class="{ open: mobileOpen }" aria-label="Navegação principal">
        <RouterLink v-for="nav in Navigations" :key="nav.rota" :to="nav.rota" class="nav-item" @click="closeMobile">
          <font-awesome-icon :icon="nav.icon" />
          <span>{{ nav.title }}</span>
        </RouterLink>

        <RouterLink v-if="authStore.isLoggedIn" to="/profile" class="nav-item nav-item--auth" @click="closeMobile">
          <font-awesome-icon :icon="faUser" />
          <span>{{ authStore.user?.userName }}</span>
        </RouterLink>

        <RouterLink v-else to="/login" class="nav-item" @click="closeMobile">
          <font-awesome-icon :icon="faRightToBracket" />
          <span>Login</span>
        </RouterLink>
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
      background 200ms ease,
      border-color 200ms ease;

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

  .nav-menu {
    display: flex;
    align-items: center;
    gap: var(--space-6);

    @media (max-width: bp.$bp-md) {
      position: absolute;
      top: var(--nav-height);
      left: 0;
      right: 0;
      flex-direction: column;
      align-items: stretch;
      gap: var(--space-1);
      padding: 0 var(--space-4);
      background: var(--bg-surface);
      border-bottom: 1px solid transparent;
      max-height: 0;
      overflow: hidden;
      transition:
        max-height 300ms ease,
        padding 300ms ease,
        border-color 300ms ease;

      &.open {
        max-height: 400px;
        padding: var(--space-4);
        border-bottom-color: var(--border);
      }
    }
  }

  .nav-item {
    display: flex;
    align-items: center;
    gap: var(--space-2);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    font-weight: 500;
    padding-bottom: 3px;
    border-bottom: 2px solid transparent;
    transition:
      color 150ms ease,
      border-color 150ms ease,
      background 150ms ease;
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

    &--auth {
      color: var(--brand);
    }

    @media (max-width: bp.$bp-md) {
      font-size: var(--text-base);
      padding: var(--space-3) var(--space-2);
      border-bottom: none;
      border-radius: var(--radius-sm);
      min-height: 44px;
      width: 100%;
      gap: var(--space-3);

      &:hover,
      &.router-link-exact-active {
        color: var(--brand);
        background: var(--brand-dim);
        border-bottom-color: transparent;
        opacity: 1 !important;
      }
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
</style>
