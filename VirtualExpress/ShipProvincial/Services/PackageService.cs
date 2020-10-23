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
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IPackageRepository packageRepository, IUnitOfWork unitOfWork)
        {
            _packageRepository = packageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PackageResponse> DeleteAsync(int id)
        {
            var existing = await _packageRepository.FindById(id);
            if (existing == null)
                return new PackageResponse("Package not found");
            try
            {
                _packageRepository.Remove(existing);
                await _unitOfWork.CompleteAsync();

                return new PackageResponse(existing);
            }
            catch (Exception e)
            {
                return new PackageResponse($"An error ocurred while deleting Package: {e.Message}");
            }
        }

        public async Task<PackageResponse> GetById(int id)
        {
            var existing = await _packageRepository.FindById(id);
            if (existing == null)
                return new PackageResponse("Package not found");
            return new PackageResponse(existing);
        }

        public async Task<PackageResponse> GetInfoToDispatcherByPackageId(int packageId)
        {
            var existing = await _packageRepository.FindById(packageId);
            if (existing == null)
                return new PackageResponse("Package not found");
            return new PackageResponse("Dispacher: " + existing.Dispatcher.Name + " Dni: " + existing.Dispatcher.DNI);
        }

        public async Task<IEnumerable<Package>> ListAsync()
        {
            return await _packageRepository.ListAsync();
        }

        public async Task<IEnumerable<Package>> ListByCostumerId(int costumerId)
        {
            return await _packageRepository.ListByCostumerId(costumerId);
        }

        public async Task<PackageResponse> SaveAsync(Package package)
        {
            try
            {
                await _packageRepository.AddAsync(package);
                await _unitOfWork.CompleteAsync();

                return new PackageResponse(package);
            }
            catch (Exception e)
            {
                return new PackageResponse($"An error ocurred while saving Package: {e.Message}");
            }
        }

        public async Task<PackageResponse> UpdateAsync(int id, Package package)
        {
            var existing = await _packageRepository.FindById(id);
            if (existing == null)
                return new PackageResponse("Package not found");
            existing.Observations = package.Observations;
            try
            {
                _packageRepository.Update(existing);
                await _unitOfWork.CompleteAsync();

                return new PackageResponse(existing);
            }
            catch (Exception e)
            {
                return new PackageResponse($"An error ocurred while updating Package: {e.Message}");
            }
        }
    }
}
