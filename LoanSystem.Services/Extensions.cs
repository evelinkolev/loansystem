using LoanSystem.Data;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Authentication;
using LoanSystem.Services.Authorization;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoanSystem.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .Authentication(configuration)
                .Authorization();

            return services;
        }

        public static IServiceCollection Authorization(
            this IServiceCollection services)
        {
            services
                .AddAuthorization(options =>
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                });

            // Authorization handlers.
            services.AddScoped<IAuthorizationHandler,
                LoanIsOwnerAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler,
                LoanAdministratorAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler,
                LoanAgentAuthorizationHandler>();

            return services;
        }

        public static IServiceCollection Authentication(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LoanSystemContext>();

            var authenticationOptions = new AuthenticationOptions();
            configuration.Bind(nameof(authenticationOptions), authenticationOptions);

            services.AddSingleton(authenticationOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            services
                .AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

    }
}
