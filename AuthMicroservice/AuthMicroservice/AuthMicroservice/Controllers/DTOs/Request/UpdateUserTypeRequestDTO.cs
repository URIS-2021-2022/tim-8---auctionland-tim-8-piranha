namespace AuthMicroservice.Controllers.DTOs.Request
{
    /// <summary>
    /// Update user type request DTO.
    /// </summary>
    public class UpdateUserTypeRequestDto
    {
        /// <summary>
        /// New name for the user type.
        /// </summary>
        public string Name { get; set; }
    }
}
