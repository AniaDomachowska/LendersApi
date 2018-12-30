using LendersApi.Dto;
using LendersApi.Helpers;
using LendersApi.Repository;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using NLog.Extensions.Logging;
using NLog.Web;

namespace LendersApi
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
			AutoMapperConfig.Initialize();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddOData();
			services.AddODataQueryFilter();

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPeopleRepository, PeopleRepository>();
			services.AddScoped<ILoanRepository, LoanRepository>();

			services.AddDbContext<EfDbContext>(
				options =>
				{
					options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
				}
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, 
			ILoggerFactory loggerFactory, 
			IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseHttpsRedirection();

			app.UseMvc(b =>
				b.MapODataServiceRoute("odata", "odata", GetEdmModel()
				));

			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<EfDbContext>();
				context.Database.EnsureCreated();
			}

			//loggerFactory.AddNLog();

			//env.ConfigureNLog("nlog.config");

		}

		private IEdmModel GetEdmModel()
		{
			// you can add all the entities you need
			var builder = new ODataConventionModelBuilder();
			var people = builder.EntitySet<PersonDto>("People");

			var action = people.EntityType.Collection.Action("AddPerson");
			action.Parameter<PersonCreateDto>("model");
			action.ReturnsFromEntitySet<PersonDto>("People");

			var loans = builder.EntitySet<LoanDto>("Loans");

			action = loans.EntityType.Collection.Action("AddLoan");
			action.Parameter<LoanCreateDto>("model");
			action.ReturnsFromEntitySet<LoanDto>("Loans");

			action = loans.EntityType.Action("PayLoan");
			action.Parameter<decimal>("Amount");
			action.ReturnsFromEntitySet<LoanDto>("Loans");

			return builder.GetEdmModel();
		}
	}
}