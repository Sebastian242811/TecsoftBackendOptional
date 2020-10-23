using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Services
{
    public class DispatcherService: IDispatcherService
    {
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DispatcherService(IDispatcherRepository dispatcherRepository, IUnitOfWork unitOfWork)
        {
            _dispatcherRepository = dispatcherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DispatcherResponse> DeleteAsync(int id)
        {
            var existing = await _dispatcherRepository.FindById(id);
            if (existing == null)
                return new DispatcherResponse("Dispatcher not found");
            try
            {
                _dispatcherRepository.Remove(existing);
                await _unitOfWork.CompleteAsync();

                return new DispatcherResponse(existing);
            }
            catch (Exception e)
            {
                return new DispatcherResponse($"An error ocurred while deleting the Dispatcher: {e.Message}");
            }
        }

        public async Task<DispatcherResponse> GetById(int id)
        {
            var existing = await _dispatcherRepository.FindById(id);
            if (existing == null)
                return new DispatcherResponse("Dispatcher not found");
            return new DispatcherResponse(existing);
        }

        public async Task<IEnumerable<Dispatcher>> ListAsync()
        {
            return await _dispatcherRepository.ListAsync();
        }

        public async Task<DispatcherResponse> SaveAsync(Dispatcher dispatcher)
        {
            try
            {
                await _dispatcherRepository.AddAsync(dispatcher);
                await _unitOfWork.CompleteAsync();

                return new DispatcherResponse(dispatcher);
            }
            catch (Exception e)
            {
                return new DispatcherResponse($"An error ocurred while saving the Dispatcher: {e.Message}");
            }
        }

        public async Task<DispatcherResponse> UpdateAsync(int id, Dispatcher dispatcher)
        {
            var existing = await _dispatcherRepository.FindById(id);
            if (existing == null)
                return new DispatcherResponse("Dispatcher not found");
            existing.Name = dispatcher.Name;
            try
            {
                _dispatcherRepository.Update(existing);
                await _unitOfWork.CompleteAsync();

                return new DispatcherResponse(existing);
            }
            catch (Exception e)
            {
                return new DispatcherResponse($"An error ocurred while updating the Dispatcher: {e.Message}");
            }
        }
    }
}
