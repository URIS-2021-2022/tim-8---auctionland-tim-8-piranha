namespace AuthMicroservice.Controllers.DTOs.Response
{
    public class ValidateTokenResponseDTO
    {
        #nullable enable
        public string? userUid;
        #nullable disable

        public ValidateTokenResponseDTO(string userUid)
        {
            this.userUid = userUid;
        }
    }
}
