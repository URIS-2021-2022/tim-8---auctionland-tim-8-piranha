namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Request DTO containing info for creating a client.
    /// </summary>
    public class CreateClientRequestDto
    {
        /// <summary>
        /// First name of the client.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the client.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Client username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Client password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Type of the user.
        /// </summary>
        [Required]
        public string UserType { get; set; }
    }
}
