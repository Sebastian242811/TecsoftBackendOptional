using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> FindById(int id);
        Task<Employee> FindByUsername(string username);
        Task<Employee> FindByEmail(string email);
        Task<Employee> FindByUsernameAndPassword(string username, string password);
        Task AddAsync(Employee employee);
        void Remove(Employee employee);
        void Update(Employee employee);
    }
}
