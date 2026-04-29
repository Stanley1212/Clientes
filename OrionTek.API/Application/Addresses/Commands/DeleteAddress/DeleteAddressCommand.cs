using MediatR;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Addresses.Commands.DeleteAddress;

public record DeleteAddressCommand(Guid Id) : IRequest<bool>;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly IAddressRepository _repository;

    public DeleteAddressCommandHandler(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAddressAsync(request.Id);
    }
}