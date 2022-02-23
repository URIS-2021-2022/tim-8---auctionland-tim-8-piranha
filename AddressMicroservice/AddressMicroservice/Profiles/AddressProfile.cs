using AutoMapper;
using AddressMicroservice.Entities;
using AddressMicroservice.Models.Address;

namespace AddressMicroservice.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressCreationDto, Address>();
            CreateMap<Address, AddressConfirmation>();
            CreateMap<AddressConfirmation, AddressConfirmationDto>();
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<Address, Address>();
        }
    }
}