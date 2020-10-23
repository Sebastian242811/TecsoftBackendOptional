using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task<Customer> FindById(int id);
        Task<Customer> FindByUsername(string username);
        Task<Customer> FindByEmail(string email);
        Task<Customer> FindByUsernameAndPassword(string username, string password);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
