namespace AuthMicroservice.Profiles
{
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Domain;
    using AutoMapper;
    using System.Collections.Generic;

    /// <summary>
    /// Client profile.
    /// </summary>
    public class ClientProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientProfile()
        {
            CreateMap<Client, ClientResponseDto>();
            CreateMap<ClientResponseDto, Client>();
            CreateMap<Client, Client>();
        }
    }
}
