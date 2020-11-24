using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public enum EState:byte
    {
        Espera = 1,
        En_ruta = 2,
        En_destino_terminal= 3,
        Enviado = 4,
        Entregado = 5
    }
}
