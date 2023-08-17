using LoanSystem.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanSystem.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Extensions).Assembly));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            var registrationOptions = new RegistrationOptions();
            configuration.Bind(nameof(registrationOptions), registrationOptions);

            return services;
        }

    }
}
