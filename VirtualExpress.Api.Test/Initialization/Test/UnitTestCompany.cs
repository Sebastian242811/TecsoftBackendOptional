using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Register.Services;

namespace VirtualExpress.Api.Test.Initialization.Test
{
    public class UnitTestCompany
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoCompaniesReturnsEmptyCollection()
        {
            //Arrange
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<CompanyResource>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CompanyService(
                mockCompanyRepository.Object,
                mockUnitOfWork.Object
                );
        
            //Act
            List<CompanyResource> companies = (List<CompanyResource>)await service.ListAsync();
            var companyCount = companies.Count;
        
            //Assert
            companyCount.Should().Equals(0);
        }

        [Test]
        public async Task GetFindByIdWhenNoCompanieReturnCompanyNotFound()
        {
            //Arrange
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<CompanyResource>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CompanyService(
                mockCompanyRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            int id = 1;
            var response = await service.DeleteAsync(id);

            //Assert
            response.Message.Should().Be("Company not found");
        }

        [Test]
        public async Task GetIfTheUsernameIsUsedReturnsThisUsernameIsBeginUsedByAnotherUser()
        {
            //Arrange
            CompanyResource company = new CompanyResource();
            company.Username = "Web";
        
            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.FindByUsername("Web"))
                .ReturnsAsync(company);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CompanyService(
                mockCompanyRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            CompanyResource company2 = new CompanyResource();
            company2.Username = "Web";
        
            var response = await service.SaveAsync(company2);
            //Assert
            response.Message.Should().Be("This username is being used by another company");
        }

        [Test]
        public async Task GetIfTheCustomerIsUsedAnEmailSaveReturnsThisUsernameIsBeginUsedByAnotherUser()
        {
            //Arrange
            CompanyResource company = new CompanyResource();
            company.Email = "web@hotmail.com";

            var mockCompanyRepository = GetDefaultICompanyRepositoryInstance();
            mockCompanyRepository.Setup(r => r.FindByEmail("web@hotmail.com"))
                .ReturnsAsync(company);
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CompanyService(
                mockCompanyRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            CompanyResource company2 = new CompanyResource();
            company2.Email = "web@hotmail.com";
        
            var response = await service.SaveAsync(company2);
            //Assert
            response.Message.Should().Be("This email is being used by another company");
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
