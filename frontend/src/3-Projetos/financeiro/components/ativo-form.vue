<script setup lang="ts">
  import { reactive, ref, watch } from 'vue';
  import type { Ativo, AtivoTipo } from '../types/financeiro-types';

  const props = defineProps<{ initial?: Partial<Ativo> }>();
  const formEl = ref<HTMLFormElement>();
  const emit = defineEmits<{
    (e: 'submit', data: Omit<Ativo, 'id'>): void;
    (e: 'cancel'): void;
  }>();

  const tipoOptions: AtivoTipo[] = ['Ação', 'FII', 'ETF', 'Tesouro', 'CDB'];

  const DEFAULTS: Omit<Ativo, 'id'> = {
    codigo: '',
    tipo: 'Ação',
    setor: '',
    corretora: '',
    quantidade: 0,
    precoMedio: 0,
    ultimoPreco: null,
  };

  const form = reactive<Omit<Ativo, 'id'>>({ ...DEFAULTS });

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
    <div class="form-row">
      <label class="form-label">Código *</label>
      <input
        v-model="form.codigo"
        class="form-input"
        type="text"
        placeholder="ex: PETR4"
        required
        @input="form.codigo = (form.codigo as string).toUpperCase()"
      />
    </div>

    <div class="form-row-2">
      <div class="form-row">
        <label class="form-label">Tipo *</label>
        <select v-model="form.tipo" class="form-input" required>
          <option v-for="t in tipoOptions" :key="t" :value="t">{{ t }}</option>
        </select>
      </div>
      <div class="form-row">
        <label class="form-label">Setor</label>
        <input v-model="form.setor" class="form-input" type="text" placeholder="ex: Petróleo e Gás" />
      </div>
    </div>

    <div class="form-row">
      <label class="form-label">Corretora</label>
      <input v-model="form.corretora" class="form-input" type="text" placeholder="ex: Clear" />
    </div>

    <div class="form-row-2">
      <div class="form-row">
        <label class="form-label">Quantidade *</label>
        <input v-model.number="form.quantidade" class="form-input mono" type="number" min="1" step="1" required />
      </div>
      <div class="form-row">
        <label class="form-label">Preço Médio (R$) *</label>
        <input v-model.number="form.precoMedio" class="form-input mono" type="number" min="0.01" step="0.01" required />
      </div>
    </div>

    <div class="form-row">
      <label class="form-label">Último Preço (R$) <span class="optional">— opcional</span></label>
      <input
        :value="form.ultimoPreco ?? ''"
        class="form-input mono"
        type="number"
        min="0"
        step="0.01"
        placeholder="—"
        @input="form.ultimoPreco = ($event.target as HTMLInputElement).value ? Number(($event.target as HTMLInputElement).value) : null"
      />
    </div>

    <div class="form-actions">
      <button type="button" class="btn-cancel" @click="emit('cancel')">Cancelar</button>
      <button type="submit" class="btn-brand">Salvar</button>
    </div>
  </form>
</template>

<style scoped lang="scss">
  @use '@/0-Global/style/utilities/mixins' as mx;

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

  .optional {
    color: var(--text-muted);
    font-size: 0.85em;
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

    &:invalid:not(:placeholder-shown) {
      border-color: var(--error-border);
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
