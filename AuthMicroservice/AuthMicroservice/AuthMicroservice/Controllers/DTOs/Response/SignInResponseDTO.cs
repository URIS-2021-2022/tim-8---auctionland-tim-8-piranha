namespace AuthMicroservice.Controllers.DTOs.Response
{
    /// <summary>
    /// Sign in response DTO.
    /// </summary>
    public class SignInResponseDTO
    {
        /// <summary>
        /// Email address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// JWT token.
        /// </summary>
        public string token { get; set; }
    }
}
