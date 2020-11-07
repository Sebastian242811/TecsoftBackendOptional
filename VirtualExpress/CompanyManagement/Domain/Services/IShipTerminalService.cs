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
        Task<ShipTerminalResponse> FindCityById(int id);
        Task<ShipTerminalResponse> SaveAsync(ShipTerminal shipTerminal);
        Task<ShipTerminalResponse> UpdateAsync(int id, ShipTerminal shipTerminal);
        Task<ShipTerminalResponse> DeleteAsync(int id);
    }
}
