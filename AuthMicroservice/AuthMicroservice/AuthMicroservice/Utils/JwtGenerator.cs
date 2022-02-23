namespace AuthMicroservice.Utils
{
    using AuthMicroservice.Initializers.Security;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// JWT token generator class.
    /// </summary>
    public class JwtGenerator
    {
        private IOptions<AudienceModel> _options;

        /// <summary>
        /// Constructor for the JWT token generator class.
        /// </summary>
        /// <param name="options"></param>
        public JwtGenerator(IOptions<AudienceModel> options)
        {
            _options = options;
        }

        /// <summary>
        /// Method used for generating JWT token.
        /// </summary>
        /// <param name="uid">User uid.</param>
        /// <param name="role">User role.</param>
        /// <returns>string</returns>
        public string Generate(string uid, string role)
        {
            DateTime now = DateTime.UtcNow;

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, uid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim("roles", role)
            };

            SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret));

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _options.Value.Iss,
                audience: _options.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(48)),
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
