using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Repositories
{
    public interface IPlanCompanyRepository
    {
        Task<IEnumerable<PlanCompany>> ListAsync();
        Task<PlanCompany> FindById(int id);
        Task<PlanCompany> FindByName(string name);
        Task AddAsync(PlanCompany planCompany);
        void Update(PlanCompany planCompany);
        void Remove(PlanCompany planCompany);
    }
}
