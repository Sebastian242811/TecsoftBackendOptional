using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Model.Model;

namespace VirtualExpress.MemberShip.Model.Repositories
{
    public interface ITypeOfCurrentRepository
    {
        Task<IEnumerable<TypeOfCurrent>> ListAsync();
        Task AddAsync(TypeOfCurrent typeOfCurrent);
        Task<TypeOfCurrent> FindById(int id);
        Task<TypeOfCurrent> FindByName(string name);
        void Update(TypeOfCurrent typeOfCurrent);
        void Remove(TypeOfCurrent typeOfCurrent);

    }
}
