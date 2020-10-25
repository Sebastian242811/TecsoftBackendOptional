using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.MemberShip.Resource
{
    public class SubscriptionCompanyResource
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int CompanyId { get; set; }
        public int PlanId { get; set; }
        public int Discount { get; set; }
        public double TotalPrice { get; set; }
    }
}
