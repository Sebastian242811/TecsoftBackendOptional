using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Domain.Services
{
    public interface IPlanCustomerService
    {
        Task<IEnumerable<PlanCustomer>> ListAsync();
        Task<PlanCustomerResponse> FindById(int id);
        Task<PlanCustomerResponse> SaveAsync(PlanCustomer planCustomer);
        Task<PlanCustomerResponse> UpdateAsync(int id, PlanCustomer planCustomer);
        Task<PlanCustomerResponse> RemoveAsync(int id);
    }
}
