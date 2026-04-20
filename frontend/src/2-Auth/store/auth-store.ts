import { defineStore } from 'pinia';
import { ref, computed } from 'vue';

import { getMe, logout as logoutApi, type AuthUser } from '../services/auth-api';

export const useAuthStore = defineStore('auth', () => {
  const user = ref<AuthUser | null>(null);
  const initialized = ref(false);

  const isLoggedIn = computed(() => user.value !== null);

  function setUser(data: AuthUser) {
    user.value = data;
    localStorage.setItem('auth:hasSession', '1');
  }

  async function initSession() {
    if (initialized.value) {
      return;
    }

    if (localStorage.getItem('auth:hasSession')) {
      try {
        user.value = await getMe();
      } catch {
        user.value = null;
        localStorage.removeItem('auth:hasSession');
      }
    }

    initialized.value = true;
  }

  async function logout() {
    try {
      await logoutApi();
    } finally {
      user.value = null;
      localStorage.removeItem('auth:hasSession');
    }
  }

  return { user, isLoggedIn, setUser, initSession, logout };
});
