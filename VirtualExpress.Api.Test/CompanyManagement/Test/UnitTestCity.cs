using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Services;
using VirtualExpress.General.Domain.Repositories;

namespace VirtualExpress.Api.Test.CompanyManagement.Test
{
    public class UnitTestCity
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoCitiesReturnsEmptyCollection()
        {
            //Arrange
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<City>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CityService(
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<City> cities = (List<City>)await service.ListAsync();
            var cityCount = cities.Count;

            //Assert
            cityCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageWhenACityNotExistReturnCityNotFound()
        {
            //Arrange
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<City>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CityService(
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.FindCityById(1);

            //Assert
            response.Message.Should().Be("City not found");
        }

        [Test]
        public async Task GetSuccesTrueWhenACityIsCreatedSuccessfullyReturnTrue()
        {
            //Arrange
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<City>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CityService(
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            City city = new City();
            city.Id = 1;
            city.Name = "Lima";
            var response = await service.SaveAsync(city);

            //Assert
            response.Sucess.Should().BeTrue();
        }

        private Mock<ICityRepository> GetDefaultICityRepositoryInstance()
        {
            return new Mock<ICityRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
