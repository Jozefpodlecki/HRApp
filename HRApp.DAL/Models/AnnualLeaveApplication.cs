using System;

namespace HRApp.DAL.Models
{
    public class AnnualLeaveApplication : Application
    {
        public TimeSpan? Time { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
