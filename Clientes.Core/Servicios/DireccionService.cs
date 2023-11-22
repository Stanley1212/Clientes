
using Clientes.Domain.Models;
using Clientes.Infraestructura.Repositorios;

namespace Clientes.Core.Servicios
{
	public class DireccionService: IDireccionService
	{
		private readonly IGenericRepository<Direccion, int> _repository;

		public DireccionService(IGenericRepository<Direccion, int> repository)
        {
			_repository = repository;
		}

		public IEnumerable<Direccion> GetAll()
		{
			return _repository.GetAll().Where(x => x.Estado).ToList();
		}

		public IEnumerable<Direccion> GetAllByIdCliente(int idCliente)
		{
			return _repository.GetAll().Where(x => x.Estado && x.ClienteId == idCliente).ToList();
		}

		public async Task<Direccion> GetById(int id)
		{
			var result = await _repository.GetById(id);

			if (result is null)
			{
				throw new ArgumentNullException("No se encuentra cliente con el id especificado");
			}

			return result;
		}

		public Task<Direccion> Agregar(Direccion direccion)
		{
			Validar(direccion);
			return _repository.Insert(direccion);
		}

		public void Editar(Direccion direccion)
		{
			Validar(direccion);
			_repository.Update(direccion);
		}

		public async void Delete(int id)
		{
		 await _repository.SoftDelete(id);
		}

		public void Validar(Direccion direccion)
		{
			if (direccion.ClienteId == 0)
			{
				throw new InvalidDataException("El campo IdCliente es obligatorio");
			}

			if (string.IsNullOrEmpty(direccion.Calle))
			{
				throw new InvalidDataException("El campo calle es obligatorio");
			}

			if (string.IsNullOrEmpty(direccion.Sector))
			{
				throw new InvalidDataException("El Sector es obligatorio");
			}

            if (string.IsNullOrEmpty(direccion.Municipio))
            {
                throw new InvalidDataException("El municipio es obligatorio");
            }

            if (string.IsNullOrEmpty(direccion.Referencia))
			{
				throw new InvalidDataException("El campo referencia es obligatorio");
			}
		}
	}
}
