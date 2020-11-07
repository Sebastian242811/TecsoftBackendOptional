using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Domain.Services
{
    public interface IChangeStateService
    {
        Task<IEnumerable<ChangeState>> ListAsync();
        Task<ChangeStateResponse> FindChangeStateById(int id);
        Task<ChangeStateResponse> SaveAsync(ChangeState ChangeState);
    }
}
