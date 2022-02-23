namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SignInRequestDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
