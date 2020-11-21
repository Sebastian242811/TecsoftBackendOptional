using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        private readonly IChangeStateRepository _changeStateRepository;
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IShipTerminalRepository _shipTerminalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IFreightRepository freightRepository, IDispatcherRepository dispatcherRepository, ICustomerRepository customerRepository, IPackageRepository packageRepository, IUnitOfWork unitOfWork, IShipTerminalRepository shipTerminalRepository, IChangeStateRepository changeStateRepository)
        {
            _freightRepository = freightRepository;
            _dispatcherRepository = dispatcherRepository;
            _customerRepository = customerRepository;
            _packageRepository = packageRepository;
            _unitOfWork = unitOfWork;
            _shipTerminalRepository = shipTerminalRepository;
            _changeStateRepository = changeStateRepository;
        }

        public async Task<PackageResponse> AddDispatcher(int packageId, int dispatcherId)
        {
            var existingDispatcher = await _dispatcherRepository.FindById(dispatcherId);
            if (existingDispatcher == null)
                return new PackageResponse("Dispatcher not found");
            var existingPackage = await _packageRepository.FindById(packageId);
            if (existingPackage == null)
                return new PackageResponse("Package not found");

            existingPackage.DispatcherId = dispatcherId;
            existingPackage.Dispatcher = existingDispatcher;
            try
            {
                _packageRepository.Update(existingPackage);
                await _unitOfWork.CompleteAsync();
                return new PackageResponse(existingPackage);
            }
            catch(Exception e)
            {
                return new PackageResponse($"An error ocurred while adding the dispatcher: {e.Message}");
            }
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

        public async Task<IEnumerable<Package>> ListByCustomerAndStateIsNotShipped(int customerId)
        {
            return await _packageRepository.ListByCustomerShipped(customerId);
        }

        public async Task<IEnumerable<Package>> ListByDispatcherNull()
        {
            return await _packageRepository.ListByDispatcherNull();
        }

        public async Task<IEnumerable<Package>> ListByState(int state, int dispatcherId)
        {
            return await _packageRepository.ListByState(state, dispatcherId);
        }

        public async Task<PackageResponse> SaveAsync(Package package)
        {
            var existingCustomer = await _customerRepository.FindById(package.CustomerId);
            var existingShipTerminal = await _shipTerminalRepository.FindByOriginIdAndDestinyId(package.TerminalOriginId, package.TerminalDestinyId);

            
            if (existingCustomer == null)
            {
                return new PackageResponse("Customer doesnt exist");
            }
            if (existingShipTerminal == null)
            {
                return new PackageResponse("Shipterminal doesnt exist");
            }

            package.Freight = null  ;
            package.Customer = existingCustomer;
            package.State = EState.Waiting;
            package.Dispatcher = null;
            package.ShipTerminal = existingShipTerminal;
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

        public async Task<PackageResponse> UpdateStateAsync(int id, int value)
        {
            if (value <= 0 || value >= 6)
            {
                return new PackageResponse("The state not found");
            }

            var existingPackage = await _packageRepository.FindById(id);

            if(existingPackage == null)
            {
                return new PackageResponse("Package not found");
            }

            existingPackage.State = (EState)value;

            ChangeState changeState = new ChangeState();
            changeState.InitialState = (EState)(value - 1);
            changeState.FinalState = (EState)value;
            changeState.Package = existingPackage;
            changeState.PackageId = id;
            changeState.EditDate = DateTime.Now;

            try
            {
                _packageRepository.Update(existingPackage);
                await _unitOfWork.CompleteAsync();
                await _changeStateRepository.AddAsync(changeState);
                await _unitOfWork.CompleteAsync();
                return new PackageResponse(existingPackage);
            }
            catch (Exception e)
            {
                return new PackageResponse($"An error ocurred while update the state: {e.Message}");
            }
        }
    }
}
