<script setup lang="ts">
  import { onMounted } from 'vue';
  import { useTerminalTyper } from '@/0-Global/composables/useTerminalTyper';
  import TerminalCard from '@/0-Global/components/TerminalCard.vue';

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

  const { lines: visible, done: typingDone, runOnce } = useTerminalTyper();

  const boat = [
    '                  |                 ',
    '                 /|\\                ',
    '                / | \\               ',
    '            ___/__|__\\___           ',
    '           /             \\          ',
    ' ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~',
    ' ~~ ~~ ~~~~ ~~~ ~~~ ~~ ~~~ ~~~~ ~ ~',
  ].join('\n');

  const LOGIN_TERMINAL_KEY = 'login_terminal_shown';

  onMounted(() => {
    const alreadySeen =
      window.matchMedia('(prefers-reduced-motion: reduce)').matches || !!sessionStorage.getItem(LOGIN_TERMINAL_KEY);
    if (alreadySeen) {
      runOnce(LINES, LOGIN_TERMINAL_KEY);
    } else {
      setTimeout(() => runOnce(LINES, LOGIN_TERMINAL_KEY), 600);
    }
  });
</script>

<!-- TODO: try to remove duplicate code between this and other views (the terminal lines and the onMounted logic) -->
<template>
  <section>
    <div class="glitch-wrap" aria-label="404">
      <span class="glitch" data-text="404">404</span>
    </div>

    <pre class="ascii">{{ boat }}</pre>

    <p class="subtitle">Página perdida em alto mar</p>

    <TerminalCard label="lost.4ever" v-fade-up>
      <div class="terminal-output">
        <template v-for="(line, idx) in visible" :key="idx">
          <div v-if="line.type === 'box'" class="terminal-box">
            <span v-for="(l, li) in line.text.split('\n')" :key="li">{{ l }}</span>
          </div>
          <p v-else :class="['terminal-line', `terminal-line--${line.type}`]">
            <template v-if="line.type === 'gap'">&nbsp;</template>
            <template v-else-if="line.text === '$ _'">
              <span class="t-prompt">$</span>
              <span class="terminal-cursor" aria-hidden="true">▋</span>
            </template>
            <template v-else>
              {{ line.text
              }}<span v-if="idx === visible.length - 1 && !typingDone" class="terminal-cursor" aria-hidden="true"
                >▋</span
              >
            </template>
          </p>
        </template>
      </div>
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

  .t-prompt {
    color: var(--brand);
    margin-right: 0.5ch;
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

  .terminal-output {
    padding: 0 var(--space-6);
    display: flex;
    flex-direction: column;
    gap: 2px;
    min-height: 220px;
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
