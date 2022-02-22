using AdMicroservice.Entities.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    public interface IAdRepository
    {
        Task<List<AdModel>> GetAds(string publicationDate = null);

        Task<AdModel> GetAdById(Guid AdId);

        Task<AdConfirmation> CreateAd(AdModel ad);

        Task UpdateAd(AdModel ad);

        Task DeleteAd(Guid adId);

        Task<bool> SaveChanges();
    }
}
