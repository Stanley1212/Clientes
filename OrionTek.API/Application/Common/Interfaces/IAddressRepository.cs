using OrionTek.API.Application.Addresses.DTOs;

namespace OrionTek.API.Application.Common.Interfaces;

public interface IAddressRepository
{
    Task<List<AddressDto>> GetAddressesByClientAsync(Guid clientId);
    Task<AddressDto> CreateAddressAsync(Guid clientId, CreateAddressDto dto);
    Task<AddressDto?> UpdateAddressAsync(Guid id, UpdateAddressDto dto);
    Task<bool> DeleteAddressAsync(Guid id);
}