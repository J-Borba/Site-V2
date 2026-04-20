<script setup lang="ts">
  import { onMounted } from 'vue';
  import { useTerminalTyper } from '@/0-Global/composables/useTerminalTyper';
  import TerminalCard from '@/0-Global/components/terminal-card.vue';
  import TerminalOutput from '@/0-Global/components/terminal-output.vue';

  const LINES = [
    { full: '$ cd /pagina-que-voce-procura', type: 'cmd' },
    { full: 'bash: cd: /pagina-que-voce-procura: Não existe', type: 'error' },
    { full: '', type: 'gap', instant: true },
    { full: '$ ls -la esperanca/', type: 'cmd' },
    { full: "ls: não foi possível acessar 'esperanca/': Diretório vazio", type: 'error' },
    { full: '', type: 'gap', instant: true },
    { full: '$ sudo find / -name "pagina-certa"', type: 'cmd' },
    { full: '[sudo] senha para você: ••••••••', type: 'warn' },
    { full: 'find: permissão negada (e também não existe)', type: 'error' },
    { full: '', type: 'gap', instant: true },
    { full: 'Sugestão: tente o menu em cima. Ou chore. Ambos funcionam.', type: 'comment' },
    { full: '', type: 'gap', instant: true },
    { full: '$ _', type: 'cmd', instant: true },
  ];

  const { lines: visible, done: typingDone, runOnceDelayed } = useTerminalTyper();

  const boat = [
    '                  |                 ',
    '                 /|\\                ',
    '                / | \\               ',
    '            ___/__|__\\___           ',
    '           /             \\          ',
    ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~',
    ' ~~ ~~ ~~~~ ~~~ ~~~ ~~ ~~~ ~~~~ ~ ~',
  ].join('\n');

  const NOT_FOUND_TERMINAL_KEY = 'not_found_terminal_shown';

  onMounted(() => {
    runOnceDelayed(LINES, NOT_FOUND_TERMINAL_KEY);
  });
</script>
<template>
  <section>
    <div class="glitch-wrap" aria-label="404">
      <span class="glitch" data-text="404">404</span>
    </div>

    <pre class="ascii">{{ boat }}</pre>

    <p class="subtitle">Página perdida em alto mar</p>

    <TerminalCard label="lost.4ever" v-fade-up>
      <TerminalOutput :lines="visible" :typing-done="typingDone" />
    </TerminalCard>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex: 1;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: var(--space-6);
    padding: var(--space-8);
  }

  .glitch-wrap {
    line-height: 1;
  }

  .glitch {
    font-family: 'Space Mono', monospace;
    font-size: clamp(6rem, 18vw, 14rem);
    font-weight: 700;
    color: var(--brand);
    text-shadow: var(--brand-glow);
    position: relative;
    display: inline-block;

    &::before,
    &::after {
      content: attr(data-text);
      position: absolute;
      inset: 0;
      clip-path: polygon(0 0, 100% 0, 100% 35%, 0 35%);
    }

    &::before {
      color: var(--error);
      animation: glitch-top 3s infinite linear;
    }

    &::after {
      clip-path: polygon(0 65%, 100% 65%, 100% 100%, 0 100%);
      color: var(--gold);
      animation: glitch-bottom 3s infinite linear;
    }
  }

  @keyframes glitch-top {
    0%,
    90%,
    100% {
      transform: translate(0);
      opacity: 0;
    }
    92% {
      transform: translate(-3px, -2px);
      opacity: 0.8;
    }
    94% {
      transform: translate(3px, 0);
      opacity: 0.8;
    }
    96% {
      transform: translate(-2px, 1px);
      opacity: 0.8;
    }
    98% {
      transform: translate(0);
      opacity: 0;
    }
  }

  @keyframes glitch-bottom {
    0%,
    90%,
    100% {
      transform: translate(0);
      opacity: 0;
    }
    93% {
      transform: translate(3px, 1px);
      opacity: 0.8;
    }
    95% {
      transform: translate(-3px, -1px);
      opacity: 0.8;
    }
    97% {
      transform: translate(2px, 0);
      opacity: 0.8;
    }
    99% {
      transform: translate(0);
      opacity: 0;
    }
  }

  .ascii {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
    text-align: center;
    margin: 0;
    line-height: 1.5;
    animation: drift 4s ease-in-out infinite;
  }

  @keyframes drift {
    0%,
    100% {
      transform: translateY(0px) rotate(-0.5deg);
    }
    50% {
      transform: translateY(-6px) rotate(0.5deg);
    }
  }

  .subtitle {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-lg);
    color: var(--text-secondary);
    margin: 0;
    letter-spacing: 0.05em;
  }
</style>
