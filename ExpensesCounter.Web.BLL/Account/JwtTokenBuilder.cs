using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesCounter.Web.BLL.Account
{
    internal class JwtTokenBuilder
    {
        private readonly string _securityKey;

        public JwtTokenBuilder(string securityKey)
        {
            if (string.IsNullOrWhiteSpace(securityKey))
                throw new ArgumentNullException(nameof(securityKey), "Security key must have a value");

            _securityKey = securityKey;
        }

        public string   Issuer   { private get; set; }
        public string   Audience { private get; set; }
        public TimeSpan LifeTime { private get; set; }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var credentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_securityKey)),
                                       SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                                           Issuer,
                                           Audience,
                                           notBefore: now,
                                           claims: claims,
                                           expires: now.Add(LifeTime),
                                           signingCredentials: credentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}