using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Models;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Services;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Services;

namespace VirtualExpress.Api.Test.ShipProvincial.Test
{
    public class UnitTestDispatcher
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoDispatchersReturnsEmptyCollection()
        {
            //Arrange
            var mockDispatcherRepository = GetDefaultIDispatcherRepositoryInstance();
            mockDispatcherRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Dispatcher>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCompanyRepository = GetDefaultICompanyRepository();
            var service = new DispatcherService(
                mockCompanyRepository.Object,
                mockDispatcherRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<Dispatcher> dispatchers = (List<Dispatcher>)await service.ListAsync();
            var dispatcherCount = dispatchers.Count;

            //Assert
            dispatcherCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageIfDispatcherNotFoundReturnDispatcherNotFound()
        {
            //Arrange
            var mockDispatcherRepository = GetDefaultIDispatcherRepositoryInstance();
            mockDispatcherRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Dispatcher>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCompanyRepository = GetDefaultICompanyRepository();
            var service = new DispatcherService(
                mockCompanyRepository.Object,
                mockDispatcherRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.GetById(1);

            //Assert
            response.Message.Should().Be("Dispatcher not found");
        }

        private Mock<IDispatcherRepository> GetDefaultIDispatcherRepositoryInstance()
        {
            return new Mock<IDispatcherRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<ICompanyRepository> GetDefaultICompanyRepository()
        {
            return new Mock<ICompanyRepository>();
        }

    }
}
