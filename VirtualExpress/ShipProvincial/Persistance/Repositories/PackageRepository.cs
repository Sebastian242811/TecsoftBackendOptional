using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;

namespace VirtualExpress.ShipProvincial.Persistance.Repositories
{
    public class PackageRepository:BaseRepository,IPackageRepository
    {
        public PackageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
        }

        public async Task<Package> FindById(int id)
        {
            return await _context.Packages.FindAsync(id);
        }

        public async Task<IEnumerable<Package>> ListAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<IEnumerable<Package>> ListByCostumerId(int costumerId)
        {
            return await _context.Packages
                .Where(p => p.CustomerId == costumerId)
                .Include(p => p.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Package>> ListByCustomerShipped(int customer)
        {
            return await _context.Packages
                .Where(p => p.CustomerId == customer)
                .Where(p => (int)p.State != 4)
                .ToListAsync();
        }

        public async Task<IEnumerable<Package>> ListByDispatcherNull()
        {
            return await _context.Packages
                .Where(p => p.DispatcherId == null)
                .Include(p => p.ShipTerminal)
                .Include(p => p.ShipTerminal.TerminalDestiny)
                .Include(p => p.ShipTerminal.TerminalOrigin)
                .ToListAsync();
        }

        public async Task<IEnumerable<Package>> ListByState(int state, int dispatcherId)
        {
            return await _context.Packages
                .Where(p => (int)p.State == state)
                .Where(p => p.DispatcherId == dispatcherId)
                .ToListAsync();
        }

        public void Remove(Package package)
        {
            _context.Packages.Remove(package);
        }

        public void Update(Package package)
        {
            _context.Packages.Update(package);
        }
    }
}
