using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Services.Responses;

namespace VirtualExpress.MemberShip.Model.Services
{
    public interface ITypeOfCurrentService
    {
        Task<IEnumerable<TypeOfCurrent>> GetAllAsync();
        Task<TypeOfCurrentResponse> FindById(int id);
        Task<TypeOfCurrentResponse> SaveAsync(TypeOfCurrent typeOfCurrent);
        Task<TypeOfCurrentResponse> UpdateAsync(int id, TypeOfCurrent typeOfCurrent);
        Task<TypeOfCurrentResponse> DeleteAsync(int id);
    }
}
