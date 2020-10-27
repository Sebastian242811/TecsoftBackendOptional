using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Services;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services.Responses;
using VirtualExpress.Register.Services;

namespace VirtualExpress.Api.Test.Initialization.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoCustomersReturnsEmptyCollection()
        {
            //Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Customer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCity = GetDefaultICityRepositoryInstance();
            var service = new CustomerService(
                mockCustomerRepository.Object,
                mockCity.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<Customer> customers = (List<Customer>)await service.ListAsync();
            var customerCount = customers.Count;

            //Assert
            customerCount.Should().Equals(0);
        }

        [Test]
        public async Task GetIfWhenCreatingTheCustomerThereIsNoCityIdThenReturnsCityNotFound()
        {
            //Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Customer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCity = GetDefaultICityRepositoryInstance();
            var service = new CustomerService(
                mockCustomerRepository.Object,
                mockCity.Object,
                mockUnitOfWork.Object
                );
            //Act
            Customer customer = new Customer();
            customer.CityId = 1;
            var response = await service.SaveAsync(customer);

            //Assert
            response.Message.Should().Be("City not found");
        }

        [Test]
        public async Task GetSucessTrueWhenCustomerCreateAnAccountWithCityIdExistingReturnsTrue()
        {
            //Arrange
            City city = new City();
            city.Id = 1;
            city.Name = "Lima";

            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Customer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(city);
            var service = new CustomerService(
                mockCustomerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );
            //Act            
            Customer customer = new Customer();
            customer.CityId = 1;
            customer.City = city;            
            var response = await service.SaveAsync(customer);
            //Assert
            response.Sucess.Should().BeTrue();
        }

        [Test]
        public async Task GetIfTheUsernameIsUsedReturnsThisUsernameIsBeginUsedByAnotherUser()
        {
            //Arrange
            Customer customer = new Customer();
            customer.CityId = 1;
            customer.Username = "Web";
        
            City city = new City();
            city.Id = 1;
            city.Name = "Lima";
        
           var mockCityRepository = GetDefaultICityRepositoryInstance();
           mockCityRepository.Setup(r => r.FindById(1))
               .ReturnsAsync(city);
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.FindByUsername("Web"))
                .ReturnsAsync(customer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
        
            var service = new CustomerService(
                mockCustomerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );
        
            //Act
            Customer customer2 = new Customer();
            customer2.CityId = 1;
            customer2.Username = "Web";
        
            var response = await service.SaveAsync(customer2);
            //Assert
            response.Message.Should().Be("This username is being used by another user");
        }

        [Test]
        public async Task GetIfTheCustomerIsUsedAnEmailSaveReturnsThisUsernameIsBeginUsedByAnotherUser()
        {
            //Arrange
            Customer customer = new Customer();
            customer.CityId = 1;
            customer.Name = "Web";
            customer.Email = "web@hotmail.com";
        
            City city = new City();
            city.Id = 1;
            city.Name = "Lima";
        
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(city);
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.FindByEmail("web@hotmail.com"))
                .ReturnsAsync(customer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
        
            var service = new CustomerService(
                mockCustomerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );
        
            //Act
            Customer customer2 = new Customer();
            customer2.CityId = 1;
            customer2.Username = "Web2";
            customer2.Email = "web@hotmail.com";
        
            var response = await service.SaveAsync(customer2);
            //Assert
            response.Message.Should().Be("This email is being used by another user");
        }
              

        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
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