using MontyHall.Engine.Models;

namespace MontyHall.Engine.Services
{
    public interface IGameConsole
    {

         GameModel? GameModel { get;   }
         Statistics? Statistics { get;  }

        void Init(GameModel gameModel);
        Task Run();
    }
}