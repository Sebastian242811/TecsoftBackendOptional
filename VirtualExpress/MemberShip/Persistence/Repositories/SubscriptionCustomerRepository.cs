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
    public class SubscriptionCustomerRepository : BaseRepository, ISubscriptionCustomerRepository
    {
        public SubscriptionCustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SubscriptionCustomer subscriptionCustomer)
        {
            await _context.SubscriptionCustomers.AddAsync(subscriptionCustomer);
        }

        public async Task<SubscriptionCustomer> FindById(int id)
        {
            return await _context.SubscriptionCustomers.FindAsync(id);
        }

        public async Task<IEnumerable<SubscriptionCustomer>> ListAsync()
        {
            return await _context.SubscriptionCustomers.ToListAsync();
        }

        public void Remove(SubscriptionCustomer subscriptionCustomer)
        {
            _context.SubscriptionCustomers.Remove(subscriptionCustomer);
        }

        public void Update(SubscriptionCustomer subscriptionCustomer)
        {
            _context.SubscriptionCustomers.Update(subscriptionCustomer);
        }
    }
}
