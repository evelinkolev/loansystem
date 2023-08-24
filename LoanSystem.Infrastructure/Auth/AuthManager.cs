using LoanSystem.Application.Abstraction.Auth;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoanSystem.Infrastructure.Auth
{
    public partial class AuthManager : IAuthManager
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IClock _clock;
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;

        public AuthManager(JwtOptions jwtOptions, IClock clock)
        {
            var issuerSigningKey = jwtOptions.IssuerSigningKey;
            if(issuerSigningKey is null)
            {
                throw new InvalidOperationException("Issuer signing key not set.");
            }

            _jwtOptions = jwtOptions;
            _clock = clock;
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey!)), SecurityAlgorithms.HmacSha256);
            _issuer = jwtOptions.Issuer!;
        }

        public string GenerateToken(User user)
        {
            var now = _clock.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
            };

            var expires = now.Add(_jwtOptions.Expiry);

            var securityToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _jwtOptions.Audience,
                notBefore: now,
                expires: expires,
                claims: jwtClaims,
                signingCredentials: _signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
