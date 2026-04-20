<script setup lang="ts">
  import type { TyperLine } from '@/0-Global/composables/useTerminalTyper';

  withDefaults(
    defineProps<{
      lines: TyperLine[];
      typingDone: boolean;
      minHeight?: string;
    }>(),
    {
      minHeight: '220px',
    },
  );
</script>

<template>
  <div class="terminal-output" :style="{ minHeight }">
    <template v-for="(line, idx) in lines" :key="idx">
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
          }}<span v-if="idx === lines.length - 1 && !typingDone" class="terminal-cursor" aria-hidden="true">▋</span>
        </template>
      </p>
    </template>
  </div>
</template>

<style scoped lang="scss">
  .terminal-output {
    padding: 0 var(--space-6);
    display: flex;
    flex-direction: column;
    gap: 2px;
  }

  .t-prompt {
    color: var(--brand);
    margin-right: 0.5ch;
  }
</style>
