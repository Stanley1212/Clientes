import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useClients, useCreateClient, useDeleteClient } from '../hooks/useClients';
import { Button } from '../components/ui/Button';
import { Modal } from '../components/ui/Modal';
import { ClientForm } from '../components/ClientForm';
import { PlusIcon, UserIcon, TrashIcon } from '../components/ui/Icons';
import type { CreateClientDto } from '../types';

export function ClientsPage() {
  const { data: clients, isLoading, error } = useClients();
  const createClient = useCreateClient();
  const deleteClient = useDeleteClient();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const handleCreate = async (data: CreateClientDto) => {
    try {
      await createClient.mutateAsync(data);
      setIsModalOpen(false);
      setSuccessMessage('Client created successfully');
      setTimeout(() => setSuccessMessage(null), 3000);
    } catch {
      // Error handled by React Query
    }
  };

  const handleDelete = async (id: string, name: string) => {
    if (window.confirm(`Are you sure you want to delete ${name}?`)) {
      try {
        await deleteClient.mutateAsync(id);
        setSuccessMessage('Client deleted successfully');
        setTimeout(() => setSuccessMessage(null), 3000);
      } catch {
        // Error handled by React Query
      }
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="text-center">
          <p className="text-red-600 mb-4">Error loading clients</p>
          <Button variant="secondary" onClick={() => window.location.reload()}>
            Retry
          </Button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-50">
      <header className="bg-white border-b border-slate-200 sticky top-0 z-10">
        <div className="max-w-5xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center h-16">
            <div className="flex items-center gap-3">
              <div className="bg-blue-600 p-2 rounded-lg">
                <UserIcon className="w-5 h-5 text-white" />
              </div>
              <h1 className="text-xl font-semibold text-slate-900">Clients</h1>
            </div>
            <Button onClick={() => setIsModalOpen(true)}>
              <PlusIcon className="w-4 h-4 mr-2" />
              Add Client
            </Button>
          </div>
        </div>
      </header>

      <main className="max-w-5xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {successMessage && (
          <div className="mb-6 p-4 bg-green-50 border border-green-200 rounded-lg text-green-700">
            {successMessage}
          </div>
        )}

        {clients?.length === 0 ? (
          <div className="bg-white rounded-xl shadow-sm border border-slate-200 p-12 text-center">
            <div className="w-16 h-16 bg-slate-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <UserIcon className="w-8 h-8 text-slate-400" />
            </div>
            <h2 className="text-lg font-medium text-slate-900 mb-2">No clients yet</h2>
            <p className="text-slate-500 mb-6">Get started by adding your first client</p>
            <Button onClick={() => setIsModalOpen(true)}>
              <PlusIcon className="w-4 h-4 mr-2" />
              Add Your First Client
            </Button>
          </div>
        ) : (
          <div className="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead className="bg-slate-50 border-b border-slate-200">
                  <tr>
                    <th className="text-left px-6 py-4 text-sm font-medium text-slate-600">Name</th>
                    <th className="text-left px-6 py-4 text-sm font-medium text-slate-600">Email</th>
                    <th className="text-left px-6 py-4 text-sm font-medium text-slate-600">Phone</th>
                    <th className="text-right px-6 py-4 text-sm font-medium text-slate-600">Actions</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-200">
                  {clients?.map((client) => (
                    <tr 
                      key={client.id} 
                      className="hover:bg-slate-50 transition-colors duration-150"
                    >
                      <td className="px-6 py-4">
                        <Link 
                          to={`/clients/${client.id}`}
                          className="font-medium text-slate-900 hover:text-blue-600 transition-colors cursor-pointer"
                        >
                          {client.name}
                        </Link>
                      </td>
                      <td className="px-6 py-4 text-slate-600">{client.email}</td>
                      <td className="px-6 py-4 text-slate-600">{client.phone || '-'}</td>
                      <td className="px-6 py-4">
                        <div className="flex items-center justify-end gap-2">
                          <Link to={`/clients/${client.id}`}>
                            <Button variant="secondary">View</Button>
                          </Link>
                          <Button
                            variant="danger"
                            onClick={() => handleDelete(client.id, client.name)}
                          >
                            <TrashIcon className="w-4 h-4" />
                          </Button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </main>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <div className="p-6">
          <h2 className="text-xl font-semibold text-slate-900 mb-6">New Client</h2>
          <ClientForm
            onSubmit={handleCreate}
            isLoading={createClient.isPending}
          />
        </div>
      </Modal>
    </div>
  );
}