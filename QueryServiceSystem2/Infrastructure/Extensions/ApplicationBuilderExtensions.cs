namespace QueryServiceSystem2.Infrastructure.Extensions
{
    using QueryServiceSystem2.Data;
    using QueryServiceSystem2.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static QueryServiceSystem2.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCars(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<QueryService2DbContext>();

            data.Database.Migrate();
        }

        private static void SeedCars(IServiceProvider services)
        {
            var data = services.GetRequiredService<QueryService2DbContext>();

            if (data.Cars.Any())
            {
                return;
            }
            data.Cars.AddRange(new[]
            {
                new Car { Name = "Audi"},
                new Car { Name = "Seat"},
                new Car { Name = "Renault"},
                new Car { Name = "Peugeot"},
                new Car { Name = "Dacia"},
                new Car { Name = "Citroën"},
                new Car { Name = "Opel"},
                new Car { Name = "Alfa Romeo"},
                new Car { Name = "Škoda"},
                new Car { Name = "Chevrolet"},
                new Car { Name = "Porsche"},
                new Car { Name = "Honda"},
                new Car { Name = "Subaru"},
                new Car { Name = "Mazda"},
                new Car { Name = "Mitsubishi"},
                new Car { Name = "Lexus"},
                new Car { Name = "Toyota"},
                new Car { Name = "BMW"},
                new Car { Name = "Volkswagen"},
                new Car { Name = "Suzuki"},
                new Car { Name = "Mercedes-Benz"},
                new Car { Name = "Saab"},
                new Car { Name = "Land Rover"},
                new Car { Name = "Dodge"},
                new Car { Name = "Chrysler"},
                new Car { Name = "Ford"},
                new Car { Name = "Hummer"},
                new Car { Name = "Hyundai"},
                new Car { Name = "Infiniti"},
                new Car { Name = "Jaguar"},
                new Car { Name = "Jeep"},
                new Car { Name = "Nissan"},
                new Car { Name = "Volvo"},
                new Car { Name = "Daewoo"},
                new Car { Name = "Fiat"},
                new Car { Name = "MINI"},
                new Car { Name = "Rover"},
                new Car { Name = "Smart"}
            });
               
            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
            if (await roleManager.RoleExistsAsync(AdministratorRoleName))
            {
                return;
            }

            var role = new IdentityRole { Name = AdministratorRoleName };

            await roleManager.CreateAsync(role);

                const string adminEmail = "admin@crs.com";
                const string adminPassword = "admin12";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user,adminPassword);

                await userManager.AddToRoleAsync(user,role.Name);
        })
                .GetAwaiter()
                .GetResult();
    }
}
}
