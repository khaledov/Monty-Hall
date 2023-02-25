using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.Engine.Exceptions;
using MontyHall.Engine.Services;
namespace MontyHall.Test
{
    public class EngineTest : IClassFixture<ServiceFixture>
    {
        public ServiceProvider Service { get; set; }
        public EngineTest(ServiceFixture fixture)
        {
            Service = fixture.Service;
        }


        [Fact]
        public async Task Switch_Strategy_Success()
        {
            var iEngine = Service.GetService<IGameConsole>();
            iEngine?.Init(new Engine.Models.GameModel { Tries = 10000000, Switch = true });

            await iEngine?.Run();
            iEngine?.Statistics?.Accuracy.Should().BeGreaterThan(0.66d);

        }

        [Fact]
        public async Task Keep_Selection_Strategy_Success()
        {
            var iEngine = Service.GetService<IGameConsole>();
            iEngine?.Init(new Engine.Models.GameModel { Tries = 10000000, Switch = false });

            await iEngine?.Run();
            iEngine?.Statistics?.Accuracy.Should().BeGreaterThan(0.33d);

        }

       

        [Fact]
        public void Exception_If_Tries_Negative()
        {
            
            try
            {
                var iEngine = Service.GetService<IGameConsole>();
                iEngine?.Init(new Engine.Models.GameModel { Tries = -1, Switch = false });
              
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType(typeof( InvalidNoOfTriesException));
                
            }
            
            

        }


        [Fact]
        public void Exception_If_Model_Empty()
        {

            try
            {
                var iEngine = Service.GetService<IGameConsole>();
              
                iEngine.Run();
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType(typeof(EngineNotInitializedException));

            }



        }
    }
}