using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Services.Responses;

namespace VirtualExpress.Initialization.Domain.Services
{
    public interface IDealerService
    {
        Task<IEnumerable<Dealer>> ListAsync();
        Task<DealerResponse> FindEmployeeById(int employeeId);
        Task<DealerResponse> SaveAsync(Dealer employee);
        Task<DealerResponse> UpdateAsync(int employeeId, Dealer employee);
        Task<DealerResponse> DeleteAsync(int employeeId);
    }
}
