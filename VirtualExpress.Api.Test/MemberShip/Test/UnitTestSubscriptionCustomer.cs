using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Services;

namespace VirtualExpress.Api.Test.MemberShip.Test
{
    class UnitTestSubscriptionCustomer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoSubscriptionCustomerReturnsEmptyCollection()
        {
            //Arrange
            var mockPSubscriptionCustomerRepository = GetDefaultISubscriptionCustomerRepositoryInstance();
            mockPSubscriptionCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCustomer>());
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCustomerService(
                mockPSubscriptionCustomerRepository.Object,
                mockCustomerRepository.Object,
                mockPlanCustomerRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<SubscriptionCustomer> subscriptionCompanies = (List<SubscriptionCustomer>)await service.ListAsync();
            var subscriptionCustomerCount = subscriptionCompanies.Count;

            //Assert
            subscriptionCustomerCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageWhenSubscriptionCustomerNotExistsReturnSubscriptionCustomerNotFound()
        {
            //Arrange
            var mockPSubscriptionCustomerRepository = GetDefaultISubscriptionCustomerRepositoryInstance();
            mockPSubscriptionCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCustomer>());
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCustomerService(
                mockPSubscriptionCustomerRepository.Object,
                mockCustomerRepository.Object,
                mockPlanCustomerRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.FindById(1);

            //Assert
            response.Message.Should().Be("SubscriptionCustomer not found");
        }

        [Test]
        public async Task GetMessageWhenSaveSubscriptionCustomerWitoutCustomerReturnCustomerNotFound()
        {
            //Arrange
            PlanCustomer planCustomer = new PlanCustomer();
            planCustomer.Id = 1;
            planCustomer.Name = "Nivel 1";
            var mockPSubscriptionCustomerRepository = GetDefaultISubscriptionCustomerRepositoryInstance();
            mockPSubscriptionCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCustomer>());
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(planCustomer);
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCustomerService(
                mockPSubscriptionCustomerRepository.Object,
                mockCustomerRepository.Object,
                mockPlanCustomerRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCustomer subscriptionCustomer = new SubscriptionCustomer();
            subscriptionCustomer.PlanId = 1;
            subscriptionCustomer.CustomerId = 1;
            var response = await service.SaveAsync(subscriptionCustomer);

            //Assert
            response.Message.Should().Be("Customer not found");
        }

        [Test]
        public async Task GetMessageWhenSaveSubscriptionCustomerWitoutPlanReturnCustomerNotFound()
        {
            //Arrange
            Customer Customer = new Customer();
            Customer.Id = 1;
            Customer.Name = "Nivel 1";
            var mockPSubscriptionCustomerRepository = GetDefaultISubscriptionCustomerRepositoryInstance();
            mockPSubscriptionCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCustomer>());
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(Customer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCustomerService(
                mockPSubscriptionCustomerRepository.Object,
                mockCustomerRepository.Object,
                mockPlanCustomerRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCustomer subscriptionCustomer = new SubscriptionCustomer();
            subscriptionCustomer.PlanId = 1;
            subscriptionCustomer.CustomerId = 1;
            var response = await service.SaveAsync(subscriptionCustomer);

            //Assert
            response.Message.Should().Be("PlanCustomer not found");
        }

        [Test]
        public async Task GetSuccesTruWhenSaveSubscriptionCustomerSuccesfulReturnCustomerNotFound()
        {
            //Arrange
            Customer Customer = new Customer();
            Customer.Id = 1;
            Customer.Name = "Nivel 1";
            PlanCustomer planCustomer = new PlanCustomer();
            planCustomer.Id = 1;
            planCustomer.Name = "Nivel 1";
            var mockPSubscriptionCustomerRepository = GetDefaultISubscriptionCustomerRepositoryInstance();
            mockPSubscriptionCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCustomer>());
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(planCustomer);
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(Customer);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCustomerService(
                mockPSubscriptionCustomerRepository.Object,
                mockCustomerRepository.Object,
                mockPlanCustomerRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCustomer subscriptionCustomer = new SubscriptionCustomer();
            subscriptionCustomer.PlanId = 1;
            subscriptionCustomer.CustomerId = 1;
            var response = await service.SaveAsync(subscriptionCustomer);

            //Assert
            response.Sucess.Should().BeTrue();
        }


        private Mock<ISubscriptionCustomerRepository> GetDefaultISubscriptionCustomerRepositoryInstance()
        {
            return new Mock<ISubscriptionCustomerRepository>();
        }

        private Mock<IPlanCustomerRepository> GetDefaultIPlanCustomerRepositoryInstance()
        {
            return new Mock<IPlanCustomerRepository>();
        }

        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
