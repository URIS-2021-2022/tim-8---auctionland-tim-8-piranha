namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Token validation request DTO.
    /// </summary>
    public class ValidateTokenRequestDTO
    {
        /// <summary>
        /// JWT token.
        /// </summary>
        [Required]
        public string token { get; set; }
    }
}
