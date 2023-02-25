using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.Engine.IoC;
using System.Reflection;

namespace MontyHall.Test
{
    public class ServiceFixture
    {
        public IMediator? Bus { get; set; }

        public ServiceProvider Service { get; set; }

        public ServiceFixture()
        {


            Service = new ServiceCollection()
                .AddGameEngine(Assembly.GetExecutingAssembly().GetType())
                .BuildServiceProvider();

        }
    }
}
