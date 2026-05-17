import type { Ativo, Provento } from '../types/financeiro-types';

export function totalInvestido(ativos: Ativo[]): number {
  return ativos.reduce((acc, a) => acc + a.precoMedio * a.quantidade, 0);
}

export function valorAtual(ativos: Ativo[]): number {
  return ativos.reduce((acc, a) => acc + (a.ultimoPreco ?? a.precoMedio) * a.quantidade, 0);
}

export function ganhoPercent(ativos: Ativo[]): number {
  const investido = totalInvestido(ativos);
  if (investido === 0) {
    return 0;
  }
  return ((valorAtual(ativos) - investido) / investido) * 100;
}

export function proventosYTD(proventos: Provento[], year: number): number {
  return proventos
    .filter((p) => parseInt(p.data.substring(0, 4)) === year)
    .reduce((acc, p) => acc + p.valorLiquido, 0);
}

export function composicaoPor(
  ativos: Ativo[],
  key: 'codigo' | 'tipo' | 'setor',
): { label: string; value: number }[] {
  const map = new Map<string, number>();
  for (const a of ativos) {
    const preco = a.ultimoPreco ?? a.precoMedio;
    const valor = preco * a.quantidade;
    const k = a[key] as string;
    map.set(k, (map.get(k) ?? 0) + valor);
  }
  return Array.from(map.entries()).map(([label, value]) => ({ label, value }));
}

export function proventosMensais(proventos: Provento[]): { mes: string; valor: number }[] {
  const map = new Map<string, number>();
  for (const p of proventos) {
    const [y, m] = p.data.split('-');
    const key = `${y}-${m}`;
    map.set(key, (map.get(key) ?? 0) + p.valorLiquido);
  }
  return Array.from(map.entries())
    .sort(([a], [b]) => a.localeCompare(b))
    .map(([mes, valor]) => ({ mes, valor }));
}

