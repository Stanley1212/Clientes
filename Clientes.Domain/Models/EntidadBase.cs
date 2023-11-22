
namespace Clientes.Domain.Models
{
	public class EntidadBase<TId>
	{
        public TId Id { get; set; }
        public bool Estado { get; set; }
    }
}
