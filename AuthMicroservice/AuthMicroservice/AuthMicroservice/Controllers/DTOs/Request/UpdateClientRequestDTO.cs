namespace AuthMicroservice.Controllers.DTOs.Request
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Request DTO containing info for updating user.
    /// </summary>
    public class UpdateClientRequestDTO
    {
        /// <summary>
        /// First name of the client.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the client.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Client username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Type of the user.
        /// </summary>
        public string UserType { get; set; }
    }
}
