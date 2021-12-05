using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApp.DAL.Models
{
    public class OutOfOffice
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }

        public TimeSpan? Time { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
