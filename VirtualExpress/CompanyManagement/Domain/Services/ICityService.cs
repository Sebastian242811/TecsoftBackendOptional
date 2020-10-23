using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;

namespace VirtualExpress.CompanyManagement.Domain.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> ListAsync();
        Task<CityResponse> FindCityById(int id);
        Task<CityResponse> SaveAsync(City city);
        Task<CityResponse> UpdateAsync(int id, City city);
        Task<CityResponse> DeleteAsync(int id);
    }
}
