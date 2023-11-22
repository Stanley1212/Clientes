

using Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infraestructura.Repositorios
{
	public class GenericRepository<T, TId> : IGenericRepository<T, TId>
											where T : EntidadBase<TId>
											where TId : IEquatable<TId>
	{
		private readonly AppDbContext _context;
		protected DbSet<T> Entities => _context.Set<T>();

		public GenericRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<T> Insert(T entity)
		{
            entity.Estado = true;
            var insertedValue = Entities.Add(entity);
            _context.SaveChanges();
			return insertedValue.Entity;
		}
        public async Task<int> InsertVoid(T entity)
        {
			Entities.Add(entity);
            return _context.SaveChanges();
        }

        public async Task<T?> GetById(TId id)
			=> await _context.Set<T>()
				.FindAsync(id);


		public IQueryable<T> GetAll()
			=> _context.Set<T>();

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

		public async Task<bool> SoftDelete(TId id)
		{
			T? entity = await GetById(id);

			if (entity is null)
				return false;

			entity.Estado = false;

			_context.Set<T>().Update(entity);
           _context.SaveChanges();
            return true;
		}

		public async Task<bool> HardDelete(TId id)
		{
			T? entity = await GetById(id);
			if (entity is null)
				return false;
			_context.Set<T>().Remove(entity);
			return true;
		}
	}
}
