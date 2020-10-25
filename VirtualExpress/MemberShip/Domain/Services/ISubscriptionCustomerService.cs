using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Domain.Services
{
    public interface ISubscriptionCustomerService
    {
        Task<IEnumerable<SubscriptionCustomer>> ListAsync();
        Task<SubscriptionCustomerResponse> FindById(int id);
        Task<SubscriptionCustomerResponse> SaveAsync(SubscriptionCustomer subscriptionCustomer);
        Task<SubscriptionCustomerResponse> UpdateAsync(int id, SubscriptionCustomer subscriptionCustomer);
        Task<SubscriptionCustomerResponse> RemoveAsync(int id);
    }
}
