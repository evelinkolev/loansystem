using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Infrastructure.Persistence;
using LoanSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoanSystem.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddPersistence(configuration);

            return services;
        }

        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddDbContext<LoanSystemContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IHost MigrateDatabase(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                using(var dbContext = scope.ServiceProvider.GetRequiredService<LoanSystemContext>())
                {
                    try
                    {
                        dbContext.Database.Migrate();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

            return host;
        }

        //public static async Task<IHost> InitializeRolesAsync(this IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        //adding roles
        //        string[] roleNames = { "Administrator", "Agent", "Member" };

        //        foreach (var roleName in roleNames)
        //        {
        //            //creating the roles once and seeding them to the database.
        //            if (!await RoleManager.RoleExistsAsync(roleName))
        //            {
        //                await RoleManager.CreateAsync(new IdentityRole(roleName));
        //            }
        //        }
        //    }

        //    return host;
        //}

        //public static async Task<IHost> InitializeUserRolesAsync(this IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        //        string email = "member@loansystem.com";
        //        string password = "Passw0rd!";

        //        if(await UserManager.FindByEmailAsync(email) == null)
        //        {
        //            var user = new User();
        //            user.UserName = email;
        //            user.Email = email;

        //            await UserManager.CreateAsync(user, password);

        //            await UserManager.AddToRoleAsync(user, "Member");
        //        }
        //    }

        //    return host;
        //}


    }
}
