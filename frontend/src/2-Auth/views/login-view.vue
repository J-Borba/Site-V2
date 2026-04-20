<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { login, parseApiErrors } from '../services/auth-api';
  import { useAuthStore } from '../store/auth-store';
  import { useTerminalTyper } from '@/0-Global/composables/useTerminalTyper';
  import TerminalCard from '@/0-Global/components/terminal-card.vue';
  import TerminalOutput from '@/0-Global/components/terminal-output.vue';
  import AmbientBackground from '@/0-Global/components/ambient-background.vue';

  // TODO: turn API_ONLINE = true when deployed
  const API_ONLINE = false;

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
      errors.value = parseApiErrors(e);
    } finally {
      loading.value = false;
    }
  }

  const LINES = [
    { full: '$ curl https://api.jborba.dev/projects', type: 'cmd' },
    { full: 'Resolving api.jborba.dev...', type: 'dim' },
    { full: 'Connecting to 0.0.0.0:443...', type: 'dim' },
    { full: 'Timeout after 30s — retrying (1/3)...', type: 'dim' },
    { full: '', type: 'gap', instant: true },
    { full: 'curl: (7) Failed to connect to api.jborba.dev port 443: Connection refused', type: 'error' },
    { full: '', type: 'gap', instant: true },
    {
      full: 'Ainda estou montando essa parte do site 🙃\nVolto com novidades em breve, prometo!',
      type: 'box',
      instant: true,
    },
    { full: '', type: 'gap', instant: true },
    { full: '$ _', type: 'cmd', instant: true },
  ];

  const { lines: visible, done: typingDone, runOnceDelayed } = useTerminalTyper();

  const LOGIN_TERMINAL_KEY = 'login_terminal_shown';

  onMounted(() => {
    if (API_ONLINE) {
      return;
    }
    runOnceDelayed(LINES, LOGIN_TERMINAL_KEY);
  });
</script>

<template>
  <div class="auth-page">
    <AmbientBackground variant="auth" show-grid />

    <!-- maintenance view  -->
    <div v-if="!API_ONLINE" class="auth-card-wrap">
      <TerminalCard label="auth.connect — em manutenção" v-fade-up>
        <TerminalOutput :lines="visible" :typing-done="typingDone" />
        <p class="offline-note">Obrigado pela visita — em breve tem novidade por aqui.</p>
      </TerminalCard>
    </div>

    <!-- real login form  -->
    <div v-else class="auth-card-wrap">
      <TerminalCard label="auth.connect" v-fade-up>
        <div class="auth-header">
          <h1 class="auth-title">Bem vindo<span class="cursor">_</span></h1>
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
      </TerminalCard>
    </div>
  </div>
</template>

<style scoped lang="scss">
  @use '../assets/auth';

  .auth-card-wrap {
    width: 100%;
    max-width: 480px;
  }

  .offline-note {
    margin: var(--space-4) var(--space-6) 0;
    padding: var(--space-4) var(--space-8) var(--space-6);
    border-top: 1px solid var(--border);
    text-align: center;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    letter-spacing: 0.04em;
    color: var(--text-muted);
  }
</style>
