using MediatR;
using OrionTek.API.Application.Common.Interfaces;

namespace OrionTek.API.Application.Clients.Commands.DeleteClient;

public record DeleteClientCommand(Guid Id) : IRequest<bool>;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IClientRepository _repository;

    public DeleteClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteClientAsync(request.Id);
    }
}