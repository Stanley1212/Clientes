using MediatR;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand(Guid Id, UpdateClientDto Dto) : IRequest<ClientDto?>;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDto?>
{
    private readonly IClientRepository _repository;

    public UpdateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto?> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        return await _repository.UpdateClientAsync(request.Id, request.Dto);
    }
}