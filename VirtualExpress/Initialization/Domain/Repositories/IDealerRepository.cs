using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Repositories
{
    public interface IDealerRepository
    {
        Task<IEnumerable<Dealer>> ListAsync();
        Task<Dealer> FindById(int id);
        Task<Dealer> FindByUsername(string username);
        Task<Dealer> FindByEmail(string email);
        Task<Dealer> FindByUsernameAndPassword(string username, string password);
        Task AddAsync(Dealer employee);
        void Remove(Dealer employee);
        void Update(Dealer employee);
    }
}
