using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;

namespace VirtualExpress.CompanyManagement.Domain.Services
{
    public interface ITerminalService
    {
        Task<IEnumerable<Terminal>> ListAsync();
        Task<IEnumerable<Terminal>> ListByCityOriginIdAndCityShipIdAsync(int cityOriginId, int cityShipId);
        Task<IEnumerable<Terminal>> ListByCompanyByIdAsync(int id);
        Task<TerminalResponse> SaveAssync(Terminal terminal);
        Task<TerminalResponse> UpdateAssync(int id, Terminal terminal);
        Task<TerminalResponse> DeleteAsync(int id);
        Task<TerminalResponse> AssignTerminalCompanyAsync(int terminaId, int companyID);
        Task<TerminalResponse> GetByIdAsync(int id);
    }
}
