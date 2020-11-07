using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Models;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Domain.Services.Responses;

namespace VirtualExpress.Initialization.Services
{
    public class DispatcherService: IDispatcherService
    {
        private readonly ITerminalRepository _terminalRepository;
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DispatcherService(IDispatcherRepository dispatcherRepository, IUnitOfWork unitOfWork, ITerminalRepository terminalRepository)
        {
            _dispatcherRepository = dispatcherRepository;
            _unitOfWork = unitOfWork;
            _terminalRepository = terminalRepository;
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
            var existingTerminal = await _terminalRepository.FindById(dispatcher.TerminalId);
            if (existingTerminal == null)
            {
                return new DispatcherResponse("Terminal doesnt exist");
            }
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
            existing.DNI = dispatcher.DNI;
            existing.TerminalId = dispatcher.TerminalId;
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
