namespace AuthMicroservice.Controllers.DTOs.Response
{
    /// <summary>
    /// Client entity response DTO.
    /// </summary>
    public class ClientResponseDTO
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
        public UserTypeResponseDTO UserType { get; set; }
    }
}
