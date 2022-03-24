using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly RegistrationContext context;
        private readonly IMapper mapper;

        public UserRegistrationRepository(IMapper mapper, RegistrationContext  context)
        {
            this.mapper = mapper;
            this.context = context;
        }
       

        public void CreateUser(Registration register)
        {
            var entity = context.Add(register);
        }

        public Registration GetRegistrationByEmail(string email)
        {
            return context.registration.FirstOrDefault(e => e.Email == email);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;   
        }
    }
}
