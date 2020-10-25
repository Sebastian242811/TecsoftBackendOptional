using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Model.Model
{
    public class TypeOfCurrent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PlanCompany> PlanCompanies { get; set; } = new List<PlanCompany>();
        public IList<PlanCustomer> PlanCustomers { get; set; } = new List<PlanCustomer>();
    }
}
