using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Qliro.MontyHall.Engine.Services;
using Scrutor;
using System.Reflection;

namespace Qliro.MontyHall.Engine.IoC
{
    public static class AddGameEngineEx
    {
        public static IServiceCollection AddGameEngine(this IServiceCollection services, Type migrateAssemblyType)
        {
            var assemblies = new[]
            {
                Assembly.Load("Qliro.MontyHall.Engine")
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
