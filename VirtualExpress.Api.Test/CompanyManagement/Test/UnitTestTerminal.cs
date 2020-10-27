using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Services;
using VirtualExpress.General.Domain.Repositories;

namespace VirtualExpress.Api.Test.CompanyManagement.Test
{
    public class UnitTestTerminal
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoTerminalsReturnsEmptyCollection()
        {
            //Arrange
            var mockTerminalRepository = GetDefaultITerminalRepositoryInstance();
            mockTerminalRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Terminal>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TerminalService(
                mockTerminalRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            List<Terminal> terminals = (List<Terminal>)await service.ListAsync();
            var terminalCount = terminals.Count;

            //Assert
            terminalCount.Should().Equals(0);
        }

        [Test]
        public async Task GetMessageWhenTerminalNotExistReturnTerminalNotFound()
        {
            //Arrange
            var mockTerminalRepository = GetDefaultITerminalRepositoryInstance();
            mockTerminalRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Terminal>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TerminalService(
                mockTerminalRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            var response = await service.GetByIdAsync(1);

            //Assert
            response.Message.Should().Be("Terminal not found");
        }

        [Test]
        public async Task GetMessageWhenATerminalIsCreatedSuccesfulleReturnTrue()
        {
            //Arrange
            var mockTerminalRepository = GetDefaultITerminalRepositoryInstance();
            mockTerminalRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Terminal>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new TerminalService(
                mockTerminalRepository.Object,
                mockUnitOfWork.Object
                );

            //Act
            Terminal terminal = new Terminal();
            terminal.Id = 1;
            terminal.Name = "Panamericana";
            var response = await service.SaveAssync(terminal);

            //Assert
            response.Sucess.Should().BeTrue();
        }


        private Mock<ITerminalRepository> GetDefaultITerminalRepositoryInstance()
        {
            return new Mock<ITerminalRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
