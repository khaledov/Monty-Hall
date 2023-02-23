using Qliro.MontyHall.Engine.Models;

namespace Qliro.MontyHall.Engine.Services
{
    public interface IGameConsole
    {
        void Init(GameModel gameModel);
        Task Run();
    }
}