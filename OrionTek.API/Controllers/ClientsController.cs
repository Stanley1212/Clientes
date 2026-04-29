using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrionTek.API.Application.Clients.Commands.CreateClient;
using OrionTek.API.Application.Clients.Commands.DeleteClient;
using OrionTek.API.Application.Clients.Commands.UpdateClient;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Clients.Queries.GetAllClients;
using OrionTek.API.Application.Clients.Queries.GetClientById;

namespace OrionTek.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        var query = new GetAllClientsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id)
    {
        var query = new GetClientByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientDto dto)
    {
        var command = new CreateClientCommand(dto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDto>> Update(Guid id, [FromBody] UpdateClientDto dto)
    {
        var command = new UpdateClientCommand(id, dto);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var command = new DeleteClientCommand(id);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return Ok(true);
    }
}