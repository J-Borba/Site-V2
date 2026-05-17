export type AtivoTipo = 'Ação' | 'FII' | 'ETF' | 'Tesouro' | 'CDB';
export type ProventoTipo = 'Dividendo' | 'JCP' | 'Rendimento';

export interface Ativo {
  id: string;
  codigo: string;
  tipo: AtivoTipo;
  setor: string;
  corretora: string;
  quantidade: number;
  precoMedio: number;
  ultimoPreco: number | null;
}

export interface Provento {
  id: string;
  data: string;
  ticker: string;
  tipo: ProventoTipo;
  valorLiquido: number;
}
