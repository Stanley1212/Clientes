using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Common.Interfaces;
using OrionTek.API.Domain.Entities;
using OrionTek.API.Infrastructure.Persistence;

namespace OrionTek.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClientRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ClientDto>> GetAllClientsAsync()
    {
        var clients = await _context.Clients
            .Include(c => c.Addresses)
            .ToListAsync();
        return _mapper.Map<List<ClientDto>>(clients);
    }

    public async Task<ClientDto?> GetClientByIdAsync(Guid id)
    {
        var client = await _context.Clients
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Id == id);
        return client == null ? null : _mapper.Map<ClientDto>(client);
    }

    public async Task<ClientDto> CreateClientAsync(CreateClientDto dto)
    {
        var client = _mapper.Map<Client>(dto);
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClientDto>(client);
    }

    public async Task<ClientDto?> UpdateClientAsync(Guid id, UpdateClientDto dto)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return null;

        _mapper.Map(dto, client);
        await _context.SaveChangesAsync();
        return _mapper.Map<ClientDto>(client);
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}