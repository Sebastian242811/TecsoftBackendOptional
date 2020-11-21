using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

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
        public EPriority Priority { get; set; }
        [Required]
        [MaxLength(5)]
        public string Weight { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int TerminalOriginId { get; set; }
        [Required]
        public int TerminalDestinyId { get; set; }
    }
}
