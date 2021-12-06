using System;

namespace HRApp.Web.Models
{
    public class NewAnnualLeaveApplication
    {
        public TimeSpan? Time { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
