
using System;

namespace HRApp.Web.Configuration
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }

        public string SecretKey { get; set; }

        public TimeSpan TokenExpiry { get; set; }
    }
}
