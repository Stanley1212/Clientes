

using System.ComponentModel.DataAnnotations;

namespace Clientes.Domain.Models
{
	public class Cliente: EntidadBase<int>
	{
		[Required]
        [MinLength(3)]
		public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Telefono { get; set; }

        public ICollection<Direccion>? Direcciones { get; set; } = new List<Direccion>();
    }
}
