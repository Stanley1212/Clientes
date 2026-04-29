using MediatR;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Clients.Queries.GetClientById;

public record GetClientByIdQuery(Guid Id) : IRequest<ClientDto?>;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetClientByIdAsync(request.Id);
    }
}