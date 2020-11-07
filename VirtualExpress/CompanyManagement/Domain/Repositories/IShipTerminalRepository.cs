using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Repositories
{
    public interface IShipTerminalRepository
    {
        Task<IEnumerable<ShipTerminal>> ListAsync();
        Task AddAsync(ShipTerminal shipTerminal);
        Task<ShipTerminal> FindById(int id);
        void Update(ShipTerminal shipTerminal);
        void Remove(ShipTerminal shipTerminal);
    }
}
