using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;

namespace VirtualExpress.MemberShip.Persistence.Repositories
{
    public class PlanCompanyRepository : BaseRepository, IPlanCompanyRepository
    {
        public PlanCompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PlanCompany planCompany)
        {
            await _context.PlanCompanies.AddAsync(planCompany);
        }

        public async Task<PlanCompany> FindById(int id)
        {
            return await _context.PlanCompanies.FindAsync(id);
        }

        public async Task<PlanCompany> FindByName(string name)
        {
            return await _context.PlanCompanies
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlanCompany>> ListAsync()
        {
            return await _context.PlanCompanies.ToListAsync();
        }

        public void Remove(PlanCompany planCompany)
        {
            _context.PlanCompanies.Remove(planCompany);
        }

        public void Update(PlanCompany planCompany)
        {
            _context.PlanCompanies.Update(planCompany);
        }
    }
}
