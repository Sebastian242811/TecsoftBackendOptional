using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public enum EState:byte
    {
        En_espera = 1,
        En_camino = 2,
        Retrasado = 3,
        En_terminal_destino = 4
    }
}
