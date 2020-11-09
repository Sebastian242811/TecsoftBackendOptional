using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Persistence.Repositories;
using VirtualExpress.CompanyManagement.Services;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.General.Extensions;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Persistance.Repositories;
using VirtualExpress.Initialization.Persistence.Repositories;
using VirtualExpress.Initialization.Services;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Repositories;
using VirtualExpress.MemberShip.Domain.Services;
using VirtualExpress.MemberShip.Model.Repositories;
using VirtualExpress.MemberShip.Model.Services;
using VirtualExpress.MemberShip.Persistence.Repositories;
using VirtualExpress.MemberShip.Services;
using VirtualExpress.Register.Persistence.Repositories;
using VirtualExpress.Register.Services;
using VirtualExpress.ShipDelivery.Domain.Repositories;
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Persistance.Repositories;
using VirtualExpress.ShipDelivery.Services;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Persistance.Repositories;
using VirtualExpress.ShipProvincial.Services;
using VirtualExpress.Social.Domain.Repositories;
using VirtualExpress.Social.Domain.Services;
using VirtualExpress.Social.Persistance.Repositories;
using VirtualExpress.Social.Services;

namespace VirtualExpress
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin()));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
                //options.UseInMemoryDatabase("transport-api-in-memory");
            });

            //Repositorie
                //Initialization
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
                //CompanyManagement
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ITerminalRepository, TerminalRepository>();
            services.AddScoped<IShipTerminalRepository,ShipTerminalRepository>();
                //ShipDelivery
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IPackageDeliveryRepository, PackageDeliveryRepository>();
                //ShipProvincial
            services.AddScoped<IDispatcherRepository, DispatcherRepository>();
            services.AddScoped<IFreightRepository, FreightRepository>();
            services.AddScoped<IPackageRepository,PackageRepository>();
            services.AddScoped<IChangeStateRepository, ChangeStateRepository>();
                //Social
            services.AddScoped<ICommentaryRepository, CommentaryRepository>();
                //MemberShip
            services.AddScoped<IPlanCompanyRepository, PlanCompanyRepository>();
            services.AddScoped<IPlanCustomerRepository, PlanCustomerRepository>();
            services.AddScoped<ISubscriptionCompanyRepository, SubscriptionCompanyRepository>();
            services.AddScoped<ISubscriptionCustomerRepository, SubscriptionCustomerRepository>();
            services.AddScoped<ITypeOfCurrentRepository, TypeOfCurrentRepository>();
                //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
                //Initialization
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDealerService, DealerService>();
                //CompanyManagement
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IShipTerminalService, ShipTerminalService>();
                //shipDelivery
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IPackageDeliveryService, PackageDeliveryService>();
                //ShipProvincial
            services.AddScoped<IDispatcherService, DispatcherService>();
            services.AddScoped<IFreightService, FreightService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IChangeStateService, ChangeStateService>();
                //Social
            services.AddScoped<ICommentaryService, CommentaryService>();
                //MemberShip
            services.AddScoped<IPlanCompanyService, PlanCompanyService>();
            services.AddScoped<IPlanCustomerService, PlanCustomerService>();
            services.AddScoped<ISubscriptionCompanyService, SubscriptionCompanyService>();
            services.AddScoped<ISubscriptionCustomerService, SubscriptionCustomerService>();
            services.AddScoped<ITypeOfCurrentService, TypeOfCurrentService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddCustomSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UserCustomSwagger();
        }
    }
}
