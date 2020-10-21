using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Domain.Services
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> ListAsync();
        Task<IEnumerable<Package>> ListByCostumerId(int costumerId);
        Task<PackageResponse> GetInfoToDispatcherByPackageId(int packageId);
        Task<PackageResponse> GetById(int id);
        Task<PackageResponse> SaveAsync(Package package);
        Task<PackageResponse> UpdateAsync(int id, Package package);
        Task<PackageResponse> DeleteAsync(int id);
    }
}
