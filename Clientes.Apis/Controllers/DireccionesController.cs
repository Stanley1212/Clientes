using Clientes.Core.Servicios;
using Clientes.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionesController : ControllerBase
    {
        private readonly IDireccionService _direccionService;

        public DireccionesController(IDireccionService direccionService)
        {
            this._direccionService = direccionService;
        }

        [HttpGet]
        public IEnumerable<Direccion> Get()
        {
            return _direccionService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Direccion> Get(int id)
        {
            return await _direccionService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Direccion direccion)
        {
            return Created("",await _direccionService.Agregar(direccion));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Direccion direccion)
        {
            _direccionService.Editar(direccion);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _direccionService.Delete(id);
            return Accepted();
        }
    }
}
