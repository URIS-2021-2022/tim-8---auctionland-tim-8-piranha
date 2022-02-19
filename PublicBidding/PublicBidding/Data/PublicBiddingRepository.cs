using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public class PublicBiddingRepository : IPublicBiddingRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public PublicBiddingRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<PublicBiddingConfirmation> CreatePublicBidding(Entities.PublicBidding publicBidding)
        {
            //Izračunavanje dopune depozita
            if (publicBidding.Price > (publicBidding.StartPricePerHa * 2))
            {
                publicBidding.DepositSupplement = (int)(publicBidding.Price * 0.5);
            }
            else
            {
                publicBidding.DepositSupplement = (int)(publicBidding.Price * 0.1);
            }

            List<Guid> authorizedPersons = publicBidding.AuthorizedPersons;
            List<Guid> buyers = publicBidding.Bidders;
            List<Guid> plotParts = publicBidding.Plots;

            if(authorizedPersons != null)
            {
                foreach (var authorizedPersonId in authorizedPersons)
                {
                    var publicBiddingAuthorizedPerson = new PublicBiddingAuthorizedPerson
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        AuthorizedPersonId = authorizedPersonId
                    };
                    await context.PublicBiddingAuthorizedPerson.AddAsync(publicBiddingAuthorizedPerson);
                }
            }
            
            if(buyers != null)
            {
                foreach (var buyerId in buyers)
                {
                    var publicBiddingBuyer = new PublicBiddingBuyer
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        BuyerId = buyerId
                    };
                    await context.PublicBiddingBuyer.AddAsync(publicBiddingBuyer);
                }
            }

            if(plotParts != null)
            {
                foreach (var plotPartId in plotParts)
                {
                    var publicBiddingPlotPart = new PublicBiddingPlotPart
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        PlotPartId = plotPartId
                    };
                    await context.PublicBiddingPlotPart.AddAsync(publicBiddingPlotPart);
                }
            }

            var createdEntity = await context.PublicBiddings.AddAsync(publicBidding);
            return mapper.Map<PublicBiddingConfirmation>(createdEntity.Entity);
        }

        public async Task DeletePublicBidding(Guid publicBiddingId)
        {
            var publicBidding = await GetPublicBiddingById(publicBiddingId);
            context.Remove(publicBidding);
        }

        public async Task<Entities.PublicBidding> GetPublicBiddingById(Guid publicBiddingId)
        {
            var publicBidding = await context.PublicBiddings.Include(s => s.Status).Include(t => t.Type)
                .FirstOrDefaultAsync(pb => pb.PublicBiddingId == publicBiddingId);

            if (publicBidding is not null)
            {
                publicBidding.AuthorizedPersons = await context.PublicBiddingAuthorizedPerson.Where(pa => pa.PublicBiddingId == publicBidding.PublicBiddingId).Select(a => a.AuthorizedPersonId).ToListAsync();
                publicBidding.Bidders = await context.PublicBiddingBuyer.Where(pb => pb.PublicBiddingId == publicBidding.PublicBiddingId).Select(b => b.BuyerId).ToListAsync();
                publicBidding.Plots = await context.PublicBiddingPlotPart.Where(pp => pp.PublicBiddingId == publicBidding.PublicBiddingId).Select(p => p.PlotPartId).ToListAsync();
            }

            return publicBidding;
        }

        public async Task<List<Entities.PublicBidding>> GetPublicBiddings()
        {
            var publicBiddings = await context.PublicBiddings.Include(s => s.Status).Include(t => t.Type)
                .ToListAsync();

            foreach (var publicBidding in publicBiddings)
            {
                publicBidding.AuthorizedPersons = await context.PublicBiddingAuthorizedPerson.Where(pa => pa.PublicBiddingId == publicBidding.PublicBiddingId).Select(a => a.AuthorizedPersonId).ToListAsync();
                publicBidding.Bidders = await context.PublicBiddingBuyer.Where(pb => pb.PublicBiddingId == publicBidding.PublicBiddingId).Select(b => b.BuyerId).ToListAsync();
                publicBidding.Plots = await context.PublicBiddingPlotPart.Where(pp => pp.PublicBiddingId == publicBidding.PublicBiddingId).Select(p => p.PlotPartId).ToListAsync();
            }

            return publicBiddings;
        }

        public async Task UpdatePublicBidding(Entities.PublicBidding publicBidding)
        {
            var authorizedPerson = await context.PublicBiddingAuthorizedPerson.Where(pa => pa.PublicBiddingId == publicBidding.PublicBiddingId).ToListAsync();
            context.PublicBiddingAuthorizedPerson.RemoveRange(authorizedPerson);

            var buyer = await context.PublicBiddingBuyer.Where(pb => pb.PublicBiddingId == publicBidding.PublicBiddingId).ToListAsync();
            context.PublicBiddingBuyer.RemoveRange(buyer);

            var plotPart = await context.PublicBiddingPlotPart.Where(pp => pp.PublicBiddingId == publicBidding.PublicBiddingId).ToListAsync();
            context.PublicBiddingPlotPart.RemoveRange(plotPart);

            List<Guid> authorizedPersons = publicBidding.AuthorizedPersons;
            List<Guid> buyers = publicBidding.Bidders;
            List<Guid> plotParts = publicBidding.Plots;

            if (authorizedPersons != null)
            {
                foreach (var authorizedPersonId in authorizedPersons)
                {
                    var publicBiddingAuthorizedPerson = new PublicBiddingAuthorizedPerson
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        AuthorizedPersonId = authorizedPersonId
                    };
                    await context.PublicBiddingAuthorizedPerson.AddAsync(publicBiddingAuthorizedPerson);
                }
            }

            if (buyers != null)
            {
                foreach (var buyerId in buyers)
                {
                    var publicBiddingBuyer = new PublicBiddingBuyer
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        BuyerId = buyerId
                    };
                    await context.PublicBiddingBuyer.AddAsync(publicBiddingBuyer);
                }
            }

            if (plotParts != null)
            {
                foreach (var plotPartId in plotParts)
                {
                    var publicBiddingPlotPart = new PublicBiddingPlotPart
                    {
                        PublicBiddingId = publicBidding.PublicBiddingId,
                        PlotPartId = plotPartId
                    };
                    await context.PublicBiddingPlotPart.AddAsync(publicBiddingPlotPart);
                }
            }
        }
    }
}
