using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Repositories;

namespace VirtualExpress.MemberShip.Persistence.Repositories
{
    public class TypeOfCurrentRepository : BaseRepository, ITypeOfCurrentRepository
    {
        public TypeOfCurrentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TypeOfCurrent typeOfCurrent)
        {
            await _context.TypeOfCurrents.AddAsync(typeOfCurrent);
        }

        public async Task<TypeOfCurrent> FindById(int id)
        {
            return await _context.TypeOfCurrents.FindAsync(id);
        }

        public async Task<TypeOfCurrent> FindByName(string name)
        {
            return await _context.TypeOfCurrents
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TypeOfCurrent>> ListAsync()
        {
            return await _context.TypeOfCurrents.ToListAsync();
        }

        public void Remove(TypeOfCurrent typeOfCurrent)
        {
            _context.TypeOfCurrents.Remove(typeOfCurrent);
        }

        public void Update(TypeOfCurrent typeOfCurrent)
        {
            _context.TypeOfCurrents.Update(typeOfCurrent);
        }
    }
}
