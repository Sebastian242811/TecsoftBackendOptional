using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Repositories
{
    public interface ISubscriptionCompanyRepository
    {
        Task<IEnumerable<SubscriptionCompany>> ListAsync();
        Task<SubscriptionCompany> FindById(int id);
        Task AddAsync(SubscriptionCompany subscriptionCompany);
        void Update(SubscriptionCompany subscriptionCompany);
        void Remove(SubscriptionCompany subscriptionCompany);
    }
}
