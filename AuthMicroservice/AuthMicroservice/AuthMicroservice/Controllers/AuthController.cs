namespace AuthMicroservice.Controllers
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Services.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route(RouteConsts.ROUTE_AUTH_BASE)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost(RouteConsts.ROUTE_AUTH_SIGN_IN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<SignInResponseDTO> HandleSignIn([FromBody] SignInRequestDTO requestDTO)
        {
            return Ok(authService.SignInAsync(requestDTO));
        }

        [HttpPost(RouteConsts.ROUTE_AUTH_VALIDATE_TOKEN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ValidateTokenResponseDTO> HandleValidateToken([FromBody] ValidateTokenRequestDTO requestDTO)
        {
            return Ok(authService.ValidateTokenAsync(requestDTO));
        }
    }
}
