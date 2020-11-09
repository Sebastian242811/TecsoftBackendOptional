using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Services
{
    public class PackageService : IPackageService
    {
        private readonly IFreightRepository _freightRepository;
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IFreightRepository freightRepository, IDispatcherRepository dispatcherRepository, ICustomerRepository customerRepository, IPackageRepository packageRepository, IUnitOfWork unitOfWork)
        {
            _freightRepository = freightRepository;
            _dispatcherRepository = dispatcherRepository;
            _customerRepository = customerRepository;
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
            return new PackageResponse("o");
            //return new PackageResponse("Dispacher: " + existing.Dispatcher.Name + " Dni: " + existing.Dispatcher.DNI);
        }

        public async Task<IEnumerable<Package>> ListAsync()
        {
            return await _packageRepository.ListAsync();
        }

        public async Task<IEnumerable<Package>> ListByCostumerId(int costumerId)
        {
            return await _packageRepository.ListByCostumerId(costumerId);
        }

        public async Task<IEnumerable<Package>> ListByState(int customerId)
        {
            var existingcustomerpackage = await _packageRepository.ListByCostumerId(customerId);
            List<Package> packages = new List<Package>();
            foreach(Package package in existingcustomerpackage)
            {
                if (!package.State.Equals(EState.Shipped))
                {
                    packages.Add(package);
                }
            }
            return packages;
        }

        public async Task<PackageResponse> SaveAsync(Package package)
        {
            var existingFreight = await _freightRepository.FindById(package.FerightId);
            var existingDispatcher = await _dispatcherRepository.FindById(package.DispatcherId);
            var existingCustomer = await _customerRepository.FindById(package.CustomerId);


            if (existingFreight == null)
            {
                return new PackageResponse("Freight doesnt exist");
            }
            if (existingDispatcher == null)
            {
                return new PackageResponse("Dispatcher doesnt exist");
            }
            if (existingCustomer == null)
            {
                return new PackageResponse("Customer doesnt exist");
            }

            package.Freight = existingFreight;
            package.Customer = existingCustomer;
            package.Dispatcher = existingDispatcher;
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
            existing.Description = package.Description;
            existing.Observations = package.Observations;
            //existing.Priority = package.Priority;
            //existing.State = package.State;
            //existing.Weight = package.Weight;
            //existing.Discount = package.Discount;
            //existing.FerightId = package.FerightId;
            //existing.DispatcherId = package.DispatcherId;
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

        public async Task<PackageResponse> UpdateStateAsync(int id, Package package)
        {
            var existing = await _packageRepository.FindById(id);
            if (existing == null)
                return new PackageResponse("Package not found");
            //existing.State = package.State;
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
