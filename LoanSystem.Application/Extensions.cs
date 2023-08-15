using LoanSystem.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LoanSystem.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Extensions).Assembly));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }

    }
}
