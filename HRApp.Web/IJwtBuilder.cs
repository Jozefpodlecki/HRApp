using HRApp.Web.Models;
using System;
using System.Security.Claims;

namespace HRApp.Web
{
    public interface IJwtBuilder
    {
        TokenData Build(TimeSpan expiresOn, params Claim[] claims);
    }
}
