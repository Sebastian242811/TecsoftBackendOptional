﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Repositories;
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Domain.Services.Responses;

namespace VirtualExpress.ShipDelivery.Services
{
    public class PackageDeliveryService:IPackageDeliveryService
    {
        private readonly IPackageDeliveryRepository _PackageDeliveryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PackageDeliveryService(IPackageDeliveryRepository PackageDeliveryRepository, IUnitOfWork unitOfWork)
        {
            _PackageDeliveryRepository = PackageDeliveryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PackageDeliveryResponse> DeleteAsync(int id)
        {
            var existingPackageDelivery = await _PackageDeliveryRepository.FindById(id);
            if (existingPackageDelivery == null)
                return new PackageDeliveryResponse("PackageDelivery not found");
            try
            {
                _PackageDeliveryRepository.Remove(existingPackageDelivery);
                await _unitOfWork.CompleteAsync();

                return new PackageDeliveryResponse(existingPackageDelivery);
            }
            catch (Exception e)
            {
                return new PackageDeliveryResponse($"An error ocurred while deleting the PackageDelivery: {e.Message}");
            }
        }

        public async Task<PackageDeliveryResponse> GetById(int id)
        {
            var existingPackageDelivery = await _PackageDeliveryRepository.FindById(id);
            if (existingPackageDelivery == null)
                return new PackageDeliveryResponse("PackageDelivery not found");
            return new PackageDeliveryResponse(existingPackageDelivery);
        }

        public async Task<IEnumerable<PackageDelivery>> ListAsync()
        {
            return await _PackageDeliveryRepository.ListAsync();
        }

        public async Task<PackageDeliveryResponse> SaveAsync(PackageDelivery PackageDelivery)
        {
            try
            {
                await _PackageDeliveryRepository.AddAsync(PackageDelivery);
                await _unitOfWork.CompleteAsync();
                return new PackageDeliveryResponse(PackageDelivery);
            }
            catch (Exception e)
            {
                return new PackageDeliveryResponse($"An error ocurred while saving the PackageDelivery: {e.Message}");
            }
        }

        public async Task<PackageDeliveryResponse> UpdateAsync(int id, PackageDelivery PackageDelivery)
        {
            var existingPackageDelivery = await _PackageDeliveryRepository.FindById(id);
            if (existingPackageDelivery == null)
                return new PackageDeliveryResponse("PackageDelivery not found");
            existingPackageDelivery.DeliveryId = PackageDelivery.DeliveryId;
            try
            {
                _PackageDeliveryRepository.Update(existingPackageDelivery);
                await _unitOfWork.CompleteAsync();

                return new PackageDeliveryResponse(PackageDelivery);
            }
            catch (Exception e)
            {
                return new PackageDeliveryResponse($"An error ocurred while updating the PackageDelivery: {e.Message}");
            }

        }
    }
}
