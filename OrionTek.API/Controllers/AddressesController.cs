using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrionTek.API.Application.Addresses.Commands.CreateAddress;
using OrionTek.API.Application.Addresses.Commands.DeleteAddress;
using OrionTek.API.Application.Addresses.Commands.UpdateAddress;
using OrionTek.API.Application.Addresses.DTOs;
using OrionTek.API.Application.Addresses.Queries.GetAddressesByClient;

namespace OrionTek.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("client/{clientId:guid}")]
    public async Task<ActionResult<List<AddressDto>>> GetByClient(Guid clientId)
    {
        var query = new GetAddressesByClientQuery(clientId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("client/{clientId:guid}")]
    public async Task<ActionResult<AddressDto>> Create(Guid clientId, [FromBody] CreateAddressDto dto)
    {
        var command = new CreateAddressCommand(clientId, dto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByClient), new { clientId = result.ClientId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AddressDto>> Update(Guid id, [FromBody] UpdateAddressDto dto)
    {
        var command = new UpdateAddressCommand(id, dto);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var command = new DeleteAddressCommand(id);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return Ok(true);
    }
}