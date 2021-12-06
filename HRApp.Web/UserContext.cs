using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HRApp.Web
{
    internal class UserContext : IUserContext
    {
        private readonly ClaimsPrincipal _user;

        public UserContext(IHttpContextAccessor httpContextAcessor)
        {
            _user = httpContextAcessor.HttpContext.User;
        }

        public Guid? UserId
        {
            get
            {
                var userStr = _user.Claims.FirstOrDefault(x => x.Type == nameof(UserId))?.Value;

                if (userStr == null || !Guid.TryParse(userStr, out var userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public string? Email
        {
            get
            {
                var email = _user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;

                return email;
            }
        }
    }
}
