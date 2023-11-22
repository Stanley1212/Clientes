
using System.ComponentModel.DataAnnotations;

namespace Clientes.Domain.Models
{
	public class Direccion: EntidadBase<int>
	{
        public int ClienteId { get; set; }
		[Required]
        public string Calle { get; set; }
        [Required]
        public string Sector { get; set; }
		public string Municipio { get; set; }
		public string Referencia { get; set; }
        public Cliente Cliente { get; set; }
    }
}
