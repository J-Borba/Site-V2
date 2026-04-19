<script setup lang="ts">
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { register, parseApiErrors } from '../services/auth-api';
  import { useAuthStore } from '../store/auth-store';

  const router = useRouter();
  const authStore = useAuthStore();

  const userName = ref('');
  const email = ref('');
  const password = ref('');
  const errors = ref<string[]>([]);
  const loading = ref(false);

  async function handleRegister() {
    errors.value = [];
    loading.value = true;

    try {
      const user = await register(email.value, password.value, userName.value);
      authStore.setUser(user);
      router.push('/profile');
    } catch (e) {
      errors.value = parseApiErrors(e);
    } finally {
      loading.value = false;
    }
  }
</script>

<template>
  <div class="auth-page">
    <div class="auth-bg">
      <div class="auth-orb auth-orb--cyan"></div>
      <div class="auth-orb auth-orb--indigo"></div>
      <div class="auth-grid"></div>
    </div>

    <div class="auth-card">
      <div class="auth-terminal-bar">
        <span class="terminal-dot terminal-dot--red"></span>
        <span class="terminal-dot terminal-dot--yellow"></span>
        <span class="terminal-dot terminal-dot--green"></span>
        <span class="terminal-label">auth.register</span>
      </div>

      <div class="auth-header">
        <h1 class="auth-title">Crie sua conta<span class="cursor">_</span></h1>
        <p class="auth-subtitle">Junte-se e comece a usar meus projetos.</p>
      </div>

      <form class="auth-form" @submit.prevent="handleRegister">
        <div class="form-group">
          <label class="form-label" for="username">Usuário</label>
          <input
            id="username"
            v-model="userName"
            class="form-input"
            type="text"
            placeholder="Nome de usuário"
            autocomplete="username"
            required />
        </div>

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
            autocomplete="new-password"
            required />
        </div>

        <ul v-if="errors.length" class="error-list">
          <li v-for="err in errors" :key="err">{{ err }}</li>
        </ul>

        <button class="btn-submit" type="submit" :disabled="loading">
          {{ loading ? 'Criando conta…' : 'Crie sua conta' }}
        </button>
      </form>

      <p class="auth-footer">
        Já possui uma conta?
        <RouterLink to="/login" class="auth-link">Entrar</RouterLink>
      </p>
    </div>
  </div>
</template>

<style scoped lang="scss">
  @use '../assets/auth';
</style>
