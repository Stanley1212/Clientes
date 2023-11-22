using Clientes.Core.Servicios;
using Clientes.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            this._clienteService = clienteService;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _clienteService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Cliente> Get(int id)
        {
            return await _clienteService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {

            return Created("",await _clienteService.Agregar(cliente));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Cliente cliente)
        {
            _clienteService.Editar(cliente);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clienteService.Delete(id);
            return Accepted();
        }
    }
}
