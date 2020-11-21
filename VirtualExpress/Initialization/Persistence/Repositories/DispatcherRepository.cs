using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Initialization.Persistance.Repositories
{
    public class DispatcherRepository:BaseRepository, IDispatcherRepository
    {
        public DispatcherRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Dispatcher dispatcher)
        {
            await _context.Dispatchers.AddAsync(dispatcher);
        }

        public async Task<Dispatcher> FindById(int id)
        {
            return await _context.Dispatchers.FindAsync(id);
        }

        public async Task<IEnumerable<Dispatcher>> ListAsync()
        {
            return await _context.Dispatchers.ToListAsync();
        }

        public async Task<Dispatcher> GetDispatcherByUsernameAndPassword(string Username, string Password)
        {
            return await _context.Dispatchers
                .Where(p => p.Username == Username)
                .Where(p => p.Password == Password)
                .FirstOrDefaultAsync();
        }

        public void Remove(Dispatcher dispatcher)
        {
            _context.Dispatchers.Remove(dispatcher);
        }

        public void Update(Dispatcher dispatcher)
        {
            _context.Dispatchers.Update(dispatcher);
        }
    }
}
