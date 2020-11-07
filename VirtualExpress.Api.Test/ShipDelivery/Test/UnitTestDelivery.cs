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
using VirtualExpress.ShipDelivery.Domain.Repositories;
using VirtualExpress.ShipDelivery.Services;

namespace VirtualExpress.Api.Test.ShipDelivery.Test
{
    public class UnitTestDelivery
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoDeliveriesReturnsEmptyCollection()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Delivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockdealer = GetDefaultIDealerRepository();
            var service = new DeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object,
                mockdealer.Object
                );

            //Act
            List<Delivery> deliveries = (List<Delivery>)await service.ListAsync();
            var deliveryCount = deliveries.Count;

            //Assert
            deliveryCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageIfDeliveryNotFoundReturnDeliveryNotFound()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Delivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockdealer = GetDefaultIDealerRepository();
            var service = new DeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object,
                mockdealer.Object
                );

            //Act
            var response = await service.GetById(1);

            //Assert
            response.Message.Should().Be("Delivery not found");
        }

        [Test]
        public async Task GetSuccesTruWhenDeliverySaveSuccesful()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Delivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockdealer = GetDefaultIDealerRepository();
            var service = new DeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object,
                mockdealer.Object
                );

            //Act
            Delivery delivery = new Delivery();
            delivery.Id = 1;
            var response = await service.SaveAsync(delivery);

            //Assert
            response.Sucess.Should().BeTrue();
        }

        private Mock<IDeliveryRepository> GetDefaultIDeliveryRepositoryInstance()
        {
            return new Mock<IDeliveryRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IDealerRepository> GetDefaultIDealerRepository()
        {
            return new Mock<IDealerRepository>();
        }
    }
}
