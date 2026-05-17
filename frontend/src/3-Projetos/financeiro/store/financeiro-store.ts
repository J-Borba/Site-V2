import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import type { Ativo, Provento } from '../types/financeiro-types';
import * as api from '../services/financeiro-api';
import * as sel from '../utils/financeiro-selectors';

function extractApiError(err: unknown, fallback: string): string {
  const data = (err as { response?: { data?: unknown } }).response?.data;
  if (Array.isArray(data) && data.length) {
    return (data as string[]).join('\n');
  }
  return fallback;
}

export const useFinanceiroStore = defineStore('financeiro', () => {
  const ativos = ref<Ativo[]>([]);
  const proventos = ref<Provento[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);

  const kpis = computed(() => {
    const year = new Date().getFullYear();
    return {
      totalInvestido: sel.totalInvestido(ativos.value),
      valorAtual: sel.valorAtual(ativos.value),
      ganhoPercent: sel.ganhoPercent(ativos.value),
      proventosYTD: sel.proventosYTD(proventos.value, year),
      composicaoPorCodigo: sel.composicaoPor(ativos.value, 'codigo'),
      composicaoPorTipo: sel.composicaoPor(ativos.value, 'tipo'),
      composicaoPorSetor: sel.composicaoPor(ativos.value, 'setor'),
      proventosMensais: sel.proventosMensais(proventos.value),
    };
  });

  async function loadAll() {
    loading.value = true;
    error.value = null;
    try {
      [ativos.value, proventos.value] = await Promise.all([
        api.listAtivos(),
        api.listProventos(),
      ]);
    } catch {
      error.value = 'Erro ao carregar dados.';
    }
    finally {
      loading.value = false;
    }
  }

  async function addAtivo(input: Omit<Ativo, 'id'>) {
    error.value = null;
    try {
      const novo = await api.createAtivo(input);
      ativos.value.push(novo);
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao adicionar ativo.');
      throw error.value;
    }
  }

  async function editAtivo(id: string, patch: Partial<Omit<Ativo, 'id'>>) {
    error.value = null;
    try {
      const updated = await api.updateAtivo(id, patch);
      const i = ativos.value.findIndex((a) => a.id === id);
      if (i !== -1) {
        ativos.value[i] = updated;
      }
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao editar ativo.');
      throw error.value;
    }
  }

  async function removeAtivo(id: string) {
    error.value = null;
    try {
      await api.deleteAtivo(id);
      ativos.value = ativos.value.filter((a) => a.id !== id);
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao remover ativo.');
      throw error.value;
    }
  }

  async function addProvento(input: Omit<Provento, 'id'>) {
    error.value = null;
    try {
      const novo = await api.createProvento(input);
      proventos.value.push(novo);
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao adicionar provento.');
      throw error.value;
    }
  }

  async function editProvento(id: string, patch: Partial<Omit<Provento, 'id'>>) {
    error.value = null;
    try {
      const updated = await api.updateProvento(id, patch);
      const i = proventos.value.findIndex((p) => p.id === id);
      if (i !== -1) {
        proventos.value[i] = updated;
      }
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao editar provento.');
      throw error.value;
    }
  }

  async function removeProvento(id: string) {
    error.value = null;
    try {
      await api.deleteProvento(id);
      proventos.value = proventos.value.filter((p) => p.id !== id);
    } catch (err) {
      error.value = extractApiError(err, 'Erro ao remover provento.');
      throw error.value;
    }
  }

  return {
    ativos,
    proventos,
    loading,
    error,
    kpis,
    loadAll,
    addAtivo,
    editAtivo,
    removeAtivo,
    addProvento,
    editProvento,
    removeProvento,
  };
});
