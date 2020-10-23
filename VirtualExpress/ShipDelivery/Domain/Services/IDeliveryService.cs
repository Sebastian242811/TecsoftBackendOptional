using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Services.Responses;

namespace VirtualExpress.ShipDelivery.Domain.Services
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> ListAsync();
        Task<DeliveryResponse> SaveAsync(Delivery delivery);
        Task<DeliveryResponse> GetById(int id);
        Task<DeliveryResponse> UpdateAsync(int id, Delivery delivery);
        Task<DeliveryResponse> DeleteAsync(int id);
    }
}
