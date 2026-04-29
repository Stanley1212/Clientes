using MediatR;
using OrionTek.API.Application.Addresses.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Addresses.Queries.GetAddressesByClient;

public record GetAddressesByClientQuery(Guid ClientId) : IRequest<List<AddressDto>>;

public class GetAddressesByClientQueryHandler : IRequestHandler<GetAddressesByClientQuery, List<AddressDto>>
{
    private readonly IAddressRepository _repository;

    public GetAddressesByClientQueryHandler(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AddressDto>> Handle(GetAddressesByClientQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAddressesByClientAsync(request.ClientId);
    }
}