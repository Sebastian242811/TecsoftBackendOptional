using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Repositories;
using VirtualExpress.ShipDelivery.Services;

namespace VirtualExpress.Api.Test.ShipDelivery.Test
{
    public class UnitTestPackageDelivery
    {
        [SetUp]
        public void SetUp()
        {
        }

        public async Task GetAllAsyncWhenNoPackageDeliveriesReturnsEmptyCollection()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIPackageDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PackageDelivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PackageDeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<PackageDelivery> deliveries = (List<PackageDelivery>)await service.ListAsync();
            var deliveryCount = deliveries.Count;

            //Assert
            deliveryCount.Should().Equals(0);
        }

        public async Task GetMessageWhenPackageDeliveryNotFoundReturnPackageDeliveryNotFound()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIPackageDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PackageDelivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PackageDeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.GetById(1);

            //Assert
            response.Message.Should().Be("PackageDelivery not found");
        }

        public async Task GetSuccesTrueWhenAPackageDeliveryIsCreatedSuccesfullyReturnTrue()
        {
            //Arrange
            var mockDeliveryRepository = GetDefaultIPackageDeliveryRepositoryInstance();
            mockDeliveryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PackageDelivery>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new PackageDeliveryService(
                mockDeliveryRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            PackageDelivery packageDelivery = new PackageDelivery();
            var response = await service.SaveAsync(packageDelivery);

            //Assert
            response.Sucess.Should().BeTrue();
        }

        private Mock<IPackageDeliveryRepository> GetDefaultIPackageDeliveryRepositoryInstance()
        {
            return new Mock<IPackageDeliveryRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
