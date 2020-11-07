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

        public async Task<ShipTerminal> FindByOriginIdAndDestinyId(int originId, int destinyId)
        {
            return await _context.ShipTerminals
                .Where(p => p.TerminalOriginId == originId)
                .Where(p => p.TerminalDestinyId == destinyId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShipTerminal>> GetAllTerminalDestinyByOriginId(int originId)
        {
            return await _context.ShipTerminals
                .Where(p => p.TerminalOriginId == originId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShipTerminal>> GetCityOriginAndCityDestinyByCompanyId(int companyId)
        {
            var ship = await _context.ShipTerminals
                .ToListAsync();

            IList<ShipTerminal> shipTerminals = new List<ShipTerminal>();
            var dict = new Dictionary<int, string>();

            foreach (var ter in ship)
            {
                shipTerminals.Add(ter);
            }
            return shipTerminals;
        }

        public async Task<IEnumerable<ShipTerminal>> GetShipTerminalsByCompanyId(int id)
        {
            return await _context.ShipTerminals
                .Where(p => p.TerminalOrigin.CompanyId == id)
                .Include(p => p.TerminalDestiny)
                .Include(p => p.TerminalDestiny.City)
                .Include(p => p.TerminalOrigin)
                .Include(p => p.TerminalOrigin.City)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShipTerminal>> ListAsync()
        {
            return await _context.ShipTerminals
                .Include(p => p.TerminalDestiny)
                .Include(p => p.TerminalOrigin)
                .ToListAsync();
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
