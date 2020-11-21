using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> ListAsync();
        Task<IEnumerable<Package>> ListByDispatcherNull();
        Task<IEnumerable<Package>> ListByState(int state, int dispatcherId);
        Task<IEnumerable<Package>> ListByCustomerShipped(int cusomter);
        Task<IEnumerable<Package>> ListByCostumerId(int costumerId);        
        Task AddAsync(Package Package);
        Task<Package> FindById(int id);
        void Update(Package Package);
        void Remove(Package Package);
    }
}
