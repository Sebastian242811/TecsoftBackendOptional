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
    class UnitTestPlanCompany
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoCompaniesReturnsEmptyCollection()
        {
            //Arrange
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCompany>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCompanyService(
                mockPlanCompanyRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<PlanCompany> companies = (List<PlanCompany>)await service.ListAsync();
            var companyCount = companies.Count;

            //Assert
            companyCount.Should().Equals(0);
        }

        [Test]
        public async Task GetIfThePlanCompanyNotFoundTypeOfCurrentReturnTypeOfCurrentNotFound()
        {
            //Arrange
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCompany>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCompanyService(
                mockPlanCompanyRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCompany planCompany = new PlanCompany();
            planCompany.Id = 1;
            planCompany.Name = "Nivel 1";
            planCompany.TypeOfCurrentId = 1;
            var response =await service.SaveAsync(planCompany);

            //Assert
            response.Message.Should().Be("TypeOfCurrent not found");
        }

        [Test]
        public async Task GetSuccessTrueWhenPlanCompanySavedSuccessfully()
        {
            //Arrange
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 1;
            type.Name = "Soles";

            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCompany>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(type);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCompanyService(
                mockPlanCompanyRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCompany planCompany = new PlanCompany();
            planCompany.Id = 1;
            planCompany.Name = "Nivel 1";
            planCompany.TypeOfCurrentId = 1;
            var response = await service.SaveAsync(planCompany);

            //Assert
            response.Sucess.Should().BeTrue();
        }

        [Test]
        public async Task GetMessageWhenCompanyIdNotFoundReturnPlanCompanyNotFound()
        {
            //Arrange
            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlanCompany>());
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCompanyService(
                mockPlanCompanyRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            var response = await service.FindById(1);

            //Assert
            response.Message.Should().Be("PlanCompany not found");
        }

        [Test]
        public async Task GetMessageIfExistAnPlanCompanyWithSameNameReturnNameIsBeginUsedInOtherPlanCompany()
        {
            //Arrange
            PlanCompany planCompany = new PlanCompany();
            planCompany.Id = 1;
            planCompany.Name = "Nivel 1";
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 1;
            type.Name = "Soles";

            var mockPlanCompanyRepository = GetDefaultIPlanCompanyRepositoryInstance();
            mockPlanCompanyRepository.Setup(r => r.FindByName("Nivel 1"))
                .ReturnsAsync(planCompany);
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(type);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PlanCompanyService(
                mockPlanCompanyRepository.Object,
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            PlanCompany planCompany2 = new PlanCompany();
            planCompany2.Id = 1;
            planCompany2.Name = "Nivel 1";
            planCompany2.TypeOfCurrentId = 1;
            var response = await service.SaveAsync(planCompany2);

            //Assert
            response.Message.Should().Be("Name is begin used in other PlanCompany");
        }

        private Mock<IPlanCompanyRepository> GetDefaultIPlanCompanyRepositoryInstance()
        {
            return new Mock<IPlanCompanyRepository>();
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
