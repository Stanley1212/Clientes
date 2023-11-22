
using Clientes.Domain.Models;
using Clientes.Infraestructura.Repositorios;

namespace Clientes.Core.Servicios
{
	public class ClienteService: IClienteService
	{
		private readonly IGenericRepository<Cliente, int> _repository;

		public ClienteService(IGenericRepository<Cliente, int> repository)
        {
			_repository = repository;
		}

		public IEnumerable<Cliente> GetAll()
		{
			return _repository.GetAll().Where(x => x.Estado).ToList();
		}

		public async Task<Cliente> GetById(int id)
		{
			var result = await  _repository.GetById(id);

			if (result is null)
			{
				throw new ArgumentNullException("No se encuentra cliente con el id especificado");
			}

			return result;
		}

		public Task<Cliente> Agregar(Cliente cliente)
		{
			Validar(cliente);
			return _repository.Insert(cliente);
		}

		public void Editar(Cliente cliente)
		{
			Validar(cliente);
			_repository.Update(cliente);
		}

		public async void Delete(int id)
		{
			await _repository.SoftDelete(id);
		}
	
		public void Validar(Cliente cliente)
		{
			if (string.IsNullOrEmpty(cliente.Nombre))
			{
				throw new InvalidDataException("El nombre es obligatorio");
			}

			if (string.IsNullOrEmpty(cliente.Email))
			{
				throw new InvalidDataException("El email es obligatorio");
			}

			if (string.IsNullOrEmpty(cliente.Telefono))
			{
				throw new InvalidDataException("El telefono es obligatorio");
			}
		}
	}
}
