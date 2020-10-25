using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Domain.Services.Responses;

namespace VirtualExpress.Register.Services
{
    public class DealerService : IDealerService
    {
        private readonly IDealerRepository _employeeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DealerService(IDealerRepository employeeRepository, ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DealerResponse> DeleteAsync(int employeeId)
        {
            var existingEmployee = await _employeeRepository.FindById(employeeId);
            if (existingEmployee == null)
                return new DealerResponse("Employee not found");
            try
            {
                _employeeRepository.Remove(existingEmployee);
                await _unitOfWork.CompleteAsync();
                return new DealerResponse(existingEmployee);
            }
            catch(Exception e)
            {
                return new DealerResponse($"An error ocurred while deleting the employee {e.Message}");
            }
        }

        public async Task<DealerResponse> FindEmployeeById(int employeeId)
        {
            var existingEmployee = await _employeeRepository.FindById(employeeId);
            if (existingEmployee == null)
                return new DealerResponse("Employee not found");
            return new DealerResponse(existingEmployee);
        }

        public async Task<IEnumerable<Dealer>> ListAsync()
        {
            return await _employeeRepository.ListAsync();
        }

        public async Task<DealerResponse> SaveAsync(Dealer employee)
        {
            var existingUsername = await _employeeRepository.FindByUsername(employee.Username);
            if(existingUsername != null)
                return new DealerResponse("This username is being used by another user");

            var existingCity = await _cityRepository.FindById(employee.CityId);
            if (existingCity == null)
                return new DealerResponse("City not found");

            var existingEmail = await _employeeRepository.FindByEmail(employee.Email);
            if(existingEmail != null)
                return new DealerResponse("This email is being used by another user");

            employee.City = existingCity;
            try
            {
                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                return new DealerResponse(employee);
            }
            catch (Exception e)
            {
                return new DealerResponse($"An error ocurred while saving the employee {e.Message}");
            }
        }

        public async Task<DealerResponse> UpdateAsync(int employeeId, Dealer employee)
        {
            var existingEmployee = await _employeeRepository.FindById(employeeId);
            if (existingEmployee == null)
                return new DealerResponse("Employee not found");

            existingEmployee.Brithday = employee.Brithday;
            existingEmployee.Username = employee.Username;
            existingEmployee.City = employee.City;
            existingEmployee.Email = employee.Email;
            existingEmployee.Name = employee.Name;
            existingEmployee.Number = employee.Number;
            existingEmployee.Password = employee.Password;
            try
            {
                _employeeRepository.Update(existingEmployee);
                await _unitOfWork.CompleteAsync();
                return new DealerResponse(existingEmployee);
            }
            catch (Exception e)
            {
                return new DealerResponse($"An error ocurred while updating the employee {e.Message}");
            }
        }
    }
}
