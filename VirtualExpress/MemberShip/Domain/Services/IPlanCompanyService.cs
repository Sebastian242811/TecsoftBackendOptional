using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Domain.Services
{
    public interface IPlanCompanyService
    {
        Task<IEnumerable<PlanCompany>> ListAsync();
        Task<PlanCompanyResponse> FindById(int id);
        Task<PlanCompanyResponse> SaveAsync(PlanCompany planCompany);
        Task<PlanCompanyResponse> UpdateAsync(int id, PlanCompany planCompany);
        Task<PlanCompanyResponse> RemoveAsync(int id);
    }
}
