using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> ListAsync();
        Task<Company> FindCompanyById(int id);
        Task<Company> FindByUsername(string username);
        Task<Company> FindByEmail(string email);
        Task<Company> FindByUsernameAndPassword(string username, string password);
        Task AddAsync(Company company);
        void Update(Company company);
        void Remove(Company company);
    }
}
