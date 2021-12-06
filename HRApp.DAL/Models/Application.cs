using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApp.DAL.Models
{
    public class Application
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool isAccepted { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public virtual Person CreatedBy { get; set; }

        public string CreatedByName { get; set; } = string.Empty;
    }
}
