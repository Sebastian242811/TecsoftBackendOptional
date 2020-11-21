using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Repositories
{
    public interface IChangeStateRepository
    {
        Task<IEnumerable<ChangeState>> ListAsync();
        Task<IEnumerable<ChangeState>> ListAsyncbypackageid(int id);
        Task AddAsync(ChangeState ChangeState);
        Task<ChangeState> GetByPackageIdAndInitStateAndEndState(int packageId, int initState, int endState);
        Task<ChangeState> FindById(int id);
        void Update(ChangeState ChangeState);
    }
}
