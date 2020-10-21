using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Services.Responses;

namespace VirtualExpress.ShipDelivery.Domain.Services
{
    interface IPackageDeliveryService
    {
        Task<IEnumerable<PackageDelivery>> ListAsync();
        Task<PackageDeliveryResponse> SaveAsync(PackageDelivery packageDelivery);
        Task<PackageDeliveryResponse> GetById(int id);
        Task<PackageDeliveryResponse> UpdateAsync(int id, PackageDelivery packageDelivery);
        Task<PackageDeliveryResponse> DeleteAsync(int id);
    }
}
