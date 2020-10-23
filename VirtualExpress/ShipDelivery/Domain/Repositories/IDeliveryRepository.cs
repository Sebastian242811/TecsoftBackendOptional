using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.ShipDelivery.Domain.Repositories
{
    interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> ListAsync();
        Task AddAsync(Delivery delivery);
        Task<Delivery> FindById(int id);
        void Update(Delivery delivery);
        void Remove(Delivery delivery);
    }
}
