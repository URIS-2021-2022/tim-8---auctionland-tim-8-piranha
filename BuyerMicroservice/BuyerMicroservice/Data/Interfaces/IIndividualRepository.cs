using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface IIndividualRepository
    {
        Task<List<Individual>> GetIndividualAsync(string JMBG = null);

        Task<Individual> GetIndividualByIdAsync(Guid buyerID);

        Task<IndividualConfirmation> CreateIndividualAsync(Individual individual);

        Task UpdateIndividualAsync(Individual individual);

        Task DeleteIndividualAsync(Guid buyerID);

        Task<bool> SaveChangesAsync();
        
    }
}
