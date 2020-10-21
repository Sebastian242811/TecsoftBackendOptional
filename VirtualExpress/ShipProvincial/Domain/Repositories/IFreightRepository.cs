using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Repositories
{
    interface IFreightRepository
    {
        Task<IEnumerable<Freight>> ListAsync();
        Task<IEnumerable<Freight>> ListByMothAndYearDate(int month, int year);
        Task AddAsync(Freight Freight);
        Task<Freight> FindById(int id);
        void Update(Freight Freight);
        void Remove(Freight Freight);
    }
}
