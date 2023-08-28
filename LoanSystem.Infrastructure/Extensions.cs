using LoanSystem.Application.Abstraction.Auth;
using LoanSystem.Application.Abstraction.Generator;
using LoanSystem.Application.Abstraction.Identity;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Pwd;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Infrastructure.Auth;
using LoanSystem.Infrastructure.Generator;
using LoanSystem.Infrastructure.Identity;
using LoanSystem.Infrastructure.Persistence;
using LoanSystem.Infrastructure.Persistence.Repositories;
using LoanSystem.Infrastructure.Pwd;
using LoanSystem.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoanSystem.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddPersistence(configuration)
                .AddAuth(configuration);

            services.AddSingleton<IClock, UtcClock>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IStringGenerator, StringGenerator>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IUserAccessor, UserAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
            services.AddScoped<IPayerRepository, PayerRepository>();
            services.AddScoped<ICardRepository, CardRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.Bind(nameof(jwtOptions), jwtOptions);

            services.AddSingleton(jwtOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.IssuerSigningKey!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddAuthorization();

            services.AddSingleton<IAuthManager, AuthManager>();

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
