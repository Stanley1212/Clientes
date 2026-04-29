import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Button } from './ui/Button';
import { Input } from './ui/Input';
import type { CreateAddressDto } from '../types';

const addressSchema = z.object({
  street: z.string().min(1, 'Street is required'),
  city: z.string().min(1, 'City is required'),
  state: z.string().min(0),
  zipCode: z.string().min(0),
  country: z.string().min(0),
});

type AddressFormData = z.infer<typeof addressSchema>;

interface AddressFormProps {
  onSubmit: (data: CreateAddressDto) => void;
  initialData?: AddressFormData;
  isLoading?: boolean;
}

export function AddressForm({ onSubmit, initialData, isLoading }: AddressFormProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AddressFormData>({
    resolver: zodResolver(addressSchema),
    defaultValues: initialData,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-4">
      <Input
        label="Street"
        {...register('street')}
        error={errors.street?.message}
      />
      <Input label="City" {...register('city')} error={errors.city?.message} />
      <Input label="State" {...register('state')} />
      <Input label="Zip Code" {...register('zipCode')} />
      <Input label="Country" {...register('country')} />
      <Button type="submit" disabled={isLoading}>
        {isLoading ? 'Saving...' : 'Save'}
      </Button>
    </form>
  );
}