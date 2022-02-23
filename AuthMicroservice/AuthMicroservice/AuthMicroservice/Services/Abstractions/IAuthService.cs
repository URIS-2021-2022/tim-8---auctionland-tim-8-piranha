namespace AuthMicroservice.Services.Abstractions
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthResource.Domain;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<SignInResponseDTO> SignInAsync(SignInRequestDTO requestDTO);

        Task<ValidateTokenResponseDTO> ValidateTokenAsync(ValidateTokenRequestDTO requestDTO);
    }
}
