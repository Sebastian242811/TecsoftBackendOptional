using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class ChangeStateResource
    {
        public int Id { get; set; }
        public string InitialState { get; set; }
        public string FinalState { get; set; }
        public DateTime EditDate { get; set; }
        public int PackageId { get; set; }
    }
}
