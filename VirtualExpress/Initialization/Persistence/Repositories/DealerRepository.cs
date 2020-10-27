using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Initialization.Persistence.Repositories
{
    public class DealerRepository : BaseRepository, IDealerRepository
    {
        public DealerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Dealer employee)
        {
            await _context.Dealers.AddAsync(employee);
        }

        public async Task<Dealer> FindByEmail(string email)
        {
            return await _context.Dealers
                .Where(p => p.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Dealer> FindById(int id)
        {
            return await _context.Dealers.FindAsync(id);
        }

        public async Task<Dealer> FindByUsername(string username)
        {
            return await _context.Dealers
                .Where(p => p.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<Dealer> FindByUsernameAndPassword(string username, string password)
        {
            return await _context.Dealers
                .Where(p => p.Username == username)
                .Where(p => p.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Dealer>> ListAsync()
        {
            return await _context.Dealers.ToListAsync();
        }

        public void Remove(Dealer employee)
        {
            _context.Dealers.Remove(employee);
        }

        public void Update(Dealer employee)
        {
            _context.Dealers.Update(employee);
        }
    }
}
