namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SignInRequestDTO
    {
        [Required]
        [EmailAddress]
        private string email { get; set; }

        [Required]
        private string password { get; set; }
    }
}
