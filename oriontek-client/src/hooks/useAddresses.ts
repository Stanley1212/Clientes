import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { addressesApi } from '../api/addresses';
import type { CreateAddressDto, UpdateAddressDto } from '../types';

export function useAddresses(clientId: string) {
  return useQuery({
    queryKey: ['addresses', clientId],
    queryFn: () => addressesApi.getByClient(clientId),
    enabled: !!clientId,
  });
}

export function useCreateAddress() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ clientId, data }: { clientId: string; data: CreateAddressDto }) =>
      addressesApi.create(clientId, data),
    onSuccess: (_, { clientId }) => {
      queryClient.invalidateQueries({ queryKey: ['addresses', clientId] });
      queryClient.invalidateQueries({ queryKey: ['clients', clientId] });
    },
  });
}

export function useUpdateAddress() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateAddressDto }) =>
      addressesApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['addresses'] });
    },
  });
}

export function useDeleteAddress() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => addressesApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['addresses'] });
    },
  });
}