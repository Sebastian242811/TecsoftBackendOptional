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
    class UnitTestSubscriptionCompany
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoSubscriptionCompaniesReturnsEmptyCollection()
        {
            //Arrange
            var mockPSubscriptionCompanyRepository = GetDefaultISubscriptionCompanyRepositoryInstance();
            mockPSubscriptionCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCompany>());
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCompanyService(
                mockPSubscriptionCompanyRepository.Object,
                mockCompanyRepository.Object,
                mockPlanCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<SubscriptionCompany> subscriptionCompanies = (List<SubscriptionCompany>)await service.ListAsync();
            var subscriptionCompanyCount = subscriptionCompanies.Count;

            //Assert
            subscriptionCompanyCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageWhenSubscriptionCompanyNotExistsReturnSubscriptionCompanyNotFound()
        {
            //Arrange
            var mockPSubscriptionCompanyRepository = GetDefaultISubscriptionCompanyRepositoryInstance();
            mockPSubscriptionCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCompany>());
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCompanyService(
                mockPSubscriptionCompanyRepository.Object,
                mockCompanyRepository.Object,
                mockPlanCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.FindById(1);

            //Assert
            response.Message.Should().Be("SubscriptionCompany not found");
        }

        [Test]
        public async Task GetMessageWhenSaveSubscriptionCompanyWitoutCompanyReturnCompanyNotFound()
        {
            //Arrange
            PlanCompany planCompany = new PlanCompany();
            planCompany.Id = 1;
            planCompany.Name = "Nivel 1";
            var mockPSubscriptionCompanyRepository = GetDefaultISubscriptionCompanyRepositoryInstance();
            mockPSubscriptionCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCompany>());
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(planCompany);
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCompanyService(
                mockPSubscriptionCompanyRepository.Object,
                mockCompanyRepository.Object,
                mockPlanCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCompany subscriptionCompany = new SubscriptionCompany();
            subscriptionCompany.PlanId = 1;
            subscriptionCompany.CompanyId = 1;
            var response = await service.SaveAsync(subscriptionCompany);

            //Assert
            response.Message.Should().Be("Company not found");
        }

        [Test]
        public async Task GetMessageWhenSaveSubscriptionCompanyWitoutPlanReturnCompanyNotFound()
        {
            //Arrange
            CompanyResource company = new CompanyResource();
            company.Id = 1;
            company.Name = "Nivel 1";
            var mockPSubscriptionCompanyRepository = GetDefaultISubscriptionCompanyRepositoryInstance();
            mockPSubscriptionCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCompany>());
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.FindCompanyById(1))
                .ReturnsAsync(company);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCompanyService(
                mockPSubscriptionCompanyRepository.Object,
                mockCompanyRepository.Object,
                mockPlanCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCompany subscriptionCompany = new SubscriptionCompany();
            subscriptionCompany.PlanId = 1;
            subscriptionCompany.CompanyId = 1;
            var response = await service.SaveAsync(subscriptionCompany);

            //Assert
            response.Message.Should().Be("PlanCompany not found");
        }

        [Test]
        public async Task GetSuccesTruWhenSaveSubscriptionCompanySuccesfulReturnCompanyNotFound()
        {
            //Arrange
            CompanyResource company = new CompanyResource();
            company.Id = 1;
            company.Name = "Nivel 1";
            PlanCompany planCompany = new PlanCompany();
            planCompany.Id = 1;
            planCompany.Name = "Nivel 1";
            var mockPSubscriptionCompanyRepository = GetDefaultISubscriptionCompanyRepositoryInstance();
            mockPSubscriptionCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SubscriptionCompany>());
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(planCompany);
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.FindCompanyById(1))
                .ReturnsAsync(company);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionCompanyService(
                mockPSubscriptionCompanyRepository.Object,
                mockCompanyRepository.Object,
                mockPlanCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            SubscriptionCompany subscriptionCompany = new SubscriptionCompany();
            subscriptionCompany.PlanId = 1;
            subscriptionCompany.CompanyId = 1;
            var response = await service.SaveAsync(subscriptionCompany);

            //Assert
            response.Sucess.Should().BeTrue();
        }


        private Mock<ISubscriptionCompanyRepository> GetDefaultISubscriptionCompanyRepositoryInstance()
        {
            return new Mock<ISubscriptionCompanyRepository>();
        }

        private Mock<IPlanCompanyRepository> GetDefaultIPlanCompanyRepositoryInstance()
        {
            return new Mock<IPlanCompanyRepository>();
        }

        private Mock<ICompanyRepository> GetDefaultICompanyRepositoryInstance()
        {
            return new Mock<ICompanyRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
