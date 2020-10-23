using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Persistence.Repositories;
using VirtualExpress.CompanyManagement.Resources;
using VirtualExpress.CompanyManagement.Services;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.General.Extensions;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Persistence.Repositories;
using VirtualExpress.Register.Persistence.Repositories;
using VirtualExpress.Register.Services;

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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("transport-api-in-memory");
            });

            //Repositorie
                //Initialization
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //CompanyManagement
            services.AddScoped<ICityRepository, CityRepository>();


            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
                //Initialization
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            //CompanyManagement
            services.AddScoped<ICityService, CityService>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UserCustomSwagger();
        }
    }
}
