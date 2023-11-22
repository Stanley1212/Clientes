
using Clientes.Domain.Models;

namespace Clientes.Core.Servicios
{
    public interface IClienteService
    {
        Task<Cliente> Agregar(Cliente cliente);
        void Delete(int id);
        void Editar(Cliente cliente);
        IEnumerable<Cliente> GetAll();
        Task<Cliente> GetById(int id);
		void Validar(Cliente cliente);
	}
}
