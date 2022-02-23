namespace AuthMicroservice.Controllers
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Services.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the auth resource.
    /// </summary>
    [ApiController]
    [Route(RouteConsts.ROUTE_AUTH_BASE)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        /// <summary>
        /// Constructor for the auth controller.
        /// </summary>
        /// <param name="authService">Auth service.</param>
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Endpoint for user sign in.
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost(RouteConsts.ROUTE_AUTH_SIGN_IN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<SignInResponseDTO> HandleSignIn([FromBody] SignInRequestDTO requestDTO)
        {
            return Ok(authService.SignInAsync(requestDTO));
        }

        /// <summary>
        /// Endpoint for validating a JWT token.
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost(RouteConsts.ROUTE_AUTH_VALIDATE_TOKEN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ValidateTokenResponseDTO> HandleValidateToken([FromBody] ValidateTokenRequestDTO requestDTO)
        {
            return Ok(authService.ValidateTokenAsync(requestDTO));
        }
    }
}
