using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp.DAL.Models
{
    public class PersonStats
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Year { get; set; }

        public TimeSpan AnnualLeaveTotalTime { get; set; }

        public double AnnualLeaveTotalDays { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual Person Person { get; set; }
    }
}
