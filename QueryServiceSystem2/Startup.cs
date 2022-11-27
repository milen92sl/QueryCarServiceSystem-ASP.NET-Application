namespace QueryServiceSystem2
{
    using QueryServiceSystem2.Controllers;
    using QueryServiceSystem2.Data;
    using QueryServiceSystem2.Data.Models;
    using QueryServiceSystem2.Infrastructure.Extensions;
    using QueryServiceSystem2.Services.Queries;
    using QueryServiceSystem2.Services.Queries.Models;
    using QueryServiceSystem2.Services.Mechanics;
    using QueryServiceSystem2.Services.Statistics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QueryService2DbContext>(options => options
            .UseSqlServer(this.Configuration
            .GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<QueryService2DbContext>();


            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IMechanicService, MechanicService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                 {
                     endpoints.MapDefaultAreaRoute();

                     endpoints.MapControllerRoute(
                         name: "Query Details",
                         pattern: "/Queries/Details/{id}/{information}",
                         defaults: new
                         {
                             controler = typeof(QueriesController).GetControllerName(),
                             action = nameof(QueriesController.Details)
                         });

                     endpoints.MapDefaultControllerRoute();
                     endpoints.MapRazorPages();
                 });
        }
    }
}
