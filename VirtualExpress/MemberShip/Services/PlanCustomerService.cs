using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Services;
using VirtualExpress.MemberShip.Domain.Services.Responses;
using VirtualExpress.MemberShip.Model.Repositories;

namespace VirtualExpress.MemberShip.Services
{
    public class PlanCustomerService : IPlanCustomerService
    {
        private readonly IPlanCustomerRepository _planCustomerRepository;
        private readonly ITypeOfCurrentRepository _typeOfCurrentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlanCustomerService(IPlanCustomerRepository planCustomerRepository, ITypeOfCurrentRepository typeOfCurrentRepository, IUnitOfWork unitOfWork)
        {
            _planCustomerRepository = planCustomerRepository;
            _typeOfCurrentRepository = typeOfCurrentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanCustomerResponse> FindById(int id)
        {
            var existingPlan = await _planCustomerRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCustomerResponse("PlanCustomer not found");
            return new PlanCustomerResponse(existingPlan);
        }

        public async Task<IEnumerable<PlanCustomer>> ListAsync()
        {
            return await _planCustomerRepository.ListAsync();
        }

        public async Task<PlanCustomerResponse> RemoveAsync(int id)
        {
            var existingPlan = await _planCustomerRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCustomerResponse("PlanCustomer not found");

            try
            {
                _planCustomerRepository.Remove(existingPlan);
                await _unitOfWork.CompleteAsync();
                return new PlanCustomerResponse(existingPlan);
            }
            catch(Exception e)
            {
                return new PlanCustomerResponse($"An error ocurred while deleting the PlanCustomer {e.Message}");
            }
        }

        public async Task<PlanCustomerResponse> SaveAsync(PlanCustomer planCustomer)
        {
            var existingName = await _planCustomerRepository.FindByName(planCustomer.Name);
            if (existingName != null)
                return new PlanCustomerResponse("Name is begin used in other PlanCustomer");

            var existingType = await _typeOfCurrentRepository.FindById(planCustomer.TypeOfCurrentId);
            if (existingType == null)
                return new PlanCustomerResponse("TypeOfCurrent not found");

            
            try
            {
                planCustomer.TypeOfCurrent = existingType;
                await _planCustomerRepository.AddAsync(planCustomer);
                await _unitOfWork.CompleteAsync();
                return new PlanCustomerResponse(planCustomer);
            }
            catch(Exception e)
            {
                return new PlanCustomerResponse($"An error ocurred while saving the PlanCustomer: {e.Message}");
            }
        }

        public async Task<PlanCustomerResponse> UpdateAsync(int id, PlanCustomer planCustomer)
        {
            var existingPlan = await _planCustomerRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCustomerResponse("PlanCustomer not found");

            var existingTypeOfCurrent = await _typeOfCurrentRepository.FindById(planCustomer.TypeOfCurrentId);
            if (existingTypeOfCurrent == null)
                return new PlanCustomerResponse("TypeOfCurrent not found");

            if (existingPlan.Name != planCustomer.Name)
            {
                var existingName = await _planCustomerRepository.FindByName(planCustomer.Name);
                if (existingName != null)
                    return new PlanCustomerResponse("Name is begin used in other PlanCustomer");
            }

            existingPlan.Name = planCustomer.Name;
            existingPlan.TypeOfCurrent = existingTypeOfCurrent;
            existingPlan.TypeOfCurrentId = planCustomer.TypeOfCurrentId;
            existingPlan.Cost = planCustomer.Cost;
            try
            {
                _planCustomerRepository.Update(existingPlan);
                await _unitOfWork.CompleteAsync();
                return new PlanCustomerResponse(existingPlan);
            }
            catch(Exception e)
            {
                return new PlanCustomerResponse($"An error ocurred while updating the PlanCustomer {e.Message}");
            }
        }
    }
}
