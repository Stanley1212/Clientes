using AutoMapper;
using OrionTek.API.Domain.Entities;
using OrionTek.API.Application.Clients.DTOs;
using OrionTek.API.Application.Addresses.DTOs;

namespace OrionTek.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

        CreateMap<CreateClientDto, Client>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Addresses, opt => opt.Ignore());

        CreateMap<UpdateClientDto, Client>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore());

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>()
            .ForMember(dest => dest.Client, opt => opt.Ignore());

        CreateMap<CreateAddressDto, Address>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Client, opt => opt.Ignore());

        CreateMap<UpdateAddressDto, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ClientId, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore());
    }
}