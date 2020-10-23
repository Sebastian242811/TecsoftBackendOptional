using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Repositories;

namespace VirtualExpress.ShipDelivery.Persistance.Repositories
{
    public class PackageDeliveryRepository:BaseRepository, IPackageDeliveryRepository
    {
        public PackageDeliveryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PackageDelivery packageDelivery)
        {
            await _context.PackageDeliveries.AddAsync(packageDelivery);
        }

        public async Task<PackageDelivery> FindById(int id)
        {
            return await _context.PackageDeliveries.FindAsync(id);
        }

        public async Task<IEnumerable<PackageDelivery>> ListAsync()
        {
            return await _context.PackageDeliveries.ToListAsync();
        }

        public void Remove(PackageDelivery packageDelivery)
        {
            _context.PackageDeliveries.Remove(packageDelivery);
        }

        public void Update(PackageDelivery packageDelivery)
        {
            _context.PackageDeliveries.Update(packageDelivery);
        }
    }
}
