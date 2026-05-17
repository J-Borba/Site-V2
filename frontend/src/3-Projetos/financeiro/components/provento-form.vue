<script setup lang="ts">
  import { reactive, ref, watch } from 'vue';
  import type { Provento, ProventoTipo } from '../types/financeiro-types';

  const props = defineProps<{ initial?: Partial<Provento> }>();
  const formEl = ref<HTMLFormElement>();
  const emit = defineEmits<{
    (e: 'submit', data: Omit<Provento, 'id'>): void;
    (e: 'cancel'): void;
  }>();

  const tipoOptions: ProventoTipo[] = ['Dividendo', 'JCP', 'Rendimento'];

  const DEFAULTS: Omit<Provento, 'id'> = {
    data: '',
    ticker: '',
    tipo: 'Dividendo',
    valorLiquido: 0,
  };

  const form = reactive<Omit<Provento, 'id'>>({ ...DEFAULTS });

  watch(
    () => props.initial,
    (val) => {
      Object.assign(form, { ...DEFAULTS, ...val });
    },
    { immediate: true },
  );

  function onSubmit() {
    if (!formEl.value!.reportValidity()) {
      return;
    }
    emit('submit', { ...form });
  }
</script>

<template>
  <form ref="formEl" class="fin-form" @submit.prevent="onSubmit" novalidate>
    <div class="form-row-2">
      <div class="form-row">
        <label class="form-label">Data *</label>
        <input v-model="form.data" class="form-input mono" type="date" required />
      </div>
      <div class="form-row">
        <label class="form-label">Ticker *</label>
        <input
          v-model="form.ticker"
          class="form-input mono"
          type="text"
          placeholder="ex: MXRF11"
          required
          @input="form.ticker = (form.ticker as string).toUpperCase()"
        />
      </div>
    </div>

    <div class="form-row-2">
      <div class="form-row">
        <label class="form-label">Tipo *</label>
        <select v-model="form.tipo" class="form-input" required>
          <option v-for="t in tipoOptions" :key="t" :value="t">{{ t }}</option>
        </select>
      </div>
      <div class="form-row">
        <label class="form-label">Valor Líquido (R$) *</label>
        <input v-model.number="form.valorLiquido" class="form-input mono" type="number" min="0.01" step="0.01" required />
      </div>
    </div>

    <div class="form-actions">
      <button type="button" class="btn-cancel" @click="emit('cancel')">Cancelar</button>
      <button type="submit" class="btn-brand">Salvar</button>
    </div>
  </form>
</template>

<style scoped lang="scss">
  .fin-form {
    display: flex;
    flex-direction: column;
    gap: var(--space-4);
  }

  .form-row-2 {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: var(--space-4);
  }

  .form-row {
    display: flex;
    flex-direction: column;
    gap: var(--space-2);
  }

  .form-label {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    letter-spacing: 0.06em;
  }

  .form-input {
    background: var(--bg-surface);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-md);
    padding: var(--space-3) var(--space-4);
    color: var(--text-primary);
    font-size: var(--text-sm);
    font-family: inherit;
    transition: border-color 150ms ease, box-shadow 150ms ease;
    outline: none;

    &.mono {
      font-family: 'Space Mono', monospace;
    }

    &:focus {
      border-color: var(--brand-border);
      box-shadow: 0 0 0 2px var(--brand-dim);
    }
  }

  select.form-input {
    cursor: pointer;
  }

  .form-actions {
    display: flex;
    justify-content: flex-end;
    gap: var(--space-3);
    margin-top: var(--space-2);
  }

  .btn-cancel {
    padding: var(--space-3) var(--space-5);
    border: 1px solid var(--border-strong);
    border-radius: var(--radius-md);
    color: var(--text-secondary);
    font-size: var(--text-sm);
    transition:
      border-color 150ms ease,
      color 150ms ease;

    &:hover {
      border-color: var(--text-muted);
      color: var(--text-primary);
    }
  }
</style>
