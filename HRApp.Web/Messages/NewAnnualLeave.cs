using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Web.Messages
{
    public class NewAnnualLeave
    {
        public const string ExchangeKey = "applications";
        public const string RouteKey = "new-annual-leave";
        public int ApplicationId { get; set; }
        public Guid PersonId { get; set; }
    }
}
