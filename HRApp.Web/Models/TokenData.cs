using System;

namespace HRApp.Web.Models
{
    public class TokenData
    {
        public string Token { get; set; }

        public DateTime Expire { get; set; }
    }
}
