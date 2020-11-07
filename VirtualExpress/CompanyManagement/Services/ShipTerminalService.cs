using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;
using VirtualExpress.General.Domain.Repositories;

namespace VirtualExpress.CompanyManagement.Services
{
    public class ShipTerminalService : IShipTerminalService
    {
        private readonly ITerminalRepository _terminalRepository;
        private readonly IShipTerminalRepository _shipTerminalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShipTerminalService(IUnitOfWork unitOfWork, IShipTerminalRepository shipTerminalRepository, ITerminalRepository terminalRepository)
        {
            _unitOfWork = unitOfWork;
            _shipTerminalRepository = shipTerminalRepository;
            _terminalRepository = terminalRepository;
        }

        public async Task<ShipTerminalResponse> DeleteAsync(int id)
        {
            var existingShipTerminal = await _shipTerminalRepository.FindById(id);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("Data not found");

            try
            {
                _shipTerminalRepository.Remove(existingShipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(existingShipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while deleting data: {e.Message}");
            }
        }

        public async Task<ShipTerminalResponse> FindCityById(int id)
        {
            var existingShipTerminal = await _shipTerminalRepository.FindById(id);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("Data not found");
            return new ShipTerminalResponse(existingShipTerminal);
        }

        public async Task<IEnumerable<ShipTerminal>> ListAsync()
        {
            return await _shipTerminalRepository.ListAsync();
        }

        public async Task<ShipTerminalResponse> SaveAsync(ShipTerminal shipTerminal)
        {
            var existingDestinyTerminals = await _terminalRepository.FindById(shipTerminal.TerminalDestinyId);
            var existingOriginTerminals = await _terminalRepository.FindById(shipTerminal.TerminalOriginId);
            var alreadyregistereddata = await _shipTerminalRepository.ListAsync();
            foreach(ShipTerminal itera in alreadyregistereddata)
            {
                if(itera.TerminalDestinyId==shipTerminal.TerminalDestinyId && itera.TerminalOriginId == shipTerminal.TerminalOriginId && itera.Price == shipTerminal.Price)
                {
                    return new ShipTerminalResponse("This data is already been registered");
                }
            }
            if (existingOriginTerminals == null)
            {
                return new ShipTerminalResponse("Origin terminal doesnt exist");
            }
            if (existingDestinyTerminals == null)
            {
                return new ShipTerminalResponse("Destiny terminal doesnt exist");
            }
            try
            {
                await _shipTerminalRepository.AddAsync(shipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(shipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while saving the data: {e.Message}");
            }
        }

        public async Task<ShipTerminalResponse> UpdateAsync(int id, ShipTerminal shipTerminal)
        {
            var existingShipTerminal = await _shipTerminalRepository.FindById(id);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("Data not found");

            existingShipTerminal.Price=shipTerminal.Price;
            existingShipTerminal.TerminalOriginId = shipTerminal.TerminalOriginId;
            existingShipTerminal.TerminalDestinyId = shipTerminal.TerminalOriginId;
            try
            {
                _shipTerminalRepository.Update(existingShipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(existingShipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while updating the data: {e.Message}");
            }
        }
    }
}
