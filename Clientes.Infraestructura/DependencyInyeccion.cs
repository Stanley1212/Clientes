using Clientes.Domain.Models;
using Clientes.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Infraestructura
{
	public static class DependencyInyeccion
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AppDbContext>(options =>
					options.UseSqlServer(connectionString));
			return services;
		}

		public static IServiceCollection AddRepositorios(this IServiceCollection services)
		{
			services.AddScoped<IGenericRepository<Cliente, int>, GenericRepository<Cliente, int>>();
			services.AddScoped<IGenericRepository<Direccion, int>, GenericRepository<Direccion, int>>();
			return services;
		}
	}
}
