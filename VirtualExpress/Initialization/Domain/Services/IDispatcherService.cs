using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Models;
using VirtualExpress.Initialization.Domain.Services.Responses;

namespace VirtualExpress.Initialization.Domain.Services
{
    public interface IDispatcherService
    {
        Task<IEnumerable<Dispatcher>> ListAsync();
        Task<DispatcherResponse> GetById(int id);
        Task<DispatcherResponse> SaveAsync(Dispatcher dispatcher);
        Task<DispatcherResponse> DeleteAsync(int id);
        Task<DispatcherResponse> UpdateAsync(int id, Dispatcher dispatcher);
    }
}
