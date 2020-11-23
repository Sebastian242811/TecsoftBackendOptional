using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;

namespace VirtualExpress.Communication.Domain.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> ListAsync();
        Task<IEnumerable<Chat>> ListAsyncByCustomerId(int customerId);
        Task<IEnumerable<Chat>> ListAsyncByCompanyId(int companyId);
        Task AddAsync(Chat chat);
        Task<Chat> FindById(int id);
        void Update(Chat chat);
        void Remove(Chat chat);
    }
}
