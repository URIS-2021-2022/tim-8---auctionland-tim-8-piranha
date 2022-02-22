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

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<AdConfirmation> CreateAd(AdModel ad)
        {
            var createdEntity = await context.Ads.AddAsync(ad);
            context.SaveChanges();
            return mapper.Map<AdConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteAd(Guid adId)
        {
            var ad = await GetAdById(adId);
            context.Ads.Remove(ad);
            context.SaveChanges();
        }

        public async Task<List<AdModel>> GetAds(string publicationDate = null)
        {
            return await context.Ads.Include(a => a.Journal).Where(e => publicationDate == null || e.PublicationDate == publicationDate).ToListAsync();

        }

        public async Task<AdModel> GetAdById(Guid adId)
        {
            return await context.Ads.Include(a => a.Journal).FirstOrDefaultAsync(e => e.AdId == adId);
        }

#pragma warning disable CS1998
        public async Task UpdateAd(AdModel ad)
        {
            //Ne treba da se implementira zbog EF
        }
#pragma warning restore CS1998
    }
}
