using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp.DAL.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }

        public HierarchyId HierarchyId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Guid AccountId { get; set; }

        public virtual ICollection<OutOfOffice> OutOfOffice { get; set; } = new Collection<OutOfOffice>();

        public virtual ICollection<Application> Applications { get; set; } = new Collection<Application>();
    }
}
