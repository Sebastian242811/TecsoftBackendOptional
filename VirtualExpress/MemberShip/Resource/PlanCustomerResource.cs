using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Model.Model;

namespace VirtualExpress.MemberShip.Resource
{
    public class PlanCustomerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int TypeOfCurrentId { get; set; }
        public TypeOfCurrentResource TypeOfCurrent { get; set; }
    }
}
