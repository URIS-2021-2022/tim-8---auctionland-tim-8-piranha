namespace AuthMicroservice.Profiles
{
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Domain;
    using AutoMapper;
    using System.Collections.Generic;

    /// <summary>
    /// User type profile.
    /// </summary>
    public class UserTypeProfile : Profile
    {
        /// <summary>
        /// User type profile.
        /// </summary>
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeResponseDTO>();
            CreateMap<UserTypeResponseDTO, UserType>();
            CreateMap<UserType, UserType>();
        }
    }
}
