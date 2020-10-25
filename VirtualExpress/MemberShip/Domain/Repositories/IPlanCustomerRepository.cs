using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Repositories
{
    public interface IPlanCustomerRepository
    {
        Task<IEnumerable<PlanCustomer>> ListAsync();
        Task<PlanCustomer> FindById(int id);
        Task<PlanCustomer> FindByName(string name);
        Task AddAsync(PlanCustomer planCustomer);
        void Update(PlanCustomer planCustomer);
        void Remove(PlanCustomer planCustomer);
    }
}
