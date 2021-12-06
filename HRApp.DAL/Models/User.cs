using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HRApp.DAL.Models
{
    public class User
    {
        private User()
        {

        }

        public User(string email)
        {
            Email = email;
        }

        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(30)]
        public string Email { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Forename { get; set; } = string.Empty;

        public byte[] PasswordHashSalt { get; set; } = Array.Empty<byte>();

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        public DateTime CreatedOn { get; set; }

        public DateTime? SignedInOn { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    }
}
