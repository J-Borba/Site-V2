<script setup lang="ts">
  import { ref } from 'vue';
  import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
  import { faPen } from '@fortawesome/free-solid-svg-icons/faPen';
  import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
  import { faPlus } from '@fortawesome/free-solid-svg-icons/faPlus';
  import { useFinanceiroStore } from '../store/financeiro-store';
  import EntityTable from '../components/entity-table.vue';
  import EntityModal from '../components/entity-modal.vue';
  import AtivoForm from '../components/ativo-form.vue';
  import type { Ativo } from '../types/financeiro-types';
  import { formatBRL, formatQtd } from '../utils/financeiro-format';

  const store = useFinanceiroStore();

  const modalOpen = ref(false);
  const editing = ref<Ativo | null>(null);

  const columns = [
    { key: 'codigo', label: 'Código', mono: true },
    { key: 'tipo', label: 'Tipo' },
    { key: 'setor', label: 'Setor' },
    { key: 'corretora', label: 'Corretora' },
    { key: 'quantidade', label: 'Qtd', mono: true, num: true },
    { key: 'precoMedio', label: 'PM (R$)', mono: true, num: true },
    { key: 'ultimoPreco', label: 'Atual (R$)', mono: true, num: true },
    { key: 'ganho', label: '+/−', mono: true, num: true },
  ];

  function variacaoClass(ativo: Ativo) {
    if (ativo.ultimoPreco === null) {
      return '';
    }
    return ativo.ultimoPreco >= ativo.precoMedio ? 'positive' : 'negative';
  }

  function variacaoText(ativo: Ativo) {
    if (ativo.ultimoPreco === null) {
      return '—';
    }
    const pct = ((ativo.ultimoPreco - ativo.precoMedio) / ativo.precoMedio) * 100;
    const sign = pct >= 0 ? '+' : '';
    return `${sign}${pct.toFixed(2)}%`;
  }

  function openAdd() {
    store.error = null;
    editing.value = null;
    modalOpen.value = true;
  }

  function openEdit(ativo: Ativo) {
    store.error = null;
    editing.value = ativo;
    modalOpen.value = true;
  }

  function closeModal() {
    store.error = null;
    modalOpen.value = false;
    editing.value = null;
  }

  async function onSubmit(data: Omit<Ativo, 'id'>) {
    try {
      if (editing.value) {
        await store.editAtivo(editing.value.id, data);
      }
      else {
        await store.addAtivo(data);
      }
      closeModal();
    } catch {
      // error surfaced via store.error
    }
  }

  async function onDelete(id: string) {
    if (confirm('Remover este ativo?')) {
      try {
        await store.removeAtivo(id);
      } catch {
        // error surfaced via store.error
      }
    }
  }
</script>

<template>
  <div class="posicoes" v-fade-up>
    <div class="view-header">
      <h2 class="view-title">Posições</h2>
      <button class="btn-brand" @click="openAdd">
        <FontAwesomeIcon :icon="faPlus" class="icon" />
        Adicionar
      </button>
    </div>

    <EntityTable :columns="columns" :items="store.ativos">
      <tr v-for="ativo in store.ativos" :key="ativo.id">
        <td class="mono">{{ ativo.codigo }}</td>
        <td>{{ ativo.tipo }}</td>
        <td>{{ ativo.setor || '—' }}</td>
        <td>{{ ativo.corretora || '—' }}</td>
        <td class="num">{{ formatQtd(ativo.quantidade) }}</td>
        <td class="num">{{ formatBRL(ativo.precoMedio) }}</td>
        <td class="num">{{ ativo.ultimoPreco !== null ? formatBRL(ativo.ultimoPreco) : '—' }}</td>
        <td class="num" :class="variacaoClass(ativo)">{{ variacaoText(ativo) }}</td>
        <td>
          <div class="td-actions">
            <button class="action-btn" @click="openEdit(ativo)" title="Editar">
              <FontAwesomeIcon :icon="faPen" />
            </button>
            <button class="action-btn danger" @click="onDelete(ativo.id)" title="Remover">
              <FontAwesomeIcon :icon="faTrash" />
            </button>
          </div>
        </td>
      </tr>

      <template #empty>
        <span class="empty-msg">$ nenhum ativo cadastrado. <button class="inline-add" @click="openAdd">→ adicionar</button></span>
      </template>
    </EntityTable>

    <EntityModal :open="modalOpen" :title="editing ? '~/posicoes/editar' : '~/posicoes/novo'" :error="store.error" @close="closeModal">
      <AtivoForm :key="editing?.id ?? String(modalOpen)" :initial="editing ?? undefined" @submit="onSubmit" @cancel="closeModal" />
    </EntityModal>
  </div>
</template>

<style scoped lang="scss">
  .posicoes {
    display: flex;
    flex-direction: column;
    gap: var(--space-5);
  }

  .view-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: var(--space-4);
  }

  .view-title {
    font-family: 'Syne', sans-serif;
    font-size: var(--text-xl);
    font-weight: 700;
    color: var(--text-primary);
  }

  .positive {
    color: var(--vue);
  }

  .negative {
    color: var(--error);
  }

  .empty-msg {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
  }

  .inline-add {
    color: var(--brand);
    font-family: inherit;
    font-size: inherit;
    text-decoration: underline;

    &:hover {
      opacity: 0.8;
    }
  }
</style>
