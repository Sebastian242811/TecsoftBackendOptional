using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Repositories;
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Domain.Services.Responses;

namespace VirtualExpress.ShipDelivery.Services
{
    public class DeliveryService:IDeliveryService
    {
        private readonly IDeliveryRepository _DeliveryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryService(IDeliveryRepository DeliveryRepository, IUnitOfWork unitOfWork)
        {
            _DeliveryRepository = DeliveryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeliveryResponse> DeleteAsync(int id)
        {
            var existingDelivery = await _DeliveryRepository.FindById(id);
            if (existingDelivery == null)
                return new DeliveryResponse("Delivery not found");
            try
            {
                _DeliveryRepository.Remove(existingDelivery);
                await _unitOfWork.CompleteAsync();

                return new DeliveryResponse(existingDelivery);
            }
            catch (Exception e)
            {
                return new DeliveryResponse($"An error ocurred while deleting the Delivery: {e.Message}");
            }
        }

        public async Task<DeliveryResponse> GetById(int id)
        {
            var existingDelivery = await _DeliveryRepository.FindById(id);
            if (existingDelivery == null)
                return new DeliveryResponse("Delivery not found");
            return new DeliveryResponse(existingDelivery);
        }

        public async Task<IEnumerable<Delivery>> ListAsync()
        {
            return await _DeliveryRepository.ListAsync();
        }

        public async Task<DeliveryResponse> SaveAsync(Delivery Delivery)
        {
            try
            {
                await _DeliveryRepository.AddAsync(Delivery);
                await _unitOfWork.CompleteAsync();
                return new DeliveryResponse(Delivery);
            }
            catch (Exception e)
            {
                return new DeliveryResponse($"An error ocurred while saving the Delivery: {e.Message}");
            }
        }

        public async Task<DeliveryResponse> UpdateAsync(int id, Delivery Delivery)
        {
            var existingDelivery = await _DeliveryRepository.FindById(id);
            if (existingDelivery == null)
                return new DeliveryResponse("Delivery not found");
            existingDelivery.Id = Delivery.Id;
            try
            {
                _DeliveryRepository.Update(existingDelivery);
                await _unitOfWork.CompleteAsync();

                return new DeliveryResponse(Delivery);
            }
            catch (Exception e)
            {
                return new DeliveryResponse($"An error ocurred while updating the Delivery: {e.Message}");
            }

        }
    }
}
