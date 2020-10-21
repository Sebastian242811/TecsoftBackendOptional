using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipDelivery.Resources
{
    public class SaveDeliveryResource
    {
        [Required]
        public string Arrival { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
