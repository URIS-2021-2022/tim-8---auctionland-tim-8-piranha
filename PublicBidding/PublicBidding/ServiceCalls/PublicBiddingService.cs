using AutoMapper;
using Microsoft.Extensions.Configuration;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class PublicBiddingService : IPublicBiddingService
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IService<BuyerDto> buyerService;
        private readonly IService<AddressDto> addressService;
        private readonly IService<PlotPartDto> plotPartService;
        private readonly IService<AuthorizedPersonDto> authorizedPersonService;

        public PublicBiddingService(IConfiguration configuration, IMapper mapper, IService<BuyerDto> buyerService, IService<AddressDto> addressService, IService<PlotPartDto> plotPartService, IService<AuthorizedPersonDto> authorizedPersonService)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.buyerService = buyerService;
            this.addressService = addressService;
            this.plotPartService = plotPartService;
            this.authorizedPersonService = authorizedPersonService;
        }

        public async Task<PublicBiddingDto> GetInfoForListsInPublicBidding(Entities.PublicBidding publicBidding)
        {
            var publicBiddingDto = mapper.Map<PublicBiddingDto>(publicBidding);

            //Komunikacija sa Address mikroservisom 
            string addressUrl = configuration["Services:AddressService"];
            if (publicBidding.AddressId is not null)
            {
                var addressDto = await addressService.SendGetRequestAsync(addressUrl + publicBidding.AddressId);
                if (addressDto is not null)
                    publicBiddingDto.Address = addressDto;
            }

            //Komunikacija sa Buyer mikroservisom
            //BestBidder
            string buyerUrl = configuration["Services:BuyerService"];
            if (publicBidding.BestBidder is not null)
            {
                var buyerDto = await buyerService.SendGetRequestAsync(buyerUrl + publicBidding.BestBidder);
                if (buyerDto is not null)
                    publicBiddingDto.BestBidder = buyerDto;
            }

            //Lista Buyers
            publicBiddingDto.Bidders = new List<BuyerDto>();
            foreach (var buyer in publicBiddingDto.Bidders)
            {
                var buyerDto = await buyerService.SendGetRequestAsync(buyerUrl + buyer);
                if (buyerDto is not null)
                    publicBiddingDto.Bidders.Add(buyerDto);
            }

            //Komunikacija sa AuthorizedPerson mikroservisom
            string authorizedPersonUrl = configuration["Services:AuthorizedPersonService"];
            publicBiddingDto.AuthorizedPersons = new List<AuthorizedPersonDto>();
            foreach (var authorizedPerson in publicBiddingDto.AuthorizedPersons)
            {
                var authorizedPersonDto = await authorizedPersonService.SendGetRequestAsync(authorizedPersonUrl + authorizedPerson);
                if (authorizedPersonDto is not null)
                    publicBiddingDto.AuthorizedPersons.Add(authorizedPersonDto);
            }

            //Komunikacija sa Plot mikroservisom
            string plotPartUrl = configuration["Services:PlotPartService"];
            publicBiddingDto.PlotParts = new List<PlotPartDto>();
            foreach (var plotPart in publicBiddingDto.PlotParts)
            {
                var plotPartDto = await plotPartService.SendGetRequestAsync(plotPartUrl + plotPart);
                if (plotPartDto is not null)
                    publicBiddingDto.PlotParts.Add(plotPartDto);
            }

            return publicBiddingDto;
        }
    }
}
