using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;

namespace VirtualExpress.CompanyManagement.Persistence.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task<City> FindById(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public void Remove(City city)
        {
            _context.Cities.Remove(city);
        }

        public void Update(City city)
        {
            _context.Cities.Update(city);
        }
    }
}
