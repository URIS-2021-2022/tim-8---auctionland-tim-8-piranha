using AutoMapper;
using AddressMicroservice.Data.Interfaces;
using AddressMicroservice.Entities;
using AddressMicroservice.Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressMicroservice.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AddressMicroservice.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressContext Context;
        private readonly IMapper Mapper;

        public AddressRepository(AddressContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public AddressConfirmation CreateAddress(Address Address)
        {
            var createdEntity = Context.Add(Address);
            return Mapper.Map<AddressConfirmation>(createdEntity.Entity);
        }

        public void DeleteAddress(Address address)
        {
            Context.Remove(address);
        }

        public List<Address> GetAddress(string place = null, string street = null)
        {
            return Context.Address
                .AsNoTracking()
                .Include(a => a.State)
                .Where(o => (o.Place == place || place == null) && (o.Street == street || street == null))
                .ToList();
        }

        public Address GetAddressById(Guid AddressId)
        {
            var address = Context.Address
                .Include(a => a.State)
                .FirstOrDefault(o => o.AddressId == AddressId);

            if (address == null) {
                throw new NotFoundException(nameof(Address), AddressId);
            }
            
            return address;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}