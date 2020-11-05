﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;

namespace VirtualExpress.Communication.Persistence.Repositories
{
    public class ChatRepository : BaseRepository, IChatRepository
    {
        public ChatRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Chat chat)
        {
           await _context.Chats.AddAsync(chat);
        }

        public async Task<Chat> FindById(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task<IEnumerable<Chat>> ListAsync()
        {
            return await _context.Chats.Include(p=>p.Messages).ToListAsync();
        }

        public void Remove(Chat chat)
        {
            _context.Chats.Remove(chat);
        }

        public void Update(Chat chat)
        {
            _context.Chats.Update(chat);
        }
    }
}
