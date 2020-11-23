using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;

namespace VirtualExpress.Communication.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<IEnumerable<Message>> ListByChatByIdAsync(int chatId);
        Task AddAsync(Message message);
        Task<Message> FindById(int id);
        void Update(Message message);
        void Remove(Message message);

    }
}
