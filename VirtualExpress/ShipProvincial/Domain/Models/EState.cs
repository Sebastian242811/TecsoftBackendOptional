using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public enum EState:byte
    {
        Waiting = 1,
        In_route = 2,
        At_destination_terminal = 3,
        Shipped = 4,
        Delayed = 5
    }
}
