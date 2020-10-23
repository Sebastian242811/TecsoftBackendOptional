using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;
using VirtualExpress.General.Domain.Repositories;

namespace VirtualExpress.CompanyManagement.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CityResponse> DeleteAsync(int cityId)
        {
            var existingCity = await _cityRepository.FindById(cityId);
            if (existingCity == null)
                return new CityResponse("City not found");

            try
            {
                _cityRepository.Remove(existingCity);
                await _unitOfWork.CompleteAsync();
                return new CityResponse(existingCity);
            }
            catch (Exception e)
            {
                return new CityResponse($"An error ocurred while deleting the city: {e.Message}");
            }
        }

        public async Task<CityResponse> FindCityById(int cityId)
        {
            var existingCity = await _cityRepository.FindById(cityId);
            if (existingCity == null)
                return new CityResponse("City not found");
            return new CityResponse(existingCity);
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _cityRepository.ListAsync();
        }

        public async Task<CityResponse> SaveAsync(City city)
        {
            try
            {
                await _cityRepository.AddAsync(city);
                await _unitOfWork.CompleteAsync();
                return new CityResponse(city);
            }
            catch (Exception e)
            {
                return new CityResponse($"An error ocurred while saving the city: {e.Message}");
            }
        }

        public async Task<CityResponse> UpdateAsync(int cityId, City city)
        {
            var existingCity = await _cityRepository.FindById(cityId);
            if (existingCity == null)
                return new CityResponse("City not found");

            existingCity.Name = city.Name;
            try
            {
                _cityRepository.Update(existingCity);
                await _unitOfWork.CompleteAsync();
                return new CityResponse(existingCity);
            }
            catch (Exception e)
            {
                return new CityResponse($"An error ocurred while updating the city: {e.Message}");
            }
        }
    }
}
