using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddData(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .HostDatabaseConfiguration(configuration);
            return services;
        }

        public static IServiceCollection HostDatabaseConfiguration(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddDbContext<LoanSystemContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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

        public static async Task<IHost> InitializeRolesAsync(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //adding roles
                string[] roleNames = { "Admin", "Agent", "Member" };

                foreach (var roleName in roleNames)
                {
                    //creating the roles once and seeding them to the database.
                    if(!await RoleManager.RoleExistsAsync(roleName))
                    {
                        await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }

            return host;
        }

        public static async Task<IHost> InitializeUserRolesAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "admin@loansystem.com";
                string password = "Passw0rd!";

                if(await UserManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await UserManager.CreateAsync(user, password);

                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }

            return host;
        }


    }
}
