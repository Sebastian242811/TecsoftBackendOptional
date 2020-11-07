using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;

namespace VirtualExpress.CompanyManagement.Persistence.Repositories
{
    public class ShipTerminalRepository : BaseRepository, IShipTerminalRepository
    {
        public ShipTerminalRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ShipTerminal shipTerminal)
        {
            await _context.ShipTerminals.AddAsync(shipTerminal);
        }

        public async Task<ShipTerminal> FindById(int id)
        {
            return await _context.ShipTerminals.FindAsync(id);
        }

        public async Task<IEnumerable<ShipTerminal>> ListAsync()
        {
            return await _context.ShipTerminals.ToListAsync();
        }

        public void Remove(ShipTerminal shipTerminal)
        {
            _context.ShipTerminals.Remove(shipTerminal);
        }

        public void Update(ShipTerminal shipTerminal)
        {
            _context.ShipTerminals.Update(shipTerminal);
        }
    }
}
