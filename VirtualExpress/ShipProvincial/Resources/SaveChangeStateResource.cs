using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class SaveChangeStateResource
    {
        public EState InitialState { get; set; }
        public EState FinalState { get; set; }
        public int PackageId { get; set; }
    }
}
