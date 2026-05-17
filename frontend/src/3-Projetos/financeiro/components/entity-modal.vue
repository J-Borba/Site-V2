<script setup lang="ts">
  import { nextTick, ref, watch } from 'vue';

  const props = defineProps<{ title: string; open: boolean; error?: string | null }>();
  const emit = defineEmits<{ (e: 'close'): void }>();
  const dialogRef = ref<HTMLDialogElement | null>(null);
  let mousedownOnBackdrop = false;

  watch(
    () => props.open,
    (val) => {
      if (val) {
        dialogRef.value?.showModal();
        nextTick(() => {
          dialogRef.value?.querySelector<HTMLElement>('input, select, textarea')?.focus();
        });
      }
      else {
        const dialog = dialogRef.value;
        if (!dialog) { return; }
        dialog.classList.add('is-closing');
        dialog.addEventListener('transitionend', () => {
          dialog.classList.remove('is-closing');
          dialog.close();
        }, { once: true });
      }
    },
  );

  function onBackdropMousedown(e: MouseEvent) {
    mousedownOnBackdrop = e.target === dialogRef.value;
  }

  function onBackdropClick(e: MouseEvent) {
    if (mousedownOnBackdrop && e.target === dialogRef.value) {
      emit('close');
    }
    mousedownOnBackdrop = false;
  }
</script>

<template>
  <dialog ref="dialogRef" class="fin-dialog" @close="emit('close')" @mousedown="onBackdropMousedown" @click="onBackdropClick">
    <div class="modal-inner" @click.stop>
      <div class="modal-header">
        <div class="modal-dots">
          <span class="dot dot--red"></span>
          <span class="dot dot--yellow"></span>
          <span class="dot dot--green"></span>
        </div>
        <span class="modal-title">{{ title }}</span>
        <button class="modal-close" @click="emit('close')" aria-label="Fechar">×</button>
      </div>
      <div class="modal-body">
        <div v-if="props.error" class="modal-error" role="alert">{{ props.error }}</div>
        <slot />
      </div>
    </div>
  </dialog>
</template>

<style scoped lang="scss">
  .fin-dialog {
    padding: 0;
    border: none;
    background: transparent;
    width: 100%;
    max-width: 520px;
    outline: none;
    opacity: 1;
    transform: translateY(0) scale(1);
    transition: opacity 200ms ease, transform 220ms cubic-bezier(0.22, 1, 0.36, 1);

    &[open] {
      @starting-style {
        opacity: 0;
        transform: translateY(-10px) scale(0.97);
      }
    }

    &.is-closing {
      opacity: 0;
      transform: translateY(8px) scale(0.97);
      transition-timing-function: ease-in;
      transition-duration: 160ms;
    }

    &::backdrop {
      background: rgba(7, 8, 12, 0.85);
      backdrop-filter: blur(4px);
      opacity: 1;
      transition: opacity 200ms ease;
    }

    &[open]::backdrop {
      @starting-style {
        opacity: 0;
      }
    }

    &.is-closing::backdrop {
      opacity: 0;
      transition-duration: 160ms;
    }
  }

  .modal-inner {
    background: var(--bg-elevated);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-lg);
    overflow: hidden;
    box-shadow: var(--shadow-lg), var(--brand-glow);
  }

  .modal-header {
    display: flex;
    align-items: center;
    gap: var(--space-3);
    padding: var(--space-3) var(--space-5);
    background: rgba(255, 255, 255, 0.03);
    border-bottom: 1px solid var(--border);
  }

  .modal-dots {
    display: flex;
    gap: var(--space-2);
  }

  .dot {
    width: 10px;
    height: 10px;
    border-radius: 50%;
    flex-shrink: 0;

    &--red {
      background: #ff5f57;
    }

    &--yellow {
      background: #febc2e;
    }

    &--green {
      background: #28c840;
    }
  }

  .modal-title {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    letter-spacing: 0.08em;
    flex: 1;
  }

  .modal-close {
    font-size: var(--text-lg);
    color: var(--text-muted);
    line-height: 1;
    padding: 0 var(--space-1);
    transition: color 150ms ease;

    &:hover {
      color: var(--text-primary);
    }
  }

  .modal-body {
    padding: var(--space-6);
  }

  .modal-error {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--error);
    background: color-mix(in srgb, var(--error) 10%, transparent);
    border: 1px solid color-mix(in srgb, var(--error) 35%, transparent);
    border-radius: var(--radius-md);
    padding: var(--space-3) var(--space-4);
    margin-bottom: var(--space-4);
    white-space: pre-wrap;
    line-height: 1.6;
  }
</style>
