using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Repositories;
using VirtualExpress.MemberShip.Model.Services;
using VirtualExpress.MemberShip.Model.Services.Responses;

namespace VirtualExpress.MemberShip.Services
{
    public class TypeOfCurrentService : ITypeOfCurrentService
    {
        private readonly ITypeOfCurrentRepository _typeOfCurrentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TypeOfCurrentService(ITypeOfCurrentRepository typeOfCurrentRepository, IUnitOfWork unitOfWork)
        {
            _typeOfCurrentRepository = typeOfCurrentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TypeOfCurrentResponse> DeleteAsync(int id)
        {
            var existingType = await _typeOfCurrentRepository.FindById(id);
            if (existingType == null)
                return new TypeOfCurrentResponse("TypeOfCurrent not found");
            try
            {
                _typeOfCurrentRepository.Remove(existingType);
                await _unitOfWork.CompleteAsync();
                return new TypeOfCurrentResponse(existingType);
            }
            catch(Exception e)
            {
                return new TypeOfCurrentResponse($"An error ocurred while deleting the TypeOfCurrent {e.Message}");
            }
        }

        public async Task<TypeOfCurrentResponse> FindById(int id)
        {
            var existingType = await _typeOfCurrentRepository.FindById(id);
            if (existingType == null)
                return new TypeOfCurrentResponse("TypeOfCurrent not found");
            return new TypeOfCurrentResponse(existingType);
        }

        public async Task<IEnumerable<TypeOfCurrent>> GetAllAsync()
        {
            return await _typeOfCurrentRepository.ListAsync();
        }

        public async Task<TypeOfCurrentResponse> SaveAsync(TypeOfCurrent typeOfCurrent)
        {
            var existingName = await _typeOfCurrentRepository.FindByName(typeOfCurrent.Name);
            if (existingName != null)
                return new TypeOfCurrentResponse("Name: " + typeOfCurrent.Name + " is begin used");

            try
            {
                await _typeOfCurrentRepository.AddAsync(typeOfCurrent);
                await _unitOfWork.CompleteAsync();
                return new TypeOfCurrentResponse(typeOfCurrent);
            }
            catch(Exception e)
            {
                return new TypeOfCurrentResponse($"An error ocurred while saving the TypeOfCurrent {e.Message}");
            }
        }

        public async Task<TypeOfCurrentResponse> UpdateAsync(int id, TypeOfCurrent typeOfCurrent)
        {
            var existingType = await _typeOfCurrentRepository.FindById(id);
            if (existingType == null)
                return new TypeOfCurrentResponse("TypeOfCurrent not found");

            if (existingType.Name != typeOfCurrent.Name)
            {
                var existingName = await _typeOfCurrentRepository.FindByName(typeOfCurrent.Name);
                if (existingName != null)
                    return new TypeOfCurrentResponse("Name: " + typeOfCurrent.Name + " is begin used");
            }

            existingType.Name = typeOfCurrent.Name;
            try
            {
                _typeOfCurrentRepository.Update(existingType);
                await _unitOfWork.CompleteAsync();
                return new TypeOfCurrentResponse(existingType);
            }
            catch (Exception e)
            {
                return new TypeOfCurrentResponse($"An error ocurred while deleting the TypeOfCurrent {e.Message}");
            }
        }
    }
}
