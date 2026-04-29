using MediatR;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Clients.Commands.CreateClient;

public record CreateClientCommand(CreateClientDto Dto) : IRequest<ClientDto>;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IClientRepository _repository;

    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        return await _repository.CreateClientAsync(request.Dto);
    }
}