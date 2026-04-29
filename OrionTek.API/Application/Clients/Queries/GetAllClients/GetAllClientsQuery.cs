using MediatR;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Clients.Queries.GetAllClients;

public record GetAllClientsQuery : IRequest<List<ClientDto>>;

public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientDto>>
{
    private readonly IClientRepository _repository;

    public GetAllClientsQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllClientsAsync();
    }
}