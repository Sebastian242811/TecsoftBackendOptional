using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Services.Responses;

namespace VirtualExpress.Communication.Domain.Services
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> ListAsync();
        Task<ChatResponse> SaveAsync(Chat chat);
        Task<ChatResponse> UpdateAsync(int id, Chat chat);
        Task<ChatResponse> DeleteAsync(int id);
        Task<ChatResponse> GetByIdAsync(int id);
    }
}
