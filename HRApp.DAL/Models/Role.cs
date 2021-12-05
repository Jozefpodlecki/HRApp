using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HRApp.DAL.Models
{
    public class Role
    {
        private Role()
        {

        }

        public Role(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    }
}
