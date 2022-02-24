namespace AuthMicroservice.Services.Implementations
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Domain;
    using AuthMicroservice.Initializers.Security;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Services.Abstractions;
    using AuthMicroservice.Utils;
    using AuthMicroservice.Utils.LoggerService;
    using Common.ExceptionHandling.Exceptions;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using AuthMicroservice.Consts;
    using System.Threading.Tasks;

    /// <summary>
    /// Service class for authentication.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly JwtGenerator jwtGenerator;
        private IOptions<AudienceModel> _appSettings;
        private readonly ILoggerService loggerService;

        public static object Domain { get; internal set; }

        /// <summary>
        /// Auth service constructor.
        /// </summary>
        /// <param name="authRepository">Auth repository.</param>
        /// <param name="jwtGenerator">JWT generator.</param>
        /// <param name="_appSettings">App settings.</param>
        /// <param name="loggerService">Logger service.</param>
        public AuthService(IAuthRepository authRepository, 
            JwtGenerator jwtGenerator, 
            IOptions<AudienceModel> _appSettings,
            ILoggerService loggerService)
        {
            this.authRepository = authRepository;
            this.jwtGenerator = jwtGenerator;
            this._appSettings = _appSettings;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Method that performs asynchronous user sign in.
        /// </summary>
        /// <param name="requestDTO">Sign in request DTO.</param>
        /// <returns>Task&lt;SignInResponseDTO&gt;</returns>
        public async Task<SignInResponseDTO> SignInAsync(SignInRequestDTO requestDTO)
        {
            Client client = authRepository.FindOne(client => client.Username == requestDTO.email);

            if (client == null || !BCrypt.Net.BCrypt.Verify(requestDTO.password, client.Password))
            {
                await loggerService.LogMessage(
                    LogLevel.Error,
                    "Sign in failed",
                    GeneralConsts.MICROSERVICE_NAME,
                    "SignInAsync");
                throw new BadCredentialsException("Bad credentials!");
            }

            AuthenticatedUser user = new AuthenticatedUser(client.Username, client.UserType.Name);

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Sign in successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "SignInAsync");

            return new SignInResponseDTO()
            {
                email = requestDTO.email,
                token = jwtGenerator.Generate(user.Uid, user.role)
            };
        }

        /// <summary>
        /// Method that performs asynchronous token validation.
        /// </summary>
        /// <param name="requestDTO">Token to be checked.</param>
        /// <returns>Task&lt;ValidateTokenResponseDTO&gt;</returns>
        public async Task<ValidateTokenResponseDTO> ValidateTokenAsync(ValidateTokenRequestDto requestDTO)
        {
            if (requestDTO.token == null || requestDTO.token == "")
            {
                await loggerService.LogMessage(
                    LogLevel.Error,
                    "Token is empty.",
                    GeneralConsts.MICROSERVICE_NAME,
                    "ValidateTokenAsync");

                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

            try
            {
                tokenHandler.ValidateToken(requestDTO.token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string userUid = jwtToken.Claims.First(x => x.Type == "Sub").Value;

                await loggerService.LogMessage(
                    LogLevel.Error,
                    "Token validation successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "ValidateTokenAsync");

                return new ValidateTokenResponseDTO(userUid);
            }
            catch
            {
                await loggerService.LogMessage(
                    LogLevel.Error,
                    "An error occured while validating token",
                    GeneralConsts.MICROSERVICE_NAME,
                    "ValidateTokenAsync");

                return null;
            }
        }
    }
}
