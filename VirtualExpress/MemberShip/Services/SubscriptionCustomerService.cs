using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.General.Extensions;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Services;
using VirtualExpress.MemberShip.Domain.Services.Responses;

namespace VirtualExpress.MemberShip.Services
{
    public class SubscriptionCustomerService : ISubscriptionCustomerService
    {
        private readonly ISubscriptionCustomerRepository _subscriptionCustomerRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPlanCustomerRepository _planCustomerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionCustomerService(ISubscriptionCustomerRepository subscriptionCustomerRepository, ICustomerRepository customerRepository, IPlanCustomerRepository planCustomerRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionCustomerRepository = subscriptionCustomerRepository;
            _customerRepository = customerRepository;
            _planCustomerRepository = planCustomerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionCustomerResponse> FindById(int id)
        {
            var existingSubscription = await _subscriptionCustomerRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCustomerResponse("SubscriptionCustomer not found");
            return new SubscriptionCustomerResponse(existingSubscription);
        }

        public async Task<IEnumerable<SubscriptionCustomer>> ListAsync()
        {
            return await _subscriptionCustomerRepository.ListAsync();
        }

        public async Task<SubscriptionCustomerResponse> RemoveAsync(int id)
        {
            var existingSubscription = await _subscriptionCustomerRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCustomerResponse("SubscriptionCustomer not found");
            try
            {
                _subscriptionCustomerRepository.Remove(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCustomerResponse(existingSubscription);
            }
            catch(Exception e)
            {
                return new SubscriptionCustomerResponse($"An error ocurred while deleting the SubscriptionCustomer {e.Message}");
            }
        }

        public async Task<SubscriptionCustomerResponse> SaveAsync(SubscriptionCustomer subscriptionCustomer)
        {
            int customerId = subscriptionCustomer.CustomerId;
            var existingCustomer = await _customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new SubscriptionCustomerResponse("Customer not found");

            int planId = subscriptionCustomer.PlanId;
            var existingPlan = await _planCustomerRepository.FindById(planId);
            if (existingPlan == null)
                return new SubscriptionCustomerResponse("PlanCustomer not found");

            try
            {
                await _subscriptionCustomerRepository.AddAsync(subscriptionCustomer);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCustomerResponse(subscriptionCustomer);
            }
            catch(Exception e)
            {
                return new SubscriptionCustomerResponse($"An error ocurred while saving the SubscriptionCustomer {e.Message}");
            }

        }

        public async Task<SubscriptionCustomerResponse> UpdateAsync(int id, SubscriptionCustomer subscriptionCustomer)
        {
            var existingSubscription = await _subscriptionCustomerRepository.FindById(id);
            if (existingSubscription == null)
                return new SubscriptionCustomerResponse("SubscriptionCustomer not found");

            int customerId = subscriptionCustomer.CustomerId;
            var existingCustomer = await _customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new SubscriptionCustomerResponse("Customer not found");

            int planId = subscriptionCustomer.PlanId;
            var existingPlan = await _planCustomerRepository.FindById(planId);
            if (existingPlan == null)
                return new SubscriptionCustomerResponse("PlanCustomer not found");

            existingSubscription.PlanCustomer = subscriptionCustomer.PlanCustomer;
            existingSubscription.CustomerId = subscriptionCustomer.CustomerId;
            existingSubscription.PlanId = subscriptionCustomer.PlanId;
            existingSubscription.DateTime = subscriptionCustomer.DateTime;
            existingSubscription.Discount = subscriptionCustomer.Discount;
            existingSubscription.TotalPrice = subscriptionCustomer.TotalPrice;
            try
            {
                _subscriptionCustomerRepository.Update(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionCustomerResponse(subscriptionCustomer);
            }
            catch (Exception e)
            {
                return new SubscriptionCustomerResponse($"An error ocurred while updating the SubscriptionCustomer {e.Message}");
            }
        }
    }
}
