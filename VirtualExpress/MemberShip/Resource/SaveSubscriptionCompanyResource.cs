using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.MemberShip.Resource
{
    public class SaveSubscriptionCompanyResource
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int PlanId { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public double TotalPrice { get; set; }
    }
}
