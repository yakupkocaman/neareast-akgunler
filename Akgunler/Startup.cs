using Akgunler.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Akgunler.Extensions.System;
using Newtonsoft.Json;
using Akgunler.Services.Core;
using Akgunler.Services.Customers;
using Akgunler.Services.Staffs;
using Akgunler.Services.Vehicles;
using Akgunler.Services.Jobs;

namespace Akgunler
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
		
        public void ConfigureServices(IServiceCollection services)
        {
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<AkgunlerContext>(o =>
				o.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("Migration", "Core")));

			services.AddSession();
			services.AddMemoryCache();

			services.AddAuthentication(options =>
			{
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
				.AddCookie(options =>
				{
					options.LoginPath = "/Auth";
				});


			services.AddRouting(options => options.LowercaseUrls = true);

			services.AddRazorPages()
			  .AddRazorRuntimeCompilation()
			  .AddNewtonsoftJson(o =>
			  {
				  o.SerializerSettings.ContractResolver = new DefaultContractResolver();
				  o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			  })
			  .AddRazorPagesOptions(options =>
			  {
				  options.Conventions.AuthorizeFolder("/");
				  options.Conventions.AllowAnonymousToPage("/Auth");
			  })
			  .AddMvcOptions(options =>
			  {
				  options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
			  });

			services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);


			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
						.SetIsOriginAllowed(_ => true)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});


			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient(typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));

			services.AddTransient(typeof(IAddressService), typeof(AddressService));
			services.AddTransient(typeof(ILogService), typeof(LogService));
			services.AddTransient(typeof(IUserService), typeof(UserService));

			services.AddTransient(typeof(ICustomerService), typeof(CustomerService));
			services.AddTransient(typeof(ICustomerAddressService), typeof(CustomerAddressService));
			services.AddTransient(typeof(ICustomerContactService), typeof(CustomerContactService));
			services.AddTransient(typeof(ICustomerDocumentService), typeof(CustomerDocumentService));

			services.AddTransient(typeof(IAccountService), typeof(AccountService));
			services.AddTransient(typeof(IFreightService), typeof(FreightService));
			services.AddTransient(typeof(IJobService), typeof(JobService));
			services.AddTransient(typeof(IJobStaffService), typeof(JobStaffService));

			services.AddTransient(typeof(IStaffService), typeof(StaffService));
			services.AddTransient(typeof(IStaffDocumentService), typeof(StaffDocumentService));

			services.AddTransient(typeof(IVehicleService), typeof(VehicleService));
			services.AddTransient(typeof(IVehicleDocumentService), typeof(VehicleDocumentService));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseSession();
			app.UseCors("CorsPolicy");
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStatusCodePagesWithRedirects("~/error/{0}");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});

			app.UseHttpsRedirection();
			app.UseAuthentication();

			

			DataSeeder.Init(app.ApplicationServices);

		}
    }
}
