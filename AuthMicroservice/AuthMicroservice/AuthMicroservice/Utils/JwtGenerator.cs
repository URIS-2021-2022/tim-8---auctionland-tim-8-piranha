namespace AuthMicroservice.Utils
{
    using AuthMicroservice.Initializers.Security;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtGenerator
    {
        private IOptions<AudienceModel> _options;

        public JwtGenerator(IOptions<AudienceModel> options)
        {
            _options = options;
        }

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
