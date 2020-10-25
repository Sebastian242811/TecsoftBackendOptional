using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Domain.Services
{
    public interface ISubscriptionCompanyService
    {
        Task<IEnumerable<SubscriptionCompany>> ListAsync();
        Task<SubscriptionCompanyResponse> FindById(int id);
        Task<SubscriptionCompanyResponse> SaveAsync(SubscriptionCompany subscriptionCompany);
        Task<SubscriptionCompanyResponse> UpdateAsync(int id, SubscriptionCompany subscriptionCompany);
        Task<SubscriptionCompanyResponse> RemoveAsync(int id);
    }
}
