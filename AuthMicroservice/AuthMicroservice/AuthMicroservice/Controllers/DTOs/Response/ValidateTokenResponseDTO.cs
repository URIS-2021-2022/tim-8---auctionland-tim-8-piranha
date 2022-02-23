namespace AuthMicroservice.Controllers.DTOs.Response
{
    /// <summary>
    /// Token validation response DTO.
    /// </summary>
    public class ValidateTokenResponseDTO
    {
        /// <summary>
        /// Nullable uid of the user.
        /// </summary>
        #nullable enable
        public string? userUid;
        #nullable disable

        /// <summary>
        /// Token validation response DTO constructor.
        /// </summary>
        /// <param name="userUid"></param>
        public ValidateTokenResponseDTO(string userUid)
        {
            this.userUid = userUid;
        }
    }
}
