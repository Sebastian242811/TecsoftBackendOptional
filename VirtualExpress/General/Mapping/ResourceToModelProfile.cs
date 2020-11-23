﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Resources;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Resources;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Resource;
using VirtualExpress.Initialization.Resources;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Resource;
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
            CreateMap<UpdateStateResource, Package>();
            CreateMap<SaveFreightResource, Freight>();
            CreateMap<SaveDispatcherResource, Dispatcher>();
            CreateMap<SaveCommentaryResource, Commentary>();
            CreateMap<SaveDeliveryResource, Delivery>();
            CreateMap<SavePackageDeliveryResource, PackageDelivery>();
            CreateMap<SaveTerminalResource, Terminal>();
            CreateMap<SaveCityResource, City>();
            CreateMap<SaveShipTerminalResource, ShipTerminal>();
            CreateMap<SaveChangeStateResource, ChangeState>();

            //Initialization
            CreateMap<SaveCustomerResource, Customer>();
            CreateMap<SaveCompanyResource, Company>();
            CreateMap<SaveDealerResource, Dealer>();

            //MemberShip
            CreateMap<SavePlanCompanyResource, PlanCompany>();
            CreateMap<SavePlanCustomerResource, PlanCustomer>();
            CreateMap<SaveSubscriptionCompanyResource, SubscriptionCompany>();
            CreateMap<SaveSubscriptionCustomerResource, SubscriptionCustomer>();
            CreateMap<SaveTypeOfCurrentResource, TypeOfCurrent>();

            CreateMap<SaveChatResource, Chat>();
            CreateMap<SaveMessageResource, Message>();
        }
    }
}
