using AutoMapper;
using PublicBidding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = PublicBidding.Entities.Type;

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

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public PublicBiddingConfirmation CreatePublicBidding(Entities.PublicBidding publicBidding)
        {
            var createdEntity = context.Add(publicBidding);
            return mapper.Map<PublicBiddingConfirmation>(createdEntity.Entity);
        }

        public void DeletePublicBidding(Guid publicBiddingId)
        {
            var publicBidding = GetPublicBiddingById(publicBiddingId);
            context.Remove(publicBidding);
        }

        public Entities.PublicBidding GetPublicBiddingById(Guid publicBiddingId)
        {
            return context.PublicBiddings.FirstOrDefault(e => e.PublicBiddingId == publicBiddingId);
        }

        public List<Entities.PublicBidding> GetPublicBiddings(int numberOfApplicants = 0, Type type = null, Status status = null)
        {
            return context.PublicBiddings.Where(e => (numberOfApplicants == 0 || e.NumberOfApplicants >= numberOfApplicants) &&
                                                        (type == null || e.Type.TypeName == type.TypeName) &&
                                                        (status == null || e.Status.StatusName == status.StatusName)).ToList();
        }

        public void UpdatePublicBidding(Entities.PublicBidding publicBidding)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
