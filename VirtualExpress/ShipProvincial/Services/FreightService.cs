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
    public class FreightService:IFreightService
    {
        private readonly IFreightRepository _freightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FreightService(IFreightRepository freightRepository, IUnitOfWork unitOfWork)
        {
            _freightRepository = freightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FreightResponse> DeleteAsync(int id)
        {
            var existingFreight = await _freightRepository.FindById(id);
            if (existingFreight == null)
                return new FreightResponse("Freight not found");
            try
            {
                _freightRepository.Remove(existingFreight);
                await _unitOfWork.CompleteAsync();

                return new FreightResponse(existingFreight);
            }
            catch (Exception e)
            {
                return new FreightResponse($"An error ocurred while deleting the Freight: {e.Message}");
            }
        }

        public async Task<FreightResponse> GetByIdAsync(int id)
        {
            var existingFreight = await _freightRepository.FindById(id);
            if (existingFreight == null)
                return new FreightResponse("Freight not found");
            return new FreightResponse(existingFreight);
        }

        public async Task<IEnumerable<Freight>> ListAsync()
        {
            return await _freightRepository.ListAsync();
        }

        public async Task<IEnumerable<Freight>> ListByMonthAndYearDate(int month, int year)
        {
            return await _freightRepository.ListByMothAndYearDate(month, year);
        }

        public async Task<FreightResponse> SaveAsync(Freight freight)
        {
            try
            {
                await _freightRepository.AddAsync(freight);
                await _unitOfWork.CompleteAsync();

                return new FreightResponse(freight);
            }
            catch (Exception e)
            {
                return new FreightResponse($"An error ocurred while saving the Freight: {e.Message}");
            }
        }

        public async Task<FreightResponse> UpdateAsync(int id, Freight freight)
        {
            var existingFreight = await _freightRepository.FindById(id);
            if (existingFreight == null)
                return new FreightResponse("Freight not found");
            existingFreight.Id = freight.Id;
            try
            {
                _freightRepository.Update(existingFreight);
                await _unitOfWork.CompleteAsync();

                return new FreightResponse(existingFreight);
            }
            catch (Exception e)
            {
                return new FreightResponse($"An error ocurred while updating the Freight: {e.Message}");
            }
        }
    }
}
