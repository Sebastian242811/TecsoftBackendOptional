using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class SavePackageResource
    {
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Observations { get; set; }
        [Required]
        [MaxLength(20)]
        public string Priority { get; set; }
        [Required]
        [MaxLength(20)]
        public string State { get; set; }
        [Required]
        [MaxLength(5)]
        public string Weight { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int FerightId { get; set; }
        [Required]
        public int DispatcherId { get; set; }
    }
}
