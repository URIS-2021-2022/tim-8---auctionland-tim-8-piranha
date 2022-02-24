using AddressMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Data.Interfaces
{
    public interface IAddressRepository
    {
        List<Address> GetAddress(string place = null, string street = null);

        Address GetAddressById(Guid addressId);

        AddressConfirmation CreateAddress(Address address);

        void DeleteAddress(Address address);

        void SaveChanges();
    }
}