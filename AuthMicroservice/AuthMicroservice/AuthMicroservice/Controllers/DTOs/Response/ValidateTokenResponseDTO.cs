namespace AuthMicroservice.Controllers.DTOs.Response
{
    /// <summary>
    /// Token validation response DTO.
    /// </summary>
    public class ValidateTokenResponseDto
    {
        /// <summary>
        /// Nullable uid of the user.
        /// </summary>
        #nullable enable
        private string? userUid;
        #nullable disable

        /// <summary>
        /// Getter and setter for userUid
        /// </summary>
        public string UserUid
        {
            get { return userUid; }
            set { this.userUid = value }
        }

        /// <summary>
        /// Token validation response DTO constructor.
        /// </summary>
        /// <param name="userUid"></param>
        public ValidateTokenResponseDto(string userUid)
        {
            this.userUid = userUid;
        }
    }
}
