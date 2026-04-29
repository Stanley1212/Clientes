using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrionTek.API.Application.Addresses.DTOs;
using OrionTek.API.Application.Common.Interfaces;
using OrionTek.API.Domain.Entities;
using OrionTek.API.Infrastructure.Persistence;

namespace OrionTek.API.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AddressRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AddressDto>> GetAddressesByClientAsync(Guid clientId)
    {
        var addresses = await _context.Addresses
            .Where(a => a.ClientId == clientId)
            .ToListAsync();
        return _mapper.Map<List<AddressDto>>(addresses);
    }

    public async Task<AddressDto> CreateAddressAsync(Guid clientId, CreateAddressDto dto)
    {
        var address = _mapper.Map<Address>(dto);
        address.ClientId = clientId;
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddressDto>(address);
    }

    public async Task<AddressDto?> UpdateAddressAsync(Guid id, UpdateAddressDto dto)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return null;

        _mapper.Map(dto, address);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddressDto>(address);
    }

    public async Task<bool> DeleteAddressAsync(Guid id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return false;

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return true;
    }
}