using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Services;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Services
{
    public class SubscriptionCompanyService : ISubscriptionCompanyService
    {
        private readonly ISubscriptionCompanyRepository _subscriptionCompanyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlanCompanyRepository _planCompanyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionCompanyService(ISubscriptionCompanyRepository subscriptionCompanyRepository, ICompanyRepository companyRepository, IPlanCompanyRepository planCompanyRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionCompanyRepository = subscriptionCompanyRepository;
            _companyRepository = companyRepository;
            _planCompanyRepository = planCompanyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionCompanyResponse> FindById(int id)
        {
            var existingSubscription = await _subscriptionCompanyRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCompanyResponse("SubscriptionCompany not found");
            return new SubscriptionCompanyResponse(existingSubscription);
        }

        public async Task<IEnumerable<SubscriptionCompany>> ListAsync()
        {
            return await _subscriptionCompanyRepository.ListAsync();
        }

        public async Task<SubscriptionCompanyResponse> RemoveAsync(int id)
        {
            var existingSubscription = await _subscriptionCompanyRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCompanyResponse("SubscriptionCompany not found");
            try
            {
                _subscriptionCompanyRepository.Remove(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCompanyResponse(existingSubscription);
            }
            catch(Exception e)
            {
                return new SubscriptionCompanyResponse($"An error ocurred while deleting the SubscriptionCompany {e.Message}");
            }
        }

        public async Task<SubscriptionCompanyResponse> SaveAsync(Domain.Model.SubscriptionCompany subscriptionCompany)
        {
            int companyId = subscriptionCompany.CompanyId;
            var existingCompany = await _companyRepository.FindCompanyById(companyId);
            if (existingCompany == null)
                return new SubscriptionCompanyResponse("Company not found");

            int planId = subscriptionCompany.PlanId;
            var existingPlan = await _planCompanyRepository.FindById(planId);
            if (existingPlan == null)
                return new SubscriptionCompanyResponse("PlanCompany not found");

            try
            {
                await _subscriptionCompanyRepository.AddAsync(subscriptionCompany);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCompanyResponse(subscriptionCompany);
            }
            catch(Exception e)
            {
                return new SubscriptionCompanyResponse($"An error ocurred while saving the SubscriptionCompany {e.Message}");
            }
        }

        public async Task<SubscriptionCompanyResponse> UpdateAsync(int id, SubscriptionCompany subscriptionCompany)
        {
            var existingSubscription = await _subscriptionCompanyRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCompanyResponse("SubscriptionCompany not found");

            int companyId = subscriptionCompany.CompanyId;
            var existingCompany = await _companyRepository.FindCompanyById(companyId);
            if (existingCompany == null)
                return new SubscriptionCompanyResponse("Company not found");

            int planId = subscriptionCompany.PlanId;
            var existingPlan = await _planCompanyRepository.FindById(planId);
            if (existingPlan == null)
                return new SubscriptionCompanyResponse("PlanCompany not found");

            existingSubscription.Company = existingCompany;
            existingSubscription.CompanyId = companyId;
            existingSubscription.DateTime = subscriptionCompany.DateTime;
            existingSubscription.Discount = subscriptionCompany.Discount;
            existingSubscription.PlanCompany = existingPlan;
            existingSubscription.PlanId = planId;
            existingSubscription.TotalPrice = subscriptionCompany.TotalPrice;
            try
            {
                _subscriptionCompanyRepository.Update(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCompanyResponse(existingSubscription);
            }
            catch(Exception e)
            {
                return new SubscriptionCompanyResponse($"An error ocurred while updating the SubscriptionCompany {e.Message}");
            }
        }
    }
}
