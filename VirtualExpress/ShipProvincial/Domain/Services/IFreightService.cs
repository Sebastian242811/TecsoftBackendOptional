using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Domain.Services
{
    public interface IFreightService
    {
        Task<IEnumerable<Freight>> ListAsync();
        Task<IEnumerable<Freight>> ListByMonthAndYearDate(int month, int year);
        Task<FreightResponse> GetByIdAsync(int id);
        Task<FreightResponse> SaveAsync(Freight freight);
        Task<FreightResponse> UpdateAsync(int id, Freight freight);
        Task<FreightResponse> DeleteAsync(int id);
    }
}
