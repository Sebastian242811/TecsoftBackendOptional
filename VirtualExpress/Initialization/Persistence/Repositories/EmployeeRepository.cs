using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Initialization.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<Employee> FindByEmail(string email)
        {
            return await _context.Employees
                .Where(p => p.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> FindById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> FindByUsername(string username)
        {
            return await _context.Employees
                .Where(p => p.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> FindByUsernameAndPassword(string username, string password)
        {
            return await _context.Employees
                .Where(p => p.Username == username)
                .Where(p => p.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public void Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
