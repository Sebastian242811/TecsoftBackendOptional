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
        Task<IEnumerable<ShipTerminal>> GetAllTerminalDestinyByOriginId(int originId);
        Task<IEnumerable<ShipTerminal>> GetShipTerminalsByCompanyId(int id);
        Task<IEnumerable<ShipTerminal>> GetCityOriginAndCityDestinyByCompanyId(int companyId);
        Task<ShipTerminal> FindByOriginIdAndDestinyId(int originId, int destinyId);
        Task AddAsync(ShipTerminal shipTerminal);
        void Remove(ShipTerminal shipTerminal);
        void Update(ShipTerminal shipTerminal);
    }
}
