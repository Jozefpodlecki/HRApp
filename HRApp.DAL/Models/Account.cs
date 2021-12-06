using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp.DAL.Models
{
    public class Account
    {
        private Account() {}

        public Account(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
