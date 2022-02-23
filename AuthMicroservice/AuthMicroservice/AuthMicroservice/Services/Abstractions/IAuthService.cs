namespace AuthMicroservice.Services.Abstractions
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using System.Threading.Tasks;

    /// <summary>
    /// Auth service interface.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Asynchronous sign in method.
        /// </summary>
        /// <param name="requestDTO">Request DTO.</param>
        /// <returns>Task&lt;SignInResponseDTO&gt;</returns>
        Task<SignInResponseDTO> SignInAsync(SignInRequestDTO requestDTO);

        /// <summary>
        /// Asynchronous validate token method.
        /// </summary>
        /// <param name="requestDTO">Request DTO.</param>
        /// <returns>Task&lt;ValidateTokenResponseDTO&gt;</returns>
        Task<ValidateTokenResponseDTO> ValidateTokenAsync(ValidateTokenRequestDTO requestDTO);
    }
}
