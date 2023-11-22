using Clientes.Core.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Core
{
    public static class DependencyInyeccion
    {
        public static IServiceCollection AddCoreServicios(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IDireccionService, DireccionService>();
            return services;
        }
    }
}
