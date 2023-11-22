using Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infraestructura
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}


		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Direccion> Direcciones { get; set; }
	}
}
