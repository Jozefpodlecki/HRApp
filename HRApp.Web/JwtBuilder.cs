using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HRApp.Common;
using HRApp.Web.Configuration;
using HRApp.Web.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HRApp.Web
{
    internal class JwtBuilder : IJwtBuilder
    {
        private readonly Encoding _encoding;
        private readonly ISystemClock _systemClock;
        private readonly SigningCredentials _signingCredentials;
        private readonly SecurityTokenHandler _securityTokenHandler;
        private readonly IOptionsMonitor<JwtConfiguration> _options;

        public JwtBuilder(
            Encoding encoding,
            ISystemClock systemClock,
            IOptionsMonitor<JwtConfiguration> options)
        {
            _encoding = encoding;
            _systemClock = systemClock;
            _options = options;
            var securityKey = new SymmetricSecurityKey(_encoding.GetBytes(_options.CurrentValue.SecretKey));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _securityTokenHandler = new JwtSecurityTokenHandler();
        }

        public TokenData Build(TimeSpan expiresOn, params Claim[] claims)
        {
            var options = _options.CurrentValue;
            var allClaims = claims.Append(
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var token = new JwtSecurityToken(options.Issuer,
                options.Issuer,
                allClaims,
                expires: _systemClock.UtcNow + expiresOn,
                signingCredentials: _signingCredentials);

            return new TokenData()
            {
                Token = _securityTokenHandler.WriteToken(token),
                ExpiresOn = token.ValidTo
            };
        }
    }
}