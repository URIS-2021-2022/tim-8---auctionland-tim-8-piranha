namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidateTokenRequestDTO
    {
        [Required]
        private string token { get; set; }
    }
}
