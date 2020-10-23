using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.ShipDelivery.Domain.Repositories
{
    public interface IPackageDeliveryRepository
    {
        Task<IEnumerable<PackageDelivery>> ListAsync();
        Task AddAsync(PackageDelivery packageDelivery);
        Task<PackageDelivery> FindById(int id);
        void Update(PackageDelivery packageDelivery);
        void Remove(PackageDelivery packageDelivery);
    }
}
