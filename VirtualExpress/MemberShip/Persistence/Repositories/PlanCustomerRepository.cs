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
    public class PlanCustomerRepository : BaseRepository, IPlanCustomerRepository
    {
        public PlanCustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PlanCustomer planCustomer)
        {
            await _context.PlanCustomers.AddAsync(planCustomer);
        }

        public async Task<PlanCustomer> FindById(int id)
        {
            return await _context.PlanCustomers.FindAsync(id);
        }

        public async Task<PlanCustomer> FindByName(string name)
        {
            return await _context.PlanCustomers
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlanCustomer>> ListAsync()
        {
            return await _context.PlanCustomers.Include(p => p.TypeOfCurrent).ToListAsync();
        }

        public void Remove(PlanCustomer planCustomer)
        {
            _context.PlanCustomers.Remove(planCustomer);
        }

        public void Update(PlanCustomer planCustomer)
        {
            _context.PlanCustomers.Update(planCustomer);
        }
    }
}
