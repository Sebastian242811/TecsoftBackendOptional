using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Model.Model;

namespace VirtualExpress.MemberShip.Domain.Model
{
    public class PlanCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public TypeOfCurrent TypeOfCurrent { get; set; }
        public int TypeOfCurrentId { get; set; }
        public IList<SubscriptionCompany> SubscriptionCompany { get; set; } = new List<SubscriptionCompany>();
    }
}
