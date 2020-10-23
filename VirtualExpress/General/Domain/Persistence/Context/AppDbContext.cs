using Microsoft.EntityFrameworkCore;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.General.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Terminal> Terminal { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<PackageDelivery> PackageDeliveries { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<Freight> Freights { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Commentary> Comentaries { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().ToTable("City");
            builder.Entity<City>().HasKey(k => k.Id);
            builder.Entity<City>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<City>()
                .HasMany(p => p.customers)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId);
            builder.Entity<City>()
                .HasMany(p => p.employees)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId);
            builder.Entity<City>()
                .HasData(
                new City { Id = 1, Name = "Lima" }
                );


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

            builder.Entity<Delivery>().ToTable("Delivery");
            builder.Entity<Delivery>().HasKey(k => k.Id);
            builder.Entity<Delivery>().Property(k => k.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Delivery>().Property(p => p.Arrival)
                .IsRequired().HasMaxLength(25);
            builder.Entity<Delivery>().Property(p => p.Price)
                .IsRequired().HasMaxLength(25);

            builder.Entity<PackageDelivery>().ToTable("PackageDelivery");
            builder.Entity<PackageDelivery>().HasKey(k => k.DeliveryId);
            builder.Entity<PackageDelivery>().HasOne(p => p.Delivery)
                .WithMany(p => p.PackageDeliveries).HasForeignKey(p => p.DeliveryId);

            builder.Entity<Dispatcher>().ToTable("Dispatchers");
            builder.Entity<Dispatcher>().HasKey(Key => Key.Id);
            builder.Entity<Dispatcher>().Property(Key => Key.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Dispatcher>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Dispatcher>().Property(p => p.DNI).HasMaxLength(8);


            builder.Entity<Freight>().ToTable("Freight");
            builder.Entity<Freight>().HasKey(prop => prop.Id);
            builder.Entity<Freight>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Freight>().Property(p => p.Observations)
                .HasMaxLength(50);


            builder.Entity<Package>().ToTable("Package");
            builder.Entity<Package>().HasKey(p => p.Id);
            builder.Entity<Package>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Package>().Property(p => p.Description)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Package>().Property(p => p.Observations).HasMaxLength(50);
            builder.Entity<Package>().Property(p => p.Priority).IsRequired();
            builder.Entity<Package>().Property(p => p.State).IsRequired();
            builder.Entity<Package>().HasOne(p => p.Freight)
                .WithMany(p => p.Packages).HasForeignKey(p => p.FerightId);
            builder.Entity<Package>().HasOne(p => p.Dispatcher)
                .WithMany(p => p.Packages).HasForeignKey(p => p.DispatcherId);


            builder.Entity<Commentary>().ToTable("Commentary");
            builder.Entity<Commentary>().HasKey(p => p.Id);
            builder.Entity<Commentary>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();

            builder.Entity<Company>().ToTable("Companies");
            builder.Entity<Company>().HasKey(p => p.Id);
            builder.Entity<Company>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
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

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.Username)
                .IsRequired().HasMaxLength(15);
            builder.Entity<Employee>().Property(p => p.Number)
                .IsRequired().HasMaxLength(9);
            builder.Entity<Employee>().Property(p => p.Brithday)
                .IsRequired();
            builder.Entity<Employee>().Property(p => p.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.Password)
                .IsRequired().HasMaxLength(15);
        }
    }

}
