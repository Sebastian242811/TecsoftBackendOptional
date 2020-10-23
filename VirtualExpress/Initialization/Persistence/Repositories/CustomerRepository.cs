using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Register.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> FindByEmail(string email)
        {
            return await _context.Customers
                .Where(p => p.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> FindById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> FindByUsername(string username)
        {
            return await _context.Customers
                .Where(p => p.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> FindByUsernameAndPassword(string username, string password)
        {
            return await _context.Customers
                .Where(p => p.Username == username)
                .Where(p => p.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
