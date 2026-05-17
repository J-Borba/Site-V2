<script setup lang="ts">
  import { ref } from 'vue';
  import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
  import { faPen } from '@fortawesome/free-solid-svg-icons/faPen';
  import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
  import { faPlus } from '@fortawesome/free-solid-svg-icons/faPlus';
  import { useFinanceiroStore } from '../store/financeiro-store';
  import EntityTable from '../components/entity-table.vue';
  import EntityModal from '../components/entity-modal.vue';
  import ProventoForm from '../components/provento-form.vue';
  import type { Provento } from '../types/financeiro-types';
  import { formatBRL, formatDate } from '../utils/financeiro-format';

  const store = useFinanceiroStore();

  const modalOpen = ref(false);
  const editing = ref<Provento | null>(null);

  const columns = [
    { key: 'data', label: 'Data', mono: true },
    { key: 'ticker', label: 'Ticker', mono: true },
    { key: 'tipo', label: 'Tipo' },
    { key: 'valorLiquido', label: 'Valor Líq. (R$)', mono: true, num: true },
  ];

  function openAdd() {
    store.error = null;
    editing.value = null;
    modalOpen.value = true;
  }

  function openEdit(p: Provento) {
    store.error = null;
    editing.value = p;
    modalOpen.value = true;
  }

  function closeModal() {
    store.error = null;
    modalOpen.value = false;
    editing.value = null;
  }

  async function onSubmit(data: Omit<Provento, 'id'>) {
    try {
      if (editing.value) {
        await store.editProvento(editing.value.id, data);
      }
      else {
        await store.addProvento(data);
      }
      closeModal();
    } catch {
      // error surfaced via store.error
    }
  }

  async function onDelete(id: string) {
    if (confirm('Remover este provento?')) {
      try {
        await store.removeProvento(id);
      } catch {
        // error surfaced via store.error
      }
    }
  }

  const sorted = () => [...store.proventos].sort((a, b) => b.data.localeCompare(a.data));
</script>

<template>
  <div class="proventos" v-fade-up>
    <div class="view-header">
      <h2 class="view-title">Proventos</h2>
      <button class="btn-brand" @click="openAdd">
        <FontAwesomeIcon :icon="faPlus" class="icon" />
        Adicionar
      </button>
    </div>

    <EntityTable :columns="columns" :items="store.proventos">
      <tr v-for="p in sorted()" :key="p.id">
        <td class="mono">{{ formatDate(p.data) }}</td>
        <td class="mono">{{ p.ticker }}</td>
        <td>{{ p.tipo }}</td>
        <td class="num">{{ formatBRL(p.valorLiquido) }}</td>
        <td>
          <div class="td-actions">
            <button class="action-btn" @click="openEdit(p)" title="Editar">
              <FontAwesomeIcon :icon="faPen" />
            </button>
            <button class="action-btn danger" @click="onDelete(p.id)" title="Remover">
              <FontAwesomeIcon :icon="faTrash" />
            </button>
          </div>
        </td>
      </tr>

      <template #empty>
        <span class="empty-msg">$ nenhum provento registrado. <button class="inline-add" @click="openAdd">→ adicionar</button></span>
      </template>
    </EntityTable>

    <EntityModal :open="modalOpen" :title="editing ? '~/proventos/editar' : '~/proventos/novo'" :error="store.error" @close="closeModal">
      <ProventoForm :key="editing?.id ?? String(modalOpen)" :initial="editing ?? undefined" @submit="onSubmit" @cancel="closeModal" />
    </EntityModal>
  </div>
</template>

<style scoped lang="scss">
  .proventos {
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
