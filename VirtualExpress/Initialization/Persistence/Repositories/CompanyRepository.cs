using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Register.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<Company> FindByEmail(string email)
        {
            return await _context.Companies
                    .Where(p => p.Email == email)
                    .FirstOrDefaultAsync();
        }

        public async Task<Company> FindByUsername(string username)
        {
            return await _context.Companies
                .Where(p => p.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<Company> FindByUsernameAndPassword(string username, string password)
        {
            return await _context.Companies
                .Where(p => p.Username == username)
                .Where(p => p.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<Company> FindCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<IEnumerable<Company>> ListAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public void Remove(Company company)
        {
            _context.Companies.Remove(company);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }
    }
}
