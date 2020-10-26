using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Repositories;
using VirtualExpress.MemberShip.Services;

namespace VirtualExpress.Api.Test.MemberShip.Test
{
    class UnitTestPlanCustomer
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public async Task GetAllAsyncWhenNoCompaniesReturnsEmptyCollection()
        {
            //Arrange
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCustomer>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCustomerService(
                mockPlanCustomerRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<PlanCustomer> companies = (List<PlanCustomer>)await service.ListAsync();
            var companyCount = companies.Count;

            //Assert
            companyCount.Should().Equals(0);
        }

        [Test]
        public async Task GetIfTheplanCustomerNotFoundTypeOfCurrentReturnTypeOfCurrentNotFound()
        {
            //Arrange
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCustomer>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCustomerService(
                mockPlanCustomerRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCustomer planCustomer = new PlanCustomer();
            planCustomer.Id = 1;
            planCustomer.Name = "Nivel 1";
            planCustomer.TypeOfCurrentId = 1;
            var response = await service.SaveAsync(planCustomer);

            //Assert
            response.Message.Should().Be("TypeOfCurrent not found");
        }

        [Test]
        public async Task GetSuccessTrueWhenplanCustomerSavedSuccessfully()
        {
            //Arrange
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 1;
            type.Name = "Soles";

            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCustomer>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(type);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCustomerService(
                mockPlanCustomerRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCustomer planCustomer = new PlanCustomer();
            planCustomer.Id = 1;
            planCustomer.Name = "Nivel 1";
            planCustomer.TypeOfCurrentId = 1;
            var response = await service.SaveAsync(planCustomer);

            //Assert
            response.Sucess.Should().BeTrue();
        }

        [Test]
        public async Task GetMessageWhenCompanyIdNotFoundReturnplanCustomerNotFound()
        {
            //Arrange
            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCustomer>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCustomerService(
                mockPlanCustomerRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            var response = await service.FindById(1);

            //Assert
            response.Message.Should().Be("PlanCustomer not found");
        }

        [Test]
        public async Task GetMessageIfExistAnplanCustomerWithSameNameReturnNameIsBeginUsedInOtherplanCustomer()
        {
            //Arrange
            PlanCustomer planCustomer = new PlanCustomer();
            planCustomer.Id = 1;
            planCustomer.Name = "Nivel 1";
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 1;
            type.Name = "Soles";

            var mockPlanCustomerRepository = GetDefaultIPlanCustomerRepositoryInstance();
            mockPlanCustomerRepository.Setup(r => r.FindByName("Nivel 1"))
                .ReturnsAsync(planCustomer);
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(type);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCustomerService(
                mockPlanCustomerRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCustomer planCustomer2 = new PlanCustomer();
            planCustomer2.Id = 1;
            planCustomer2.Name = "Nivel 1";
            planCustomer2.TypeOfCurrentId = 1;
            var response = await service.SaveAsync(planCustomer2);

            //Assert
            response.Message.Should().Be("Name is begin used in other PlanCustomer");
        }

        private Mock<IPlanCustomerRepository> GetDefaultIPlanCustomerRepositoryInstance()
        {
            return new Mock<IPlanCustomerRepository>();
        }

        private Mock<ITypeOfCurrentRepository> GetDefaultITypeOfCurrentRepositoryInstance()
        {
            return new Mock<ITypeOfCurrentRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
