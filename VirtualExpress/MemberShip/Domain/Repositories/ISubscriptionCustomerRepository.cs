using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Repositories
{
    public interface ISubscriptionCustomerRepository
    {
        Task<IEnumerable<SubscriptionCustomer>> ListAsync();
        Task<SubscriptionCustomer> FindById(int id);
        Task AddAsync(SubscriptionCustomer subscriptionCustomer);
        void Update(SubscriptionCustomer subscriptionCustomer);
        void Remove(SubscriptionCustomer subscriptionCustomer);
    }
}
