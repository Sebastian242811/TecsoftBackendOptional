using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Services.Responses;

namespace VirtualExpress.Communication.Domain.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<IEnumerable<Message>> ListByChatByIdAsync(int id);
        Task<MessageResponse> SaveAsync(Message message);
        Task<MessageResponse> UpdateAsync(int id, Message message);
        Task<MessageResponse> DeleteAsync(int id);
        Task<MessageResponse> GetByIdAsync(int id);
    }
}
