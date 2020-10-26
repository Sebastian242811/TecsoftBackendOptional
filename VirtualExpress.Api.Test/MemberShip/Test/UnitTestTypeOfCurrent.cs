using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Repositories;
using VirtualExpress.MemberShip.Services;

namespace VirtualExpress.Api.Test.MemberShip.Test
{
    class UnitTestTypeOfCurrent
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoTypeOfCurrentsReturnsEmptyCollection()
        {
            //Arrange
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<TypeOfCurrent>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TypeOfCurrentService(
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<TypeOfCurrent> typeOfCurrents = (List<TypeOfCurrent>)await service.GetAllAsync();
            var typeOfCurrentCount = typeOfCurrents.Count;

            //Assert
            typeOfCurrentCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageWhenTypeOfCurrentByIdNotExistReturnTypeOfCurrentNotFound()
        {
            //Arrange
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<TypeOfCurrent>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TypeOfCurrentService(
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.FindById(1);

            //Assert
            response.Message.Should().Equals("TypeOfCurrent not found");
        }

        [Test]
        public async Task GetMessageWhenSaveTypeOfCurrentWithNameUsedReturnNameIsBeginUsedInOtherTypeOfCurrent()
        {
            //Arrange
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 1;
            type.Name = "Soles";
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<TypeOfCurrent>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TypeOfCurrentService(
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            TypeOfCurrent type2 = new TypeOfCurrent();
            type2.Id = 2;
            type2.Name = "Soles";
            var response = await service.SaveAsync(type2);

            //Assert
            response.Message.Should().Equals("Name is begin used in other TypeOfCurrent");
        }

        [Test]
        public async Task GetSuccesTrueWhenCreateTypeOfCurrentSuccesfulReturnTrue()
        {
            //Arrange
            var mockTypeOfCurrentRepository = GetDefaultITypeOfCurrentRepositoryInstance();
            mockTypeOfCurrentRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<TypeOfCurrent>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TypeOfCurrentService(
                mockTypeOfCurrentRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            TypeOfCurrent type = new TypeOfCurrent();
            type.Id = 2;
            type.Name = "Soles";
            var response = await service.SaveAsync(type);

            //Assert
            response.Message.Should().Equals("TypeOfCurrent not found");
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
