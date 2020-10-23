using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Repositories;

namespace VirtualExpress.ShipDelivery.Persistance.Repositories
{
    public class DeliveryRepository : BaseRepository, IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
        }

        public async Task<Delivery> FindById(int id)
        {
            return await _context.Deliveries.FindAsync(id);
        }

        public async Task<IEnumerable<Delivery>> ListAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public void Remove(Delivery delivery)
        {
            _context.Deliveries.Remove(delivery);
        }

        public void Update(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
        }
    }
}
