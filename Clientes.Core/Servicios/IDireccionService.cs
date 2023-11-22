
using Clientes.Domain.Models;

namespace Clientes.Core.Servicios
{
	public interface IDireccionService
	{
		Task<Direccion> Agregar(Direccion direccion);
		void Delete(int id);
		void Editar(Direccion direccion);
		IEnumerable<Direccion> GetAll();
		IEnumerable<Direccion> GetAllByIdCliente(int idCliente);
		Task<Direccion> GetById(int id);
		void Validar(Direccion direccion);
	}
}
