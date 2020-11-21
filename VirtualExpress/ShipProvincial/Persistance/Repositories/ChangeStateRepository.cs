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
    public class ChangeStateRepository : BaseRepository, IChangeStateRepository
    {
        public ChangeStateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ChangeState ChangeState)
        {
            await _context.ChangesStates.AddAsync(ChangeState);
        }

        public async Task<ChangeState> FindById(int id)
        {
            return await _context.ChangesStates.FindAsync(id);
        }

        public async Task<ChangeState> GetByPackageIdAndInitStateAndEndState(int packageId, int initState, int endState)
        {
            return await _context.ChangesStates
                .Where(p => p.PackageId == packageId)
                .Where(p => p.InitialState == (EState)initState)
                .Where(p => p.FinalState == (EState)endState)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ChangeState>> ListAsync()
        {
            return await _context.ChangesStates.ToListAsync();
        }

        public async Task<IEnumerable<ChangeState>> ListAsyncbypackageid(int id)
        {
            return await _context.ChangesStates.Where(p => p.PackageId == id).ToListAsync();
        }

        public void Update(ChangeState ChangeState)
        {
            _context.ChangesStates.Update(ChangeState);
        }
    }
}
