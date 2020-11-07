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
        Task AddAsync(ChangeState ChangeState);
        Task<ChangeState> FindById(int id);
        void Update(ChangeState ChangeState);
    }
}
