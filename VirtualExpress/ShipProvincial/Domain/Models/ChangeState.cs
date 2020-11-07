using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public class ChangeState
    {
        public int Id { get; set; }
        public EState InitialState { get; set; }
        public EState FinalState { get; set; }
        public DateTime EditDate { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }

        public ChangeState()
        {
            EditDate = DateTime.Now;
        }
    }
}
