import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Button } from './ui/Button';
import { Input } from './ui/Input';
import type { CreateClientDto, UpdateClientDto } from '../types';

const clientSchema = z.object({
  name: z.string().min(1, 'Name is required'),
  email: z.string().email('Invalid email'),
  phone: z.string().min(0),
});

type ClientFormData = z.infer<typeof clientSchema>;

interface ClientFormProps {
  onSubmit: (data: CreateClientDto | UpdateClientDto) => void;
  initialData?: ClientFormData;
  isLoading?: boolean;
}

export function ClientForm({ onSubmit, initialData, isLoading }: ClientFormProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<ClientFormData>({
    resolver: zodResolver(clientSchema),
    defaultValues: initialData,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-5">
      <Input
        label="Full Name"
        placeholder="Enter full name"
        {...register('name')}
        error={errors.name?.message}
      />
      <Input
        label="Email Address"
        type="email"
        placeholder="Enter email address"
        {...register('email')}
        error={errors.email?.message}
      />
      <Input
        label="Phone Number"
        type="tel"
        placeholder="Enter phone number (optional)"
        {...register('phone')}
      />
      <div className="flex gap-3 pt-2">
        <Button type="submit" isLoading={isLoading}>
          {isLoading ? 'Saving...' : 'Save Client'}
        </Button>
      </div>
    </form>
  );
}