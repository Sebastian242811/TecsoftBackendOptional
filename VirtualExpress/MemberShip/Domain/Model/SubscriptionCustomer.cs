using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.MemberShip.Model.Model;

namespace VirtualExpress.MemberShip.Domain.Model
{
    public class SubscriptionCustomer
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public PlanCustomer PlanCustomer { get; set; }
        public int PlanId { get; set; }
        public int Discount { get; set; }
        public double TotalPrice { get; set; }
    }
}
