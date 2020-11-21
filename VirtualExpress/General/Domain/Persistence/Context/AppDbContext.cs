using Microsoft.EntityFrameworkCore;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.General.Extensions;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Domain.Model;
using Microsoft.EntityFrameworkCore.Internal;
using VirtualExpress.Communication.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace VirtualExpress.General.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<PackageDelivery> PackageDeliveries { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<Freight> Freights { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Commentary> Comentaries { get; set; }
        public DbSet<ShipTerminal> ShipTerminals { get; set; }
        public DbSet<ChangeState> ChangesStates { get; set; }

        //Initialization
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dealer> Dealers { get; set; }

        //MemberShip
        public DbSet<TypeOfCurrent> TypeOfCurrents { get; set; }
        public DbSet<PlanCompany> PlanCompanies { get; set; }
        public DbSet<PlanCustomer> PlanCustomers { get; set; }
        public DbSet<SubscriptionCustomer> SubscriptionCustomers { get; set; }
        public DbSet<SubscriptionCompany> SubscriptionCompanies { get; set; }

        //Communication
        public DbSet<CustomerServiceEmployee> CustomerServiceEmployees { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().ToTable("Cities");
            builder.Entity<City>().HasKey(k => k.Id);
            builder.Entity<City>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<City>()
                .HasMany(p => p.Customers)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId);
            builder.Entity<City>()
                .HasMany(p => p.Dealers)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId);


            builder.Entity<Terminal>().ToTable("Terminals");
            builder.Entity<Terminal>().HasKey(k => k.Id);
            builder.Entity<Terminal>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Terminal>().Property(p => p.Name)
                .IsRequired().HasMaxLength(25);
            builder.Entity<Terminal>().Property(p => p.Adress)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Terminal>().HasOne(p => p.City)
                .WithMany(p => p.Terminals).HasForeignKey(p => p.CityId);
            builder.Entity<Terminal>().HasOne(p => p.Company)
                .WithMany(p => p.Terminals).HasForeignKey(p => p.CompanyId);

            builder.Entity<Delivery>().ToTable("Deliveries");
            builder.Entity<Delivery>().HasKey(k => k.Id);
            builder.Entity<Delivery>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Delivery>().Property(p => p.Arrival)
                .IsRequired().HasMaxLength(25);
            builder.Entity<Delivery>().Property(p => p.Price)
                .IsRequired().HasMaxLength(25);
            builder.Entity<Delivery>().HasOne(p => p.Dealer)
                .WithMany(p => p.Deliveries).HasForeignKey(p=>p.DealerId);

            builder.Entity<PackageDelivery>().ToTable("PackageDeliveries");
            builder.Entity<PackageDelivery>().HasKey(k => k.Id);
            builder.Entity<PackageDelivery>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PackageDelivery>().HasOne(p => p.Delivery)
                .WithMany(p => p.PackageDeliveries).HasForeignKey(p => p.DeliveryId);
            //builder.Entity<PackageDelivery>().HasOne(p => p.Package)
            //    .WithMany(p => p.PackageDeliveries).HasForeignKey(p => p.PackageId);

            builder.Entity<Dispatcher>().ToTable("Dispatchers");
            builder.Entity<Dispatcher>().HasKey(Key => Key.Id);
            builder.Entity<Dispatcher>().Property(Key => Key.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Dispatcher>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Dispatcher>().Property(p => p.DNI).HasMaxLength(8);
            builder.Entity<Dispatcher>().HasOne(p => p.Company)
                .WithMany(p => p.Dispatchers).HasForeignKey(p => p.CompanyId);


            builder.Entity<Freight>().ToTable("Freights");
            builder.Entity<Freight>().HasKey(prop => prop.Id);
            builder.Entity<Freight>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Freight>().Property(p => p.Observations)
                .HasMaxLength(50);


            builder.Entity<Package>().ToTable("Packages");
            builder.Entity<Package>().HasKey(p => p.Id);
            builder.Entity<Package>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Package>().Property(p => p.Description)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Package>().Property(p => p.Observations)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Package>().Property(p => p.Discount)
                .IsRequired();
            builder.Entity<Package>().Property(p => p.Weight)
                .IsRequired().HasMaxLength(5);
            builder.Entity<Package>().Property(p => p.Priority).HasDefaultValue(EPriority.Low);
            builder.Entity<Package>().Property(p => p.State).HasDefaultValue(EState.Waiting);
            builder.Entity<Package>().Property(p => p.TerminalOriginId)
                .IsRequired();
            builder.Entity<Package>().Property(p => p.TerminalDestinyId)
                .IsRequired();
            builder.Entity<Package>().HasOne(p => p.Freight)
                .WithMany(p => p.Packages).HasForeignKey(p => p.FerightId);
            builder.Entity<Package>().HasOne(p => p.Dispatcher)
                .WithMany(p => p.Packages).HasForeignKey(p => p.DispatcherId);
            builder.Entity<Package>().HasOne(p => p.Customer)
                .WithMany(p => p.Packages).HasForeignKey(p => p.CustomerId);
            

            builder.Entity<Commentary>().ToTable("Commentaries");
            builder.Entity<Commentary>().HasKey(p => p.Id);
            builder.Entity<Commentary>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Commentary>().HasOne(p => p.Company)
                .WithMany(p => p.Commentaries).HasForeignKey(p => p.CompanyId);
            builder.Entity<Commentary>().HasOne(p => p.Customer)
                .WithMany(p => p.Commentaries).HasForeignKey(p => p.CustomerId);

                //Initialization

            builder.Entity<Company>().ToTable("Companies");
            builder.Entity<Company>().HasKey(p => p.Id);
            builder.Entity<Company>().Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Company>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Company>().Property(p => p.Username)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Company>().Property(p => p.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Company>().Property(p => p.Number)
                .IsRequired().HasMaxLength(9);
            builder.Entity<Company>().Property(p => p.Password)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Company>().Property(p => p.Ruc)
                .IsRequired().HasMaxLength(11);
            builder.Entity<Company>()
                .HasMany(p => p.Subscriptions)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId);

            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Customer>().HasKey(p => p.Id);
            builder.Entity<Customer>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().Property(p => p.Username)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Customer>().Property(p => p.Number)
                .IsRequired().HasMaxLength(9);
            builder.Entity<Customer>().Property(p => p.Brithday)
                .IsRequired();
            builder.Entity<Customer>().Property(p => p.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(p => p.Password)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Customer>()
                .HasMany(p => p.Subscriptions)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            builder.Entity<Dealer>().ToTable("Employees");
            builder.Entity<Dealer>().HasKey(p => p.Id);
            builder.Entity<Dealer>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Dealer>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Dealer>().Property(p => p.Username)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Dealer>().Property(p => p.Number)
                .IsRequired().HasMaxLength(9);
            builder.Entity<Dealer>().Property(p => p.Brithday)
                .IsRequired();
            builder.Entity<Dealer>().Property(p => p.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Dealer>().Property(p => p.Password)
                .IsRequired().HasMaxLength(15);

            //MemberShip
            builder.Entity<TypeOfCurrent>().ToTable("TypeOfCurrents");
            builder.Entity<TypeOfCurrent>().HasKey(p => p.Id);
            builder.Entity<TypeOfCurrent>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TypeOfCurrent>().Property(p => p.Name)
                .IsRequired().HasMaxLength(15);
            builder.Entity<TypeOfCurrent>()
                .HasMany(p => p.PlanCompanies)
                .WithOne(p => p.TypeOfCurrent)
                .HasForeignKey(p => p.TypeOfCurrentId);
            builder.Entity<TypeOfCurrent>()
                .HasMany(p => p.PlanCustomers)
                .WithOne(p => p.TypeOfCurrent)
                .HasForeignKey(p => p.TypeOfCurrentId);


            builder.Entity<PlanCustomer>().ToTable("PlanCustomers");
            builder.Entity<PlanCustomer>().HasKey(p => p.Id);
            builder.Entity<PlanCustomer>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PlanCustomer>().Property(p => p.Name)
                .IsRequired().HasMaxLength(20);
            builder.Entity<PlanCustomer>().Property(p => p.Cost)
                .IsRequired();
            builder.Entity<PlanCustomer>()
                .HasMany(p => p.SubscriptionCustomer)
                .WithOne(p => p.PlanCustomer)
                .HasForeignKey(p => p.PlanId);

            builder.Entity<PlanCompany>().ToTable("PlanCompanies");
            builder.Entity<PlanCompany>().HasKey(p => p.Id);
            builder.Entity<PlanCompany>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PlanCompany>().Property(p => p.Name)
                .IsRequired().HasMaxLength(20);
            builder.Entity<PlanCompany>().Property(p => p.Cost)
                .IsRequired();
            builder.Entity<PlanCompany>()
             .HasMany(p => p.SubscriptionCompany)
             .WithOne(p => p.PlanCompany)
             .HasForeignKey(p => p.PlanId);

            builder.Entity<SubscriptionCustomer>().ToTable("SubscriptionCustomers");
            builder.Entity<SubscriptionCustomer>().HasKey(p => p.Id);
            builder.Entity<SubscriptionCustomer>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubscriptionCustomer>().Property(p => p.Discount)
                .IsRequired();
            builder.Entity<SubscriptionCustomer>().Property(p => p.DateTime)
                .IsRequired();
            builder.Entity<SubscriptionCustomer>().Property(p => p.TotalPrice)
                .IsRequired();

            builder.Entity<SubscriptionCompany>().ToTable("SubscriptionCompany");
            builder.Entity<SubscriptionCompany>().HasKey(p => p.Id);
            builder.Entity<SubscriptionCompany>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubscriptionCompany>().Property(p => p.Discount)
                .IsRequired();
            builder.Entity<SubscriptionCompany>().Property(p => p.DateTime)
                .IsRequired();
            builder.Entity<SubscriptionCompany>().Property(p => p.TotalPrice)
                .IsRequired();

            //Communication
            builder.Entity<CustomerServiceEmployee>().ToTable("CustomersServicesEmployees");
            builder.Entity<CustomerServiceEmployee>().HasKey(p => p.Id);
            builder.Entity<CustomerServiceEmployee>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<CustomerServiceEmployee>().Property(p => p.Name)
                .IsRequired();
            builder.Entity<CustomerServiceEmployee>().HasOne(p => p.Terminal)
                .WithMany(p => p.CustomerServiceEmployees).HasForeignKey(p => p.TerminalId);

            builder.Entity<Chat>().ToTable("Chats");
            builder.Entity<Chat>().HasKey(p => p.Id);
            builder.Entity<Chat>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Chat>().Property(p => p.PostDate)
                .IsRequired();
            builder.Entity<Chat>().HasMany(p => p.Messages)
                .WithOne(p => p.Chat).HasForeignKey(p => p.ChatId);

            builder.Entity<Message>().ToTable("Messages");
            builder.Entity<Message>().HasKey(p => p.Id);
            builder.Entity<Message>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Message>().Property(p => p.Description)
                .IsRequired();
            builder.Entity<Message>().HasOne(p => p.Chat)
                .WithMany(p => p.Messages).HasForeignKey(p => p.ChatId);
            builder.Entity<Message>().HasOne(p => p.Customer)
               .WithMany(p => p.Messages).HasForeignKey(p => p.CustomerId);
            builder.Entity<Message>().HasOne(p => p.CustomerServiceEmployee)
               .WithMany(p => p.Messages).HasForeignKey(p => p.CustomerServiceEmployeeId);

            builder.Entity<ShipTerminal>().ToTable("ShipTerminal");
            builder.Entity<ShipTerminal>().HasKey(k => new { k.TerminalOriginId, k.TerminalDestinyId });
            builder.Entity<ShipTerminal>().HasOne(p => p.TerminalDestiny)
                .WithMany(p => p.ShipTerminalsd).HasForeignKey(p => p.TerminalDestinyId);
            builder.Entity<ShipTerminal>().HasOne(p => p.TerminalOrigin)
                .WithMany(p => p.ShipTerminalso).HasForeignKey(p => p.TerminalOriginId);

            builder.Entity<ChangeState>().ToTable("ChangeStates");
            builder.Entity<ChangeState>().HasKey(p => p.Id);
            builder.Entity<ChangeState>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            //builder.ApplySnakeCaseNamingConvention();
        }
    }

}
