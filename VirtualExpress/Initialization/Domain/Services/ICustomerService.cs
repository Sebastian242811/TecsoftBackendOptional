using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Services.Communications;

namespace VirtualExpress.Initialization.Domain.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task<CustomerResponse> FindCustomerById(int customerId);
        Task<CustomerResponse> SaveAsync(Customer customer);
        Task<CustomerResponse> UpdateAsync(int customerId, Customer customer);
        Task<CustomerResponse> DeleteAsync(int customerId);
    }
}
