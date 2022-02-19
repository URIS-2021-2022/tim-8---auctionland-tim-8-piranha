using AdMicroservice.Entities;
using AdMicroservice.Entities.Ad;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.Ad
{
    public class AdRepository : IAdRepository
    {
        private readonly AdContext context;
        private readonly IMapper mapper;

        public AdRepository(AdContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public AdConfirmation CreateAd(AdModel ad)
        {
            var createdEntity = context.Ads.Add(ad);
            context.SaveChanges();
            return mapper.Map<AdConfirmation>(createdEntity.Entity);
        }

        public void DeleteAd(Guid adId)
        {
            var ad = GetAdById(adId);
            context.Ads.Remove(ad);
            context.SaveChanges();
        }

        public List<AdModel> GetAds(string publicationDate = null)
        {
            return context.Ads.Include(a => a.Journal).Where(e => publicationDate == null || e.PublicationDate == publicationDate).ToList();

        }

        public AdModel GetAdById(Guid adId)
        {
            return context.Ads.Include(a => a.Journal).FirstOrDefault(e => e.AdId == adId);
        }

        public void UpdateAd(AdModel ad)
        {
            //NE GLEDAJ OVAJ KOD   
        }
    }
}
