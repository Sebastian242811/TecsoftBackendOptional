﻿using System;
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
        Task<IEnumerable<Package>> ListByDispatcherNull();
        Task<IEnumerable<Package>> ListByCostumerId(int costumerId);
        Task<IEnumerable<Package>> ListByState(int state, int dispatcherId);
        Task<IEnumerable<Package>> ListByCustomerAndStateIsNotShipped(int customerId);
        Task<PackageResponse> GetInfoToDispatcherByPackageId(int packageId);
        Task<PackageResponse> GetById(int id);
        Task<PackageResponse> SaveAsync(Package package);
        Task<PackageResponse> UpdateAsync(int id, Package package);
        Task<PackageResponse> UpdateStateAsync(int id, int value);
        Task<PackageResponse> DeleteAsync(int id);
        Task<PackageResponse> AddDispatcher(int packageId, int dispatcherId);
    }
}
