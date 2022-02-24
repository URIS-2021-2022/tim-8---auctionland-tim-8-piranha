namespace APIGateway.Middlewares
{
    using APIGateway.Consts;
    using Common.ExceptionHandling.Exceptions;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class TokenValidationMiddleware
    {

        private readonly IOptions<AudienceModel> _appSettings;

        public TokenValidationMiddleware()
        {
        }

        public TokenValidationMiddleware(IOptions<AudienceModel> _appSettings)
        {
            this._appSettings = _appSettings;
            
        }

        public void ValidateToken(String token)
        {
            if (token != null)
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };

                try
                {
                    new JwtSecurityTokenHandler()
                        .ValidateToken(token, validationParameters, out var rawValidatedToken);
                }
                catch (SecurityTokenValidationException stvex)
                {
                    throw new UnauthenticatedException($"Token failed validation: {stvex.Message}");
                }
                catch (ArgumentException argex)
                {
                    throw new UnauthenticatedException($"Token was invalid: { argex.Message }");
                }
            }
            else
            {
                throw new UnauthenticatedException("You must provide a token to access this route!");
            }
        }
    }
}
