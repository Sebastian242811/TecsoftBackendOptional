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
    public class SubscriptionCompanyRepository : BaseRepository, ISubscriptionCompanyRepository
    {
        public SubscriptionCompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SubscriptionCompany subscriptionCompany)
        {
            await _context.SubscriptionCompanies.AddAsync(subscriptionCompany);
        }

        public async Task<SubscriptionCompany> FindById(int id)
        {
            return await _context.SubscriptionCompanies.FindAsync(id);
        }

        public async Task<IEnumerable<SubscriptionCompany>> ListAsync()
        {
            return await _context.SubscriptionCompanies.ToListAsync();
        }

        public void Remove(SubscriptionCompany subscriptionCompany)
        {
            _context.SubscriptionCompanies.Remove(subscriptionCompany);
        }

        public void Update(SubscriptionCompany subscriptionCompany)
        {
            _context.SubscriptionCompanies.Update(subscriptionCompany);
        }
    }
}
