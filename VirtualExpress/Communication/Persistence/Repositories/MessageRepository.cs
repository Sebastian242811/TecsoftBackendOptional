using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Register.Persistence.Repositories;

namespace VirtualExpress.Communication.Persistence.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<Message> FindById(int id) { 
            return await _context.Messages.FindAsync(id); 
        }


        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _context.Messages
                .Include(p=>p.Chat)
                .Include(p => p.Customer)
                .Include(p => p.Company)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> ListByChatByIdAsync(int chatId)
        {
            return await _context.Messages
               .Where(ch => ch.ChatId == chatId)
               .Include(p => p.Chat)
               .ToListAsync();
        }

        public void Remove(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }
    }
}
