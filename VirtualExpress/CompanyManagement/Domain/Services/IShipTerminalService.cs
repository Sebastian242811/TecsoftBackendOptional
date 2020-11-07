using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;

namespace VirtualExpress.CompanyManagement.Domain.Services
{
    public interface IShipTerminalService
    {
        Task<IEnumerable<ShipTerminal>> ListAsync();
        Task<IEnumerable<ShipTerminal>> GetShipTerminalsByOriginId(int originId);
        Task<IEnumerable<ShipTerminal>> GetShipTerminalsByCompanyId(int companyId);
        Task<IEnumerable<ShipTerminal>> GetCityOriginAndCityDestinyByCompanyId(int companyId);
        Task<ShipTerminalResponse> GetByOriginIdAndDestinyId(int originId, int destinyId);
        Task<ShipTerminalResponse> SaveAsync(ShipTerminal shipTerminal);
        Task<ShipTerminalResponse> UpdateAsync(int originid, int destinyId, ShipTerminal shipTerminal);
        Task<ShipTerminalResponse> DeleteAsync(int originId, int destinyId);
    }
}
