using MediatR;
using OrionTek.API.Application.Addresses.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Addresses.Commands.CreateAddress;

public record CreateAddressCommand(Guid ClientId, CreateAddressDto Dto) : IRequest<AddressDto>;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressDto>
{
    private readonly IAddressRepository _repository;

    public CreateAddressCommandHandler(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        return await _repository.CreateAddressAsync(request.ClientId, request.Dto);
    }
}