import { api } from '@/2-Auth/services/auth-api';
import type { Ativo, Provento } from '../types/financeiro-types';

const list = <T>(path: string) => api.get<T[]>(path).then((r) => r.data);
const create = <T>(path: string, body: unknown) => api.post<T>(path, body).then((r) => r.data);
const patch = <T>(path: string, body: unknown) => api.put<T>(path, body).then((r) => r.data);
const remove = (path: string) => api.delete(path).then(() => undefined);

export const listAtivos = () => list<Ativo>('/financeiro/ativos');
export const createAtivo = (input: Omit<Ativo, 'id'>) => create<Ativo>('/financeiro/ativos', input);
export const updateAtivo = (id: string, body: Partial<Omit<Ativo, 'id'>>) => patch<Ativo>(`/financeiro/ativos/${id}`, body);
export const deleteAtivo = (id: string) => remove(`/financeiro/ativos/${id}`);

export const listProventos = () => list<Provento>('/financeiro/proventos');
export const createProvento = (input: Omit<Provento, 'id'>) => create<Provento>('/financeiro/proventos', input);
export const updateProvento = (id: string, body: Partial<Omit<Provento, 'id'>>) => patch<Provento>(`/financeiro/proventos/${id}`, body);
export const deleteProvento = (id: string) => remove(`/financeiro/proventos/${id}`);
