using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Domain.Services.Communications;

namespace VirtualExpress.Register.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyResponse> DeleteAsync(int id)
        {
            var existingCustomer = await _companyRepository.FindCompanyById(id);
            if (existingCustomer == null)
                return new CompanyResponse("Customer not found");

            try
            {
                _companyRepository.Remove(existingCustomer);
                await _unitOfWork.CompleteAsync();
                return new CompanyResponse(existingCustomer);
            }
            catch(Exception e)
            {
                return new CompanyResponse($"An error ocurred while deleting the company: {e.Message}");
            }
        }

        public async Task<CompanyResponse> FindCompanyByCityId(int cityId)
        {
            var existingCustomer = await _companyRepository.FindCompanyById(cityId);
            if (existingCustomer == null)
                return new CompanyResponse("Customer not found");
            return new CompanyResponse(existingCustomer);
            //mal
        }

        public async Task<CompanyResponse> FindCompanyById(int companyId)
        {
            var existingCustomer = await _companyRepository.FindCompanyById(companyId);
            if (existingCustomer == null)
                return new CompanyResponse("Customer not found");
            return new CompanyResponse(existingCustomer);
        }

        public async Task<IEnumerable<Company>> ListAsync()
        {
            return await _companyRepository.ListAsync();
        }

        public async Task<CompanyResponse> SaveAsync(Company company)
        {
            var existingUsername = await _companyRepository.FindByUsername(company.Username);
            if (existingUsername != null)
                return new CompanyResponse("This username is being used by another user");

            var existingEmail = await _companyRepository.FindByEmail(company.Email);
            if (existingEmail != null)
                return new CompanyResponse("This email is being used by another user");

            try
            {
                await _companyRepository.AddAsync(company);
                await _unitOfWork.CompleteAsync();
                return new CompanyResponse(company);
            }
            catch (Exception e)
            {
                return new CompanyResponse($"An error ocurred while saving the company: {e.Message}");
            }
        }

        public async Task<CompanyResponse> UpdateAsync(int companyId, Company company)
        {
            var existingCustomer = await _companyRepository.FindCompanyById(companyId);
            if (existingCustomer == null)
                return new CompanyResponse("Customer not found");

            existingCustomer.Email = company.Email;
            existingCustomer.Name = company.Name;
            existingCustomer.Number = company.Number;
            existingCustomer.Password = company.Password;
            existingCustomer.Ruc = company.Ruc;
            try
            {
                _companyRepository.Remove(existingCustomer);
                await _unitOfWork.CompleteAsync();
                return new CompanyResponse(existingCustomer);
            }
            catch (Exception e)
            {
                return new CompanyResponse($"An error ocurred while updating the company: {e.Message}");
            }
        }
    }
}
