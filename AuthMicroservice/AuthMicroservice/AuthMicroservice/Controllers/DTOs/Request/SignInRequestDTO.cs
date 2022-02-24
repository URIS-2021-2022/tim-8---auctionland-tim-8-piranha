namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Sign in request DTO.
    /// </summary>
    public class SignInRequestDTO
    {
        /// <summary>
        /// Email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string password { get; set; }
    }
}
