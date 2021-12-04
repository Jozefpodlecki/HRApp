using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApp.DAL.Models
{
    public class AnnualLeaveRequest
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Employee CreatedBy { get; set; }

        public TimeSpan? Time { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
