using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Services.Responses;

namespace VirtualExpress.Communication.Domain.Services
{
    public interface ICustomerServiceEmployeeService
    {
        Task<IEnumerable<CustomerServiceEmployee>> ListAsync();
        Task<IEnumerable<CustomerServiceEmployee>> ListByTerminalByIdAsync(int id);
        Task<CustomerServiceEmployeeResponse> SaveAsync(CustomerServiceEmployee customerServiceEmployee);
        Task<CustomerServiceEmployeeResponse> UpdateAsync(int id, CustomerServiceEmployee customerServiceEmployee);
        Task<CustomerServiceEmployeeResponse> DeleteAsync(int id);
        Task<CustomerServiceEmployeeResponse> GetByIdAsync(int id);
    }
}
