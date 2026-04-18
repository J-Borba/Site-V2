import axios, { AxiosError, type AxiosRequestConfig } from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL as string,
  withCredentials: true,
});

let isRefreshing = false;
let pendingQueue: Array<{ resolve: () => void; reject: (e: unknown) => void }> = [];

export async function refresh(): Promise<void> {
  await api.post('/user/refresh');
}

api.interceptors.response.use(
  (res) => res,
  (error: unknown) => {
    const axiosError = error instanceof AxiosError ? error : null;
    const status = axiosError?.response?.status;
    const originalConfig = axiosError?.config as AxiosRequestConfig & { _retry?: boolean };

    const isRefreshEndpoint = originalConfig?.url?.includes('/user/refresh');
    const isMeEndpoint = originalConfig?.url?.includes('/user/me');
    if (status !== 401 || originalConfig?._retry || isRefreshEndpoint || isMeEndpoint) {
      return Promise.reject(error);
    }

    if (isRefreshing) {
      return new Promise((resolve, reject) => {
        pendingQueue.push({
          resolve: () => resolve(api({ ...(originalConfig as AxiosRequestConfig & { _retry?: boolean }) })),
          reject,
        });
      });
    }

    isRefreshing = true;
    originalConfig._retry = true;

    return refresh()
      .then(() => {
        pendingQueue.forEach((p) => p.resolve());
        pendingQueue = [];
        return api(originalConfig);
      })
      .catch((refreshError) => {
        pendingQueue.forEach((p) => p.reject(refreshError));
        pendingQueue = [];
        window.dispatchEvent(new CustomEvent('auth:session-expired'));
        return Promise.reject(refreshError);
      })
      .finally(() => {
        isRefreshing = false;
      });
  },
);

export interface AuthUser {
  userName: string;
  email: string;
  roles: string[];
}

export async function login(email: string, password: string): Promise<AuthUser> {
  const { data } = await api.post<AuthUser>('/user/login', { email, password });
  return data;
}

export async function register(email: string, password: string, userName: string): Promise<AuthUser> {
  const { data } = await api.post<AuthUser>('/user/register', { email, password, userName });
  return data;
}

export async function getMe(): Promise<AuthUser> {
  const { data } = await api.get<AuthUser>('/user/me');
  return data;
}

export async function logout(): Promise<void> {
  await api.post('/user/logout');
}
