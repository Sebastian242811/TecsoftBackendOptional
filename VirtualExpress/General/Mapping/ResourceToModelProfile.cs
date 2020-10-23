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
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePackageResource, Package>();
            CreateMap<SaveFreightResource, Freight>();
            CreateMap<SaveDispatcherResource, Dispatcher>();
            CreateMap<SaveCommentaryResource, Commentary>();
            CreateMap<SaveDeliveryResource, Delivery>();
            CreateMap<SavePackageDeliveryResource, PackageDelivery>();
            CreateMap<SaveTerminalResource, Terminal>();
            CreateMap<SaveCityResource, City>();

            //Initialization
            CreateMap<SaveCustomerResource, Customer>();
            CreateMap<SaveCompanyResource, Company>();
            CreateMap<SaveEmployeeResource, Employee>();

        }
    }
}
