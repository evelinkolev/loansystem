using LoanSystem.Services.Authentication;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
