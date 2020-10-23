using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Services.Communications;

namespace VirtualExpress.Initialization.Domain.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> ListAsync();
        Task<CompanyResponse> FindCompanyById(int companyId);
        Task<CompanyResponse> FindCompanyByCityId(int cityId);
        Task<CompanyResponse> SaveAsync(Company company);
        Task<CompanyResponse> UpdateAsync(int companyId, Company company);
        Task<CompanyResponse> DeleteAsync(int id);
    }
}
