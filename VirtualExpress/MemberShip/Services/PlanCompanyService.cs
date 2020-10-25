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
    public class PlanCompanyService : IPlanCompanyService
    {
        private readonly IPlanCompanyRepository _planCompanyRepository;
        private readonly ITypeOfCurrentRepository _typeOfCurrentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlanCompanyService(IPlanCompanyRepository planCompanyRepository, ITypeOfCurrentRepository typeOfCurrentRepository, IUnitOfWork unitOfWork)
        {
            _planCompanyRepository = planCompanyRepository;
            _typeOfCurrentRepository = typeOfCurrentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanCompanyResponse> FindById(int id)
        {
            var existingPlan = await _planCompanyRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCompanyResponse("PlanCompany not found");
            return new PlanCompanyResponse(existingPlan);
        }

        public async Task<IEnumerable<PlanCompany>> ListAsync()
        {
            return await _planCompanyRepository.ListAsync();
        }

        public async Task<PlanCompanyResponse> RemoveAsync(int id)
        {
            var existingPlan = await _planCompanyRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCompanyResponse("PlanCompany not found");
            try
            {
                _planCompanyRepository.Remove(existingPlan);
                await _unitOfWork.CompleteAsync();
                return new PlanCompanyResponse(existingPlan);
            }
            catch(Exception e)
            {
                return new PlanCompanyResponse($"An error ocurred while deleting the PlanCompany {e.Message}");
            }
        }

        public async Task<PlanCompanyResponse> SaveAsync(PlanCompany planCompany)
        {
            var existingName = await _planCompanyRepository.FindByName(planCompany.Name);
            if (existingName != null)
                return new PlanCompanyResponse("Name: " + planCompany.Name + " is begin used");

            var existingType = await _typeOfCurrentRepository.FindById(planCompany.TypeOfCurrentId);
            if (existingType == null)
                return new PlanCompanyResponse("TypeOfCurrent not found");

            try
            {
                await _planCompanyRepository.AddAsync(planCompany);
                await _unitOfWork.CompleteAsync();
                return new PlanCompanyResponse(planCompany);
            }
            catch(Exception e)
            {
                return new PlanCompanyResponse($"An error ocurred while saving the PlanCompany {e.Message}");
            }
        }

        public async Task<PlanCompanyResponse> UpdateAsync(int id, PlanCompany planCompany)
        {
            var existingPlan = await _planCompanyRepository.FindById(id);
            if (existingPlan == null)
                return new PlanCompanyResponse("PlanCompany not found");

            var existingTypeOfCurrent = await _typeOfCurrentRepository.FindById(planCompany.TypeOfCurrentId);
            if (existingTypeOfCurrent == null)
                return new PlanCompanyResponse("TypeOfCurrent not found");

            if (existingPlan.Name != planCompany.Name)
            {
                var existingName = await _planCompanyRepository.FindByName(planCompany.Name);
                if (existingName != null)
                    return new PlanCompanyResponse("Name: " + planCompany.Name + " is begin used");
            }

            existingPlan.Name = planCompany.Name;
            existingPlan.TypeOfCurrent = existingTypeOfCurrent;
            existingPlan.TypeOfCurrentId = planCompany.TypeOfCurrentId;
            existingPlan.Cost = planCompany.Cost;
            try
            {
                _planCompanyRepository.Update(existingPlan);
                await _unitOfWork.CompleteAsync();
                return new PlanCompanyResponse(existingPlan);
            }
            catch(Exception e)
            {
                return new PlanCompanyResponse($"An error ocurred while updating the PlanCompany {e.Message}");
            }
        }
    }
}
