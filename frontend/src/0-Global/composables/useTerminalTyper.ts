import { ref, onUnmounted } from 'vue';

export interface TyperLine {
  text: string;
  type?: string;
}

export interface TyperInputLine {
  full: string;
  type?: string;
  instant?: boolean;
}

export function useTerminalTyper(charSpeed = 28, lineGap = 350) {
  const lines = ref<TyperLine[]>([]);
  const done = ref(false);
  let activeInterval: ReturnType<typeof setInterval> | null = null;

  function cleanup() {
    if (activeInterval !== null) {
      clearInterval(activeInterval);
      activeInterval = null;
    }
  }

  onUnmounted(cleanup);

  function runSequence(inputLines: TyperInputLine[]): Promise<void> {
    return new Promise((resolve) => {
      let lineIndex = 0;

      function typeLine() {
        if (lineIndex >= inputLines.length) {
          done.value = true;
          setTimeout(resolve, 800);
          return;
        }

        const { full, type, instant } = inputLines[lineIndex++];

        if (instant || full === '') {
          lines.value.push({ text: full, type });
          setTimeout(typeLine, 80);
          return;
        }

        lines.value.push({ text: '', type });
        const idx = lines.value.length - 1;
        let charIndex = 0;

        activeInterval = setInterval(() => {
          charIndex++;
          lines.value[idx].text = full.slice(0, charIndex);
          if (charIndex >= full.length) {
            cleanup();
            setTimeout(typeLine, lineGap);
          }
        }, charSpeed);
      }

      typeLine();
    });
  }

  function showInstant(inputLines: TyperInputLine[]) {
    lines.value = inputLines.map(({ full, type }) => ({ text: full, type }));
    done.value = true;
  }

  async function runOnce(inputLines: TyperInputLine[], sessionKey: string): Promise<void> {
    const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    if (prefersReduced || sessionStorage.getItem(sessionKey)) {
      showInstant(inputLines);
      return;
    }
    sessionStorage.setItem(sessionKey, '1');
    return runSequence(inputLines);
  }

  return { lines, done, runSequence, showInstant, cleanup, runOnce };
}
