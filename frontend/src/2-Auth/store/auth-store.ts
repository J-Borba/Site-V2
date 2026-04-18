import { defineStore } from 'pinia';
import { ref, computed } from 'vue';

import { getMe, logout as logoutApi, type AuthUser } from '../services/auth-api';

export const useAuthStore = defineStore('auth', () => {
  const user = ref<AuthUser | null>(null);
  const initialized = ref(false);

  const isLoggedIn = computed(() => user.value !== null);

  function setUser(data: AuthUser) {
    user.value = data;
  }

  async function initSession() {
    if (initialized.value) {
      return;
    }

    try {
      user.value = await getMe();
    } catch {
      user.value = null;
    } finally {
      initialized.value = true;
    }
  }

  async function logout() {
    try {
      await logoutApi();
    } finally {
      user.value = null;
    }
  }

  return { user, isLoggedIn, setUser, initSession, logout };
});
