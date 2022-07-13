using Akgunler.Models;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Akgunler.Models.Jobs;
using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Akgunler.Data
{
	public class AkgunlerContext : DbContext
	{
		public DbSet<District> Districts { get; set; }
		public DbSet<Province> Cities { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<AddressType> AddressTypes { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Document> Documents { get; set; }
		public DbSet<DocumentType> DocumentTypes { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Ship> Ships { get; set; }
		public DbSet<Port> Ports { get; set; }
		public DbSet<Customs> Customs { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Log> Logs { get; set; }

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<CustomerAddress> CustomerAddresses { get; set; }
		public DbSet<CustomerContact> CustomerContacts { get; set; }
		public DbSet<CustomerDocument> CustomerDocuments { get; set; }

		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountType> AccountTypes { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<JobType> JobTypes { get; set; }
		public DbSet<Freight> Freights { get; set; }

		public DbSet<Staff> Staffs { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<DriversLicenseType> DriversLicenseTypes { get; set; }
		public DbSet<StaffDocument> StaffDocuments { get; set; }
		
		public DbSet<Fuel> Fuels { get; set; }
		public DbSet<Make> Makes { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Transmission> Transmissions { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<VehicleDocument> VehicleDocuments { get; set; }
		public DbSet<VehicleGroup> VehicleGroups { get; set; }

		public DbSet<VehicleType> VehicleTypes { get; set; }
		
		public AkgunlerContext(DbContextOptions<AkgunlerContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.Property(e => e.LockoutEnabled)
				.HasDefaultValue(false);

			modelBuilder.Entity<User>()
				.Property(e => e.AccessFailedCount)
				.HasDefaultValue(0);

			modelBuilder
				.Entity<AddressType>()
				.Property(e => e.AddressGroup)
				.HasConversion(
					v => v.ToString(),
					v => (AddressGroup)Enum.Parse(typeof(AddressGroup), v));

			modelBuilder
				.Entity<DocumentType>()
				.Property(e => e.DocumentGroup)
				.HasConversion(
					v => v.ToString(),
					v => (DocumentGroup)Enum.Parse(typeof(DocumentGroup), v));

			modelBuilder
				.Entity<Job>()
				.HasOne(x => x.Staff);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

		}

	}
}
