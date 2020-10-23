using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.General.Persistance.Context;

namespace VirtualExpress.General.Repositories
{
    public class UnitOfWork : BaseRepository, IUnitOfWork
    {
        public UnitOfWork(AppDbContext context) : base(context)
        {
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
