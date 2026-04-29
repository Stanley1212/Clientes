using OrionTek.API.Application.Clients.DTOs;

namespace OrionTek.API.Application.Common.Interfaces;

public interface IClientRepository
{
    Task<List<ClientDto>> GetAllClientsAsync();
    Task<ClientDto?> GetClientByIdAsync(Guid id);
    Task<ClientDto> CreateClientAsync(CreateClientDto dto);
    Task<ClientDto?> UpdateClientAsync(Guid id, UpdateClientDto dto);
    Task<bool> DeleteClientAsync(Guid id);
}