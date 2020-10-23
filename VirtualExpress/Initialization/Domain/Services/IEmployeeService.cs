using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Services.Communications;

namespace VirtualExpress.Initialization.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<EmployeeResponse> FindEmployeeById(int employeeId);
        Task<EmployeeResponse> SaveAsync(Employee employee);
        Task<EmployeeResponse> UpdateAsync(int employeeId, Employee employee);
        Task<EmployeeResponse> DeleteAsync(int employeeId);
    }
}
