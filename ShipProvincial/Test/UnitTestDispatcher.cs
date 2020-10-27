using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
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
            var service = new DispatcherService(
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
            var service = new DispatcherService(
                mockDispatcherRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.GetById(1);

            //Assert
            response.Message.Should().Be("Dispatcher not found");
        }

        [Test]
        public async Task GetSuccesTruWhenDispatcherSaveSuccesful()
        {
            //Arrange
            var mockDispatcherRepository = GetDefaultIDispatcherRepositoryInstance();
            mockDispatcherRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Dispatcher>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new DispatcherService(
                mockDispatcherRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.Id = 1;
            var response = await service.SaveAsync(dispatcher);

            //Assert
            response.Sucess.Should().BeTrue();
        }


        private Mock<IDispatcherRepository> GetDefaultIDispatcherRepositoryInstance()
        {
            return new Mock<IDispatcherRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
