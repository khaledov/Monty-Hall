using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.Engine.Services;
using Scrutor;
using System.Reflection;

namespace MontyHall.Engine.IoC
{
    public static class Bootstrap
    {
        /// <summary>
        /// Adds the game engine.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="migrateAssemblyType">Type of the migrate assembly.</param>
        /// <returns></returns>
        public static IServiceCollection AddGameEngine(this IServiceCollection services, Type migrateAssemblyType)
        {
            var assemblies = new[]
            {
                Assembly.Load("MontyHall.Engine")
            };
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
            services.AddScoped<IGameConsole, GameConsole>();
            services.AddLogging();

            services.Scan(scanner =>
              scanner.FromAssemblies(assemblies)
                  .AddClasses(classes => classes.AssignableTo(typeof(INotificationHandler<>)))
                  .UsingRegistrationStrategy(RegistrationStrategy.Append)
                  .AsImplementedInterfaces()
                  .WithScopedLifetime());

            return services;
        }
    }
}
