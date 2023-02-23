using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qliro.MontyHall.Engine.Services;
using Scrutor;
using Serilog;
using System.Reflection;

namespace Qliro.MontyHall.Engine.Extensions
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
