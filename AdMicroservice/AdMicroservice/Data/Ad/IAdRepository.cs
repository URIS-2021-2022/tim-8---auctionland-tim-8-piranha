using AdMicroservice.Entities.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    public interface IAdRepository
    {
        List<AdModel> GetAds(string publicationDate = null);

        AdModel GetAdById(Guid AdId);

        AdConfirmation CreateAd(AdModel ad);

        void UpdateAd(AdModel ad);

        void DeleteAd(Guid adId);

        bool SaveChanges();
    }
}
