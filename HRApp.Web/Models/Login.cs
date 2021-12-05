using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Web.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
