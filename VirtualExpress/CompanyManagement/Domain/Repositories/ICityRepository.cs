using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> ListAsync();
        Task AddAsync(City city);
        Task<City> FindById(int id);
        void Update(City city);
        void Remove(City city);
    }
}
