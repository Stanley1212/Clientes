using MediatR;
using OrionTek.API.Application.Addresses.DTOs;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Addresses.Commands.UpdateAddress;

public record UpdateAddressCommand(Guid Id, UpdateAddressDto Dto) : IRequest<AddressDto?>;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressDto?>
{
    private readonly IAddressRepository _repository;

    public UpdateAddressCommandHandler(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddressDto?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAddressAsync(request.Id, request.Dto);
    }
}