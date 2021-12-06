using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp.DAL.Models
{
    public class StatutorySickPayApplication : Application
    {
        [StringLength(20)]
        public string? FormBlobUrl { get; set; }

        [StringLength(20)]
        public string Surname { get; set; } = string.Empty;

        [StringLength(20)]
        public string FirstNames { get; set; } = string.Empty;

        [StringLength(4)]
        public string Title { get; set; } = string.Empty;

        [StringLength(20)]
        public string NINO { get; set; } = string.Empty;

        [StringLength(30)]
        public string PayrollNumber { get; set; } = string.Empty;

        [StringLength(30)]
        public string Details { get; set; } = string.Empty;

        public DateTime? SicknessBeganOn { get; set; }

        public DateTime? SicknessEndedOn { get; set; }

        public DateTime? LastWorkedOn { get; set; }

        public bool CausedByAccident { get; set; }

        public string SignId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
