
using Clientes.Domain.Models;

namespace Clientes.Infraestructura.Repositorios
{
	public interface IGenericRepository<T, TId> where T : EntidadBase<TId>
	{
		Task<T> Insert(T entity);
		Task<T?> GetById(TId id);
		IQueryable<T> GetAll();
		void Update(T entity);
		Task<bool> SoftDelete(TId id);
		Task<bool> HardDelete(TId id);
        Task<int> InsertVoid(T entity);
    }
}
