using System;

namespace HRApp.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
