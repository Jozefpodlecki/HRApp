using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp.DAL.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }

        public virtual ICollection<OutOfOffice> AnnualLeaves { get; set; }
    }
}
