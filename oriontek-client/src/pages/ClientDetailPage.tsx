import { useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { useClient, useUpdateClient, useDeleteClient } from '../hooks/useClients';
import { useAddresses, useCreateAddress, useDeleteAddress } from '../hooks/useAddresses';
import { Button } from '../components/ui/Button';
import { Modal } from '../components/ui/Modal';
import { ClientForm } from '../components/ClientForm';
import { AddressForm } from '../components/AddressForm';
import { ArrowLeftIcon, PlusIcon, TrashIcon, PencilIcon, MapPinIcon, MailIcon, PhoneIcon } from '../components/ui/Icons';
import type { UpdateClientDto, CreateAddressDto } from '../types';

export function ClientDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { data: client, isLoading, error } = useClient(id!);
  const { data: addresses } = useAddresses(id!);
  const updateClient = useUpdateClient();
  const deleteClient = useDeleteClient();
  const createAddress = useCreateAddress();
  const deleteAddress = useDeleteAddress();

  const [isEditClientOpen, setIsEditClientOpen] = useState(false);
  const [isAddAddressOpen, setIsAddAddressOpen] = useState(false);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const handleUpdateClient = async (data: UpdateClientDto) => {
    await updateClient.mutateAsync({ id: id!, data });
    setIsEditClientOpen(false);
    setSuccessMessage('Client updated successfully');
    setTimeout(() => setSuccessMessage(null), 3000);
  };

  const handleAddAddress = async (data: CreateAddressDto) => {
    await createAddress.mutateAsync({ clientId: id!, data });
    setIsAddAddressOpen(false);
    setSuccessMessage('Address added successfully');
    setTimeout(() => setSuccessMessage(null), 3000);
  };

  const handleDeleteClient = async () => {
    if (window.confirm('Are you sure you want to delete this client? This will also delete all addresses.')) {
      await deleteClient.mutateAsync(id!);
      navigate('/clients');
    }
  };

  const handleDeleteAddress = async (addressId: string) => {
    if (window.confirm('Are you sure you want to delete this address?')) {
      await deleteAddress.mutateAsync(addressId);
      setSuccessMessage('Address deleted successfully');
      setTimeout(() => setSuccessMessage(null), 3000);
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (error || !client) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="text-center">
          <p className="text-red-600 mb-4">Error loading client</p>
          <Link to="/clients">
            <Button variant="secondary">Back to Clients</Button>
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-50">
      <header className="bg-white border-b border-slate-200 sticky top-0 z-10">
        <div className="max-w-5xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center h-16">
            <Link 
              to="/clients" 
              className="flex items-center gap-2 text-slate-600 hover:text-slate-900 transition-colors cursor-pointer"
            >
              <ArrowLeftIcon className="w-5 h-5" />
              <span>Back to Clients</span>
            </Link>
            <div className="flex items-center gap-2">
              <Button variant="secondary" onClick={() => setIsEditClientOpen(true)}>
                <PencilIcon className="w-4 h-4 mr-2" />
                Edit
              </Button>
              <Button variant="danger" onClick={handleDeleteClient}>
                <TrashIcon className="w-4 h-4 mr-2" />
                Delete
              </Button>
            </div>
          </div>
        </div>
      </header>

      <main className="max-w-5xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {successMessage && (
          <div className="mb-6 p-4 bg-green-50 border border-green-200 rounded-lg text-green-700">
            {successMessage}
          </div>
        )}

        <div className="bg-white rounded-xl shadow-sm border border-slate-200 p-6 mb-8">
          <div className="flex items-start gap-4">
            <div className="w-16 h-16 bg-blue-100 rounded-full flex items-center justify-center flex-shrink-0">
              <span className="text-2xl font-semibold text-blue-600">
                {client.name.charAt(0).toUpperCase()}
              </span>
            </div>
            <div className="flex-1">
              <h1 className="text-2xl font-semibold text-slate-900 mb-4">{client.name}</h1>
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div className="flex items-center gap-3 text-slate-600">
                  <MailIcon className="w-5 h-5 text-slate-400" />
                  <span>{client.email}</span>
                </div>
                <div className="flex items-center gap-3 text-slate-600">
                  <PhoneIcon className="w-5 h-5 text-slate-400" />
                  <span>{client.phone || '-'}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
          <div className="px-6 py-4 border-b border-slate-200 flex justify-between items-center">
            <h2 className="text-lg font-semibold text-slate-900">Addresses</h2>
            <Button onClick={() => setIsAddAddressOpen(true)}>
              <PlusIcon className="w-4 h-4 mr-2" />
              Add Address
            </Button>
          </div>

          {addresses?.length === 0 ? (
            <div className="p-12 text-center">
              <div className="w-12 h-12 bg-slate-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <MapPinIcon className="w-6 h-6 text-slate-400" />
              </div>
              <p className="text-slate-500 mb-4">No addresses yet</p>
              <Button onClick={() => setIsAddAddressOpen(true)}>
                <PlusIcon className="w-4 h-4 mr-2" />
                Add First Address
              </Button>
            </div>
          ) : (
            <div className="divide-y divide-slate-200">
              {addresses?.map((address) => (
                <div key={address.id} className="px-6 py-4 flex items-start justify-between hover:bg-slate-50 transition-colors">
                  <div className="flex items-start gap-3">
                    <MapPinIcon className="w-5 h-5 text-slate-400 mt-0.5" />
                    <div>
                      <p className="font-medium text-slate-900">{address.street}</p>
                      <p className="text-slate-600">
                        {address.city}{address.state && `, ${address.state}`} {address.zipCode}
                      </p>
                      <p className="text-slate-600">{address.country}</p>
                    </div>
                  </div>
                  <Button
                    variant="danger"
                    onClick={() => handleDeleteAddress(address.id)}
                  >
                    <TrashIcon className="w-4 h-4" />
                  </Button>
                </div>
              ))}
            </div>
          )}
        </div>
      </main>

      <Modal isOpen={isEditClientOpen} onClose={() => setIsEditClientOpen(false)}>
        <div className="p-6">
          <h2 className="text-xl font-semibold text-slate-900 mb-6">Edit Client</h2>
          <ClientForm
            onSubmit={handleUpdateClient}
            initialData={client}
            isLoading={updateClient.isPending}
          />
        </div>
      </Modal>

      <Modal isOpen={isAddAddressOpen} onClose={() => setIsAddAddressOpen(false)}>
        <div className="p-6">
          <h2 className="text-xl font-semibold text-slate-900 mb-6">Add Address</h2>
          <AddressForm onSubmit={handleAddAddress} isLoading={createAddress.isPending} />
        </div>
      </Modal>
    </div>
  );
}