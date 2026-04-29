import axios from 'axios';
import type { Address, CreateAddressDto, UpdateAddressDto } from '../types';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:54788',
});

export const addressesApi = {
  getByClient: (clientId: string) =>
    api.get<Address[]>(`api/addresses/client/${clientId}`).then((res) => res.data),
  create: (clientId: string, data: CreateAddressDto) =>
    api.post<Address>(`api/addresses/client/${clientId}`, data).then((res) => res.data),
  update: (id: string, data: UpdateAddressDto) =>
    api.put<Address>(`api/addresses/${id}`, data).then((res) => res.data),
  delete: (id: string) => api.delete<boolean>(`api/addresses/${id}`).then((res) => res.data),
};