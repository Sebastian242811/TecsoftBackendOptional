using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;

namespace VirtualExpress.Communication.Domain.Repositories
{
    public interface ICustomerServiceEmployeeRepository
    {
        Task<IEnumerable<CustomerServiceEmployee>> ListAsync();

        Task<IEnumerable<CustomerServiceEmployee>> ListByTerminalByIdAsync(int terminalId);
        Task AddAsync(CustomerServiceEmployee customerServiceEmployee);
        Task<CustomerServiceEmployee> FindById(int id);
        void Update(CustomerServiceEmployee customerServiceEmployee);
        void Remove(CustomerServiceEmployee customerServiceEmployee);
    }
}
