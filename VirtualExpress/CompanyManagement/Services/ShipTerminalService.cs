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
        private readonly IShipTerminalRepository _shipTerminalRepository;
        private readonly ITerminalRepository _terminalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShipTerminalService(IShipTerminalRepository shipTerminalRepository, ITerminalRepository terminalRepository, IUnitOfWork unitOfWork)
        {
            _shipTerminalRepository = shipTerminalRepository;
            _terminalRepository = terminalRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShipTerminalResponse> DeleteAsync(int originId, int destinyId)
        {
            var existingShipTerminal = await _shipTerminalRepository.FindByOriginIdAndDestinyId(originId, destinyId);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("ShipTerminal not found");
            try
            {
                _shipTerminalRepository.Remove(existingShipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(existingShipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while deleting ShipTerminal: {e.Message}");
            }
        }

        public async Task<ShipTerminalResponse> GetByOriginIdAndDestinyId(int originId, int destinyId)
        {
            var existingShipTerminal = await _shipTerminalRepository.FindByOriginIdAndDestinyId(originId, destinyId);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("ShipTerminal not found");

            return new ShipTerminalResponse(existingShipTerminal);
        }

        public async Task<IEnumerable<ShipTerminal>> GetCityOriginAndCityDestinyByCompanyId(int companyId)
        {
            return await _shipTerminalRepository.GetCityOriginAndCityDestinyByCompanyId(companyId);
        }

        public async Task<IEnumerable<ShipTerminal>> GetShipTerminalsByCompanyId(int companyId)
        {
            return await _shipTerminalRepository.GetShipTerminalsByCompanyId(companyId);
        }

        public async Task<IEnumerable<ShipTerminal>> GetShipTerminalsByOriginId(int originId)
        {
            return await _shipTerminalRepository.GetAllTerminalDestinyByOriginId(originId);
        }

        public async Task<IEnumerable<ShipTerminal>> ListAsync()
        {
            return await _shipTerminalRepository.ListAsync();
        }

        public async Task<ShipTerminalResponse> SaveAsync(ShipTerminal shipTerminal)
        {
            if (shipTerminal.TerminalOriginId == shipTerminal.TerminalDestinyId)
                return new ShipTerminalResponse("The originId and destinyId can't be equals");

            var existingShipTerminalInList = await _shipTerminalRepository.FindByOriginIdAndDestinyId(shipTerminal.TerminalOriginId, shipTerminal.TerminalDestinyId);
            if (existingShipTerminalInList != null)
                return new ShipTerminalResponse("ShipTerminal is begin used");

            var existingOrigin = await _terminalRepository.FindById(shipTerminal.TerminalOriginId);
            if (existingOrigin == null)
                return new ShipTerminalResponse("TerminalOrigin not found");

            var existingDestiny = await _terminalRepository.FindById(shipTerminal.TerminalDestinyId);
            if (existingDestiny == null)
                return new ShipTerminalResponse("TerminalDestiny not found");

            shipTerminal.TerminalOrigin = existingOrigin;
            shipTerminal.TerminalDestiny = existingDestiny;
            try
            {
                await _shipTerminalRepository.AddAsync(shipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(shipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while deleting ShipTerminal: {e.Message}");
            }
        }

        public async Task<ShipTerminalResponse> UpdateAsync(int originId, int destinyId, ShipTerminal shipTerminal)
        {
            if (shipTerminal.TerminalOriginId == shipTerminal.TerminalDestinyId)
                return new ShipTerminalResponse("The originId and destinyId can't be equals");

            if (originId == destinyId)
                return new ShipTerminalResponse("The originId and destinyId can't be equals");


            var existingShipTerminal = await _shipTerminalRepository.FindByOriginIdAndDestinyId(originId, destinyId);
            if (existingShipTerminal == null)
                return new ShipTerminalResponse("ShipTerminal not found");

            var existingShipTerminalInList = await _shipTerminalRepository.FindByOriginIdAndDestinyId(shipTerminal.TerminalOriginId, shipTerminal.TerminalDestinyId);
            if (existingShipTerminalInList != null)
                return new ShipTerminalResponse("ShipTerminal is begin used");

            var existingOrigin = await _terminalRepository.FindById(shipTerminal.TerminalOriginId);
            if (existingOrigin == null)
                return new ShipTerminalResponse("TerminalOrigin not found");

            var existingDestiny = await _terminalRepository.FindById(shipTerminal.TerminalDestinyId);
            if (existingDestiny == null)
                return new ShipTerminalResponse("TerminalDestiny not found");

            existingShipTerminal.TerminalOrigin = existingOrigin;
            existingShipTerminal.TerminalOriginId = shipTerminal.TerminalOriginId;
            existingShipTerminal.TerminalDestiny = existingDestiny;
            existingShipTerminal.TerminalDestinyId = shipTerminal.TerminalDestinyId;
            existingShipTerminal.Price = shipTerminal.Price;
            try
            {
                await _shipTerminalRepository.AddAsync(shipTerminal);
                await _unitOfWork.CompleteAsync();
                return new ShipTerminalResponse(shipTerminal);
            }
            catch (Exception e)
            {
                return new ShipTerminalResponse($"An error ocurred while deleting ShipTerminal: {e.Message}");
            }
        }
    }
}
