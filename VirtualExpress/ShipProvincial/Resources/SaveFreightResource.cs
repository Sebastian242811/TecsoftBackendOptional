using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class SaveFreightResource
    {
        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Observations { get; set; }
    }
}
