using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Domain.Services.Responses;
using VirtualExpress.CompanyManagement.Domain.Repositories;

namespace VirtualExpress.Communication.Services
{
    public class CustomerServiceEmployeeService : ICustomerServiceEmployeeService
    {
        public readonly ITerminalRepository _terminalRepository;
        public readonly ICustomerServiceEmployeeRepository _CustomerServiceEmployeeRepository;
        public async Task<CustomerServiceEmployeeResponse> DeleteAsync(int id)
        {
            var existingCustomerServiceEmployee = await _CustomerServiceEmployeeRepository.FindById(id);
            if (existingCustomerServiceEmployee == null)
                return new CustomerServiceEmployeeResponse("Customer Service Employee not found");
            try
            {
                _CustomerServiceEmployeeRepository.Remove(existingCustomerServiceEmployee);


                return new CustomerServiceEmployeeResponse(existingCustomerServiceEmployee);
            }
            catch (Exception e)
            {
                return new CustomerServiceEmployeeResponse($"An error ocurred while deleting the Customer Service Employee: {e.Message}");
            }
        }

        public async Task<CustomerServiceEmployeeResponse> GetByIdAsync(int id)
        {
            var existingCustomerServiceEmployee = await _CustomerServiceEmployeeRepository.FindById(id);
            if (existingCustomerServiceEmployee == null)
                return new CustomerServiceEmployeeResponse("Customer Service Employee not found");
            return new CustomerServiceEmployeeResponse(existingCustomerServiceEmployee);
        }

        public async Task<IEnumerable<CustomerServiceEmployee>> ListAsync()
        {
            return await _CustomerServiceEmployeeRepository.ListAsync();
        }

        public async Task<IEnumerable<CustomerServiceEmployee>> ListByTerminalByIdAsync(int id)
        {
            return await _CustomerServiceEmployeeRepository.ListByTerminalByIdAsync(id);
        }

        public async Task<CustomerServiceEmployeeResponse> SaveAsync(CustomerServiceEmployee customerServiceEmployee)
        {
            var existingTerminals = await _terminalRepository.FindById(customerServiceEmployee.TerminalId);
            if (existingTerminals == null)
            {
                return new CustomerServiceEmployeeResponse("Terminal doesnt exist");
            }
            try
            {
                await _CustomerServiceEmployeeRepository.AddAsync(customerServiceEmployee);
                return new CustomerServiceEmployeeResponse(customerServiceEmployee);
            }
            catch (Exception e)
            {
                return new CustomerServiceEmployeeResponse($"An error ocurred while saving the Customer Service Employee: {e.Message}");
            }
        }

        public async Task<CustomerServiceEmployeeResponse> UpdateAsync(int id, CustomerServiceEmployee customerServiceEmployee)
        {
            var existingCustomerServiceEmployee = await _CustomerServiceEmployeeRepository.FindById(id);
            if (existingCustomerServiceEmployee == null)
                return new CustomerServiceEmployeeResponse("Customer Service Employee not found");
            existingCustomerServiceEmployee.TerminalId = customerServiceEmployee.TerminalId;
            try
            {
                _CustomerServiceEmployeeRepository.Update(existingCustomerServiceEmployee);

                return new CustomerServiceEmployeeResponse(customerServiceEmployee);
            }
            catch (Exception e)
            {
                return new CustomerServiceEmployeeResponse($"An error ocurred while updating the Customer Service Employee: {e.Message}");
            }
        }
    }
}
