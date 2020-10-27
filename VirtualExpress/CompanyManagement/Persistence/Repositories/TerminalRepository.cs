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
    public class TerminalRepository: BaseRepository, ITerminalRepository
    {
        public TerminalRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Terminal terminal)
        {
            await _context.Terminals.AddAsync(terminal);
        }

        public async Task<IEnumerable<Terminal>> ListAsync()
        {
            return await _context.Terminals.ToListAsync();
        }

        public async Task<IEnumerable<Terminal>> ListByCityOriginIdAndCityShipIdAsync(int cityOriginId, int cityShipId)
        {
            return await _context.Terminals
                .Where(co => co.CityId == cityOriginId)
                .Where(cs => cs.CityId == cityShipId)
                .Include(p => p.Company)
                .Include(p => p.City)
                .ToListAsync();
        }

        public async Task<IEnumerable<Terminal>> ListByCompanyByIdAsync(int id)
        {
            return await _context.Terminals
               .Where(co => co.CompanyId == id)
               .Include(p => p.Company)
               .Include(p => p.City)
               .ToListAsync();
        }

        public async Task<Terminal> FindByCompanyIdAndCityOriginIdAndCityShipId(int companyId, int cityOriginId, int cityShipId)
        {
            return await _context.Terminals.FindAsync(companyId, cityOriginId, cityShipId);
        }

        public void Remove(Terminal terminal)
        {
            _context.Terminals.Remove(terminal);
        }

        public async Task<Terminal> FindById(int id)
        {
            return await _context.Terminals.FindAsync(id);
        }

        public async Task AssignTerminalCompany(int terminalId, int companyId)
        {
            Terminal terminal = await _context.Terminals.FindAsync(terminalId, companyId);
            if (terminal == null)
                await AddAsync(terminal);
        }

        public void Update(Terminal terminal)
        {
            _context.Terminals.Update(terminal);
        }

        public async Task<Terminal> FindByTerminalIdAndCompanyId(int terminalId, int companyId)
        {
            return await _context.Terminals
                .Where(p => p.CompanyId == companyId)
                .Where(p => p.Id == terminalId)
                .Include(pt => pt.Company)
                .FirstOrDefaultAsync();
        }
    }
}
