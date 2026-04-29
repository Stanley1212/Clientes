import axios from 'axios';
import type { Client, CreateClientDto, UpdateClientDto } from '../types';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:54788',
});

export const clientsApi = {
  getAll: () => api.get<Client[]>('api/clients').then((res) => res.data),
  getById: (id: string) => api.get<Client>(`api/clients/${id}`).then((res) => res.data),
  create: (data: CreateClientDto) => api.post<Client>('api/clients', data).then((res) => res.data),
  update: (id: string, data: UpdateClientDto) =>
    api.put<Client>(`api/clients/${id}`, data).then((res) => res.data),
  delete: (id: string) => api.delete<boolean>(`api/clients/${id}`).then((res) => res.data),
};