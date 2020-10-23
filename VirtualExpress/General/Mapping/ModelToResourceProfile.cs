using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Resources;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Resource;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Resources;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Resources;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Resources;

namespace VirtualExpress.General.Mapping
{
    public class ModelToResourceProfile:Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Package, PackageResource>();
            CreateMap<Freight, FreightResource>();
            CreateMap<Dispatcher, DispatcherResource>();
            CreateMap<Commentary, CommentaryResource>();
            CreateMap<Delivery, DeliveryResource>();
            CreateMap<PackageDelivery, PackageDeliveryResource>();
            CreateMap<Terminal, TerminalResource>();
            CreateMap<City, CityResource>();

            //Initialization
            CreateMap<Customer, CustomerResource>();
            CreateMap<Company, CompanyResource>();
            CreateMap<Employee, EmployeeResource>();
        }
    }
}
