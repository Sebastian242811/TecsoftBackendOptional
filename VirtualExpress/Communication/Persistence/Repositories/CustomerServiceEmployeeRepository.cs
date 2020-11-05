using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;

namespace VirtualExpress.Communication.Persistence.Repositories
{
    public class CustomerServiceEmployeeRepository : BaseRepository, ICustomerServiceEmployeeRepository
    {

        public CustomerServiceEmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(CustomerServiceEmployee customerServiceEmployee)
        {
            await _context.CustomerServiceEmployees.AddAsync(customerServiceEmployee);
        }

        public async Task<CustomerServiceEmployee> FindById(int id)
        {
            return await _context.CustomerServiceEmployees.FindAsync(id);
        }

        public async Task<IEnumerable<CustomerServiceEmployee>> ListAsync()
        {
            return await _context.CustomerServiceEmployees.Include(p=>p.Terminal).ToListAsync();
        }

        public async Task<IEnumerable<CustomerServiceEmployee>> ListByTerminalByIdAsync(int terminalId)
        {
            return await _context.CustomerServiceEmployees
               .Where(te => te.TerminalId == terminalId)
               .Include(p => p.Terminal)
               .ToListAsync();
        }


        public void Remove(CustomerServiceEmployee customerServiceEmployee)
        {
            _context.CustomerServiceEmployees.Remove(customerServiceEmployee);
        }

        public void Update(CustomerServiceEmployee customerServiceEmployee)
        {
            _context.CustomerServiceEmployees.Update(customerServiceEmployee);
        }
    }
}
