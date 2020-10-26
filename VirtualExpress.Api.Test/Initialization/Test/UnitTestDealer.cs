using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Register.Services;

namespace VirtualExpress.Api.Test.Initialization.Test
{
    public class UnitTestDealer
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoDealersReturnsEmptyCollection()
        {
            //Arrange
            var mockDealerRepository = GetDefaultIDealerRepositoryInstance();
            mockDealerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Dealer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCity = GetDefaultICityRepositoryInstance();
            var service = new DealerService(
                mockDealerRepository.Object,
                mockCity.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<Dealer> dealers = (List<Dealer>)await service.ListAsync();
            var dealerCount = dealers.Count;

            //Assert
            dealerCount.Should().Equals(0);
        }

        [Test]
        public async Task GetIfWhenCreatingTheCustomerThereIsNoCityIdThenReturnsCityNotFound()
        {
            //Arrange
            var mockDealerRepository = GetDefaultIDealerRepositoryInstance();
            mockDealerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Dealer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCity = GetDefaultICityRepositoryInstance();
            var service = new DealerService(
                mockDealerRepository.Object,
                mockCity.Object,
                mockUnitOfWork.Object
                );
            //Act
            Dealer dealer = new Dealer();
            dealer.CityId = 1;
            var response = await service.SaveAsync(dealer);

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

            var mockDealerRepository = GetDefaultIDealerRepositoryInstance();
            mockDealerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Dealer>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(city);
            var service = new DealerService(
                mockDealerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );
            //Act            
            Dealer customer = new Dealer();
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
            Dealer dealer = new Dealer();
            dealer.CityId = 1;
            dealer.Username = "Web";

            City city = new City();
            city.Id = 1;
            city.Name = "Lima";

            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(city);
            var mockDealerRepository = GetDefaultIDealerRepositoryInstance();
            mockDealerRepository.Setup(r => r.FindByUsername("Web"))
                .ReturnsAsync(dealer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new DealerService(
                mockDealerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            Dealer dealer2 = new Dealer();
            dealer2.CityId = 1;
            dealer2.Username = "Web";

            var response = await service.SaveAsync(dealer2);
            //Assert
            response.Message.Should().Be("This username is being used by another dealer");
        }

        [Test]
        public async Task GetIfTheCustomerIsUsedAnEmailSaveReturnsThisUsernameIsBeginUsedByAnotherUser()
        {
            //Arrange
            Dealer dealer = new Dealer();
            dealer.CityId = 1;
            dealer.Name = "Web";
            dealer.Email = "web@hotmail.com";

            City city = new City();
            city.Id = 1;
            city.Name = "Lima";

            var mockCityRepository = GetDefaultICityRepositoryInstance();
            mockCityRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(city);
            var mockDealerRepository = GetDefaultIDealerRepositoryInstance();
            mockDealerRepository.Setup(r => r.FindByEmail("web@hotmail.com"))
                .ReturnsAsync(dealer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var service = new DealerService(
                mockDealerRepository.Object,
                mockCityRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            Dealer dealer2 = new Dealer();
            dealer2.CityId = 1;
            dealer2.Username = "Web2";
            dealer2.Email = "web@hotmail.com";

            var response = await service.SaveAsync(dealer2);
            //Assert
            response.Message.Should().Be("This email is being used by another dealer");
        }

        private Mock<IDealerRepository> GetDefaultIDealerRepositoryInstance()
        {
            return new Mock<IDealerRepository>();
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
