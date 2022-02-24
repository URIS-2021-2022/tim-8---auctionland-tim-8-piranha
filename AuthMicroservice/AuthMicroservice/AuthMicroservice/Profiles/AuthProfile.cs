namespace AuthMicroservice.Profiles
{
    using AuthMicroservice.Domain;
    using AuthResource.Domain;
    using AutoMapper;

    /// <summary>
    /// Profile for auth.
    /// </summary>
    public class AuthProfile : Profile
    {
        /// <summary>
        /// Auth profile constructor.
        /// </summary>
        public AuthProfile()
        {
            CreateMap<AuthenticatedUser, AuthenticatedUser>();
            CreateMap<EmailAndPassword, EmailAndPassword>();
        }
    }
}
