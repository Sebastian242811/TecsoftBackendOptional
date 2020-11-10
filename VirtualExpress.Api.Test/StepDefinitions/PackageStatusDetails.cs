using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Ubiety.Dns.Core;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Register.Services;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;
using VirtualExpress.ShipProvincial.Persistance.Repositories;
using VirtualExpress.ShipProvincial.Services;

namespace OffiRent.API.Test.StepDefinitions
{

    [Binding]
    public class PackageDetailsSteps
    {
        PackageResponse response;
        private readonly IPackageService _packageService;
        private readonly ICustomerService _customerService;

        private readonly Mock<PackageRepository> _packageRepositoryMock = new Mock<PackageRepository>();
        private readonly Mock<IFreightRepository> _freightRepositoryMock = new Mock<IFreightRepository>();
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new Mock<ICustomerRepository>();
        private readonly Mock<IShipTerminalRepository> _shipTemrinalRepositoryMock = new Mock<IShipTerminalRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDispatcherRepository> _dispatcherMock = new Mock<IDispatcherRepository>();
        private readonly Mock<ICityRepository> _cityMock = new Mock<ICityRepository>();
        
        EState message = EState.Waiting;
        Package package = new Package();
        int packageId = 100;

        Customer customer = new Customer();
        int customerId = 100;
        
        public PackageDetailsSteps()
        {
            _packageService = new PackageService(_freightRepositoryMock.Object, _dispatcherMock.Object, _customerRepositoryMock.Object, _packageRepositoryMock.Object, _unitOfWorkMock.Object, _shipTemrinalRepositoryMock.Object);
            _packageRepositoryMock.Setup(a => a.FindById(packageId)).ReturnsAsync(package);

            _customerService = new CustomerService(_customerRepositoryMock.Object, _cityMock.Object, _unitOfWorkMock.Object);
            _customerRepositoryMock.Setup(a => a.FindById(customerId)).ReturnsAsync(customer);
        }
       

        [Given(@" a verified user")]
        public void GivenAVerifiedUser()
        {
            Assert.NotNull(_customerService.FindCustomerById(customerId));
        }

        [When(@"the user clicks on package")]
        public void WhenTheUserClicksOnHisProfileIcon()
        {
            response = _packageService.GetById(packageId).Result;
        }

        [Then(@"the system will show his status of package")]
        public void ThenTheSystemWillShowHisPersonalInformation()
        {
            response.Resource.State = (EState)1;
            Assert.AreEqual((int)response.Resource.State, message);
        }
    }
}
