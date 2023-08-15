using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Infrastructure.Persistence;
using LoanSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoanSystem.Infrastructure
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
    }
}
