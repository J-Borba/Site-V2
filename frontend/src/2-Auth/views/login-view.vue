<script setup lang="ts">
  import { ref } from 'vue';
  import { isAxiosError } from 'axios';
  import { useRouter } from 'vue-router';
  import { login } from '../services/auth-api';
  import { useAuthStore } from '../store/auth-store';

  const router = useRouter();
  const authStore = useAuthStore();

  const email = ref('');
  const password = ref('');
  const errors = ref<string[]>([]);
  const loading = ref(false);

  async function handleLogin() {
    errors.value = [];
    loading.value = true;

    try {
      const user = await login(email.value, password.value);
      authStore.setUser(user);
      router.push('/profile');
    } catch (e) {
      errors.value = isAxiosError(e)
        ? e.response?.data ?? ['Algo deu errado. Tente novamente.']
        : ['Algo deu errado. Tente novamente.'];
    } finally {
      loading.value = false;
    }
  }
</script>

<template>
  <div class="auth-page">
    <div class="auth-card">
      <div class="auth-header">
        <h1 class="auth-title">Bem vindo!</h1>
        <p class="auth-subtitle">Entre na sua conta</p>
      </div>

      <form class="auth-form" @submit.prevent="handleLogin">
        <div class="form-group">
          <label class="form-label" for="email">Email</label>
          <input
            id="email"
            v-model="email"
            class="form-input"
            type="email"
            placeholder="seu-email@exemplo.com"
            autocomplete="email"
            required />
        </div>

        <div class="form-group">
          <label class="form-label" for="password">Senha</label>
          <input
            id="password"
            v-model="password"
            class="form-input"
            type="password"
            placeholder="••••••••"
            autocomplete="current-password"
            required />
        </div>

        <ul v-if="errors.length" class="error-list">
          <li v-for="err in errors" :key="err">{{ err }}</li>
        </ul>

        <button class="btn-submit" type="submit" :disabled="loading">
          {{ loading ? 'Entrando…' : 'Entrar' }}
        </button>
      </form>

      <p class="auth-footer">
        Não possui uma conta?
        <RouterLink to="/register" class="auth-link">Clique aqui.</RouterLink>
      </p>
    </div>
  </div>
</template>

<style scoped lang="scss">
  @use '../assets/auth';
</style>
