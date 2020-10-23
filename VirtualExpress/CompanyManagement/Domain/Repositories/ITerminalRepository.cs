using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Repositories
{
    public interface ITerminalRepository
    {
        Task<IEnumerable<Terminal>> ListAsync();
        Task<IEnumerable<Terminal>> ListByCityOriginIdAndCityShipIdAsync(int cityOriginId, int cityShipId);
        Task<IEnumerable<Terminal>> ListByCompanyByIdAsync(int id);
        Task<Terminal> FindByCompanyIdAndCityOriginIdAndCityShipId(int companyId, int cityOriginId, int cityShipId);
        Task<Terminal> FindByTerminalIdAndCompanyId(int terminalId, int companyId);
        Task<Terminal> FindById(int id);
        void Remove(Terminal terminal);
        void Update(Terminal terminal);
        Task AssignTerminalCompany(int terminalId, int companyId);
    }
}
