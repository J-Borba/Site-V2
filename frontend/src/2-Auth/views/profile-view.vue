<script setup lang="ts">
  import { computed } from 'vue';
  import { useRouter } from 'vue-router';
  import { useAuthStore } from '../store/auth-store';

  const router = useRouter();
  const authStore = useAuthStore();

  const initials = computed(() => {
    const name = authStore.user?.userName ?? '';
    return name.charAt(0).toUpperCase();
  });

  async function handleLogout() {
    await authStore.logout();
    router.push('/login');
  }
</script>

<template>
  <div class="profile-page">
    <div class="profile-card">
      <div class="avatar">{{ initials }}</div>

      <div class="user-info">
        <h1 class="user-name">{{ authStore.user?.userName }}</h1>
        <p class="user-email">{{ authStore.user?.email }}</p>
      </div>

      <div v-if="authStore.user?.roles.length" class="roles">
        <span
          v-for="role in authStore.user.roles"
          :key="role"
          :class="['role-badge', role === 'SuperAdmin' ? 'role-badge--admin' : 'role-badge--user']">
          {{ role }}
        </span>
      </div>

      <button class="btn-logout" @click="handleLogout">Sair</button>
    </div>
  </div>
</template>

<style scoped lang="scss">
  .profile-page {
    min-height: calc(100svh - var(--nav-height));
    display: flex;
    align-items: center;
    justify-content: center;
    padding: var(--space-6);
    background: radial-gradient(ellipse at 50% 0%, rgba(0, 173, 181, 0.07) 0%, transparent 65%);
  }

  .profile-card {
    width: 100%;
    max-width: 420px;
    background: var(--bg-surface);
    border: 1px solid var(--border);
    border-radius: var(--radius-lg);
    padding: var(--space-12) var(--space-8);
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-6);
  }

  .avatar {
    width: 80px;
    height: 80px;
    border-radius: 50%;
    background: var(--brand);
    color: #fff;
    font-size: 2rem;
    font-weight: 700;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .user-info {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: var(--space-1);
  }

  .user-name {
    font-size: var(--text-xl);
    color: var(--text-primary);
  }

  .user-email {
    font-size: var(--text-sm);
    color: var(--text-secondary);
  }

  .roles {
    display: flex;
    gap: var(--space-2);
    flex-wrap: wrap;
    justify-content: center;
  }

  .role-badge {
    padding: var(--space-1) var(--space-3);
    border-radius: 999px;
    font-size: var(--text-xs);
    font-weight: 600;
    letter-spacing: 0.03em;
    text-transform: uppercase;

    &--admin {
      background: rgba(0, 173, 181, 0.12);
      color: var(--brand);
      border: 1px solid rgba(0, 173, 181, 0.25);
    }

    &--user {
      background: var(--bg-elevated);
      color: var(--text-secondary);
      border: 1px solid var(--border);
    }
  }

  .btn-logout {
    margin-top: var(--space-2);
    padding: var(--space-3) var(--space-8);
    border-radius: var(--radius-sm);
    border: 1px solid var(--border);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    font-weight: 500;
    font-family: inherit;
    cursor: pointer;
    transition:
      color 150ms ease,
      border-color 150ms ease,
      background 150ms ease;

    &:hover {
      color: #f85149;
      border-color: rgba(248, 81, 73, 0.4);
      background: rgba(248, 81, 73, 0.06);
    }
  }
</style>
