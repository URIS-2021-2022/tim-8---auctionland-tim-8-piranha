using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IUserRegistrationRepository
    {
        void CreateUser(Registration register);

        Registration GetRegistrationByEmail(string email);

        bool SaveChanges();


    }
}
