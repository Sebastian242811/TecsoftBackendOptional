using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Domain.Services.Communications;

namespace VirtualExpress.Register.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerResponse> DeleteAsync(int customerId)
        {
            var existingCustomer = await _customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");
            try
            {
                _customerRepository.Delete(existingCustomer);
                await _unitOfWork.CompleteAsync();
                return new CustomerResponse(existingCustomer);
            }
            catch(Exception e)
            {
                return new CustomerResponse($"An error ocurred while deleting the customer: {e.Message}");
            }
        }

        public async Task<CustomerResponse> FindCustomerById(int customerId)
        {
            var existingCustomer = await _customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");
            return new CustomerResponse(existingCustomer);
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _customerRepository.ListAsync();
        }

        public async Task<CustomerResponse> SaveAsync(Customer customer)
        {
            var existingCity = await _cityRepository.FindById(customer.CityId);
            if (existingCity == null)
                return new CustomerResponse("City not found");

            var existingUsername = await _customerRepository.FindByUsername(customer.Username);
            if (existingUsername != null)
                return new CustomerResponse("This username is being used by another user");

            var existingEmail = await _customerRepository.FindByEmail(customer.Email);
            if (existingEmail != null)
                return new CustomerResponse("This email is being used by another user");

            customer.City = existingCity;
            try
            {                
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();
                return new CustomerResponse(customer);
            }
            catch (Exception e)
            {
                return new CustomerResponse($"An error ocurred while saving the customer: {e.Message}");
            }
        }

        public async Task<CustomerResponse> UpdateAsync(int customerId, Customer customer)
        {
            //Arreglar
            var existingCustomer = await _customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");
            var existingCity = await _cityRepository.FindById(customer.CityId);
            if (existingCity == null)
                return new CustomerResponse("City not found");

            existingCustomer.Brithday = customer.Brithday;
            existingCustomer.City = existingCity;
            existingCustomer.CityId = customer.CityId;
            existingCustomer.Email = customer.Email;
            existingCustomer.Name = customer.Name;
            existingCustomer.Number = customer.Number;
            existingCustomer.Password = customer.Password;
            existingCustomer.Username = customer.Username;
            try
            {
                await _customerRepository.AddAsync(existingCustomer);
                await _unitOfWork.CompleteAsync();
                return new CustomerResponse(existingCustomer);
            }
            catch (Exception e)
            {
                return new CustomerResponse($"An error ocurred while updating the customer: {e.Message}");
            }
        }
    }
}
