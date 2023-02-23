using MediatR;
using Qliro.MontyHall.Engine.Events;
using Qliro.MontyHall.Engine.Exceptions;
using Qliro.MontyHall.Engine.Extensions;
using Qliro.MontyHall.Engine.Models;
using Serilog;

namespace Qliro.MontyHall.Engine.Services
{
    public class GameConsole : IGameConsole
    {
        private GameModel? _gameModel;
        private Statistics? _statistics;
        private readonly IMediator _bus;
        private readonly ILogger _logger;
        private readonly Random _random;
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsole" /> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="logger">The logger.</param>
        public GameConsole(IMediator bus, ILogger logger)
        {
            _bus = bus;
            _logger = logger;
            _random = new Random();
        }
        /// <summary>
        /// Feeds this game instance with required data.
        /// </summary>
        /// <param name="gameModel">The game model.</param>
        public void Init(GameModel gameModel)
        {
            _gameModel = gameModel;
            _statistics = new Statistics { GamesCount = gameModel.Tries, 
                Strategy = gameModel.Switch ? "Switch selection" :"Keep selection" };

        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <exception cref="Qliro.MontyHall.Engine.Exceptions.EngineNotInitializedException">You should invoke Init() method before start</exception>
        public async Task Run()
        {
            if (_gameModel == null || _statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");
            try
            {
                _statistics.WinsCount = 0;
                await _bus.Publish(new EngineStartedEvent());
                await Task.Run(async () =>
                {
                    for (int i = 0; i < _gameModel.Tries; i++)
                    {
                        var initialChoose = _random.Next(0, 3);
                        SelectDoor(initialChoose);
                        RevealGoat();

                        var chosenDoor = _gameModel.Switch ?
                        SelectDoor(DoorState.Default) :
                        SelectDoor(DoorState.Selected);

                        if (chosenDoor.Prize == GameModel.GOLD)
                            _statistics.IncrementWins();

                        Reset();

                        await _bus.Publish(new ProgressIncrementedEvent());
                    }
                });
                await _bus.Publish(new EngineCompletedEvent(_statistics));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }

        }

        #region Private helper methods

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset()
        {
            if (_gameModel == null || _statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");
            _gameModel.Doors.ForEach(door => door.State = DoorState.Default);
            _gameModel.Doors.Shuffle();
        }

        /// <summary>
        /// Reveals the goat.
        /// </summary>
        /// <returns></returns>
        DoorModel RevealGoat()
        {
            if (_gameModel == null || _statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            var goatDoor = _gameModel.Doors.First(door => door.Prize == GameModel.GOAT &&
                                                          door.State != DoorState.Selected);
            goatDoor.State = DoorState.Opened;
            return goatDoor;
        }

        /// <summary>
        /// Selects the door.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        DoorModel SelectDoor(DoorState state)
        {
            if (_gameModel == null || _statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            var door = _gameModel.Doors.First(x => x.State == state);

            door.State = DoorState.Selected;

            return door;
        }


        /// <summary>
        /// Selects the door.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="Qliro.MontyHall.Engine.Exceptions.InvalidDoorNumberException"></exception>
        /// <exception cref="Qliro.MontyHall.Engine.Exceptions.DoorOpenedBeforeException"></exception>
        DoorModel SelectDoor(int index)
        {
            if (_gameModel == null || _statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            if (index < 0 || index > 2)
            {
                throw new InvalidDoorNumberException(index);
            }

            if (_gameModel.Doors[index].State == DoorState.Opened)
            {
                throw new DoorOpenedBeforeException(index);
            }

            _gameModel.Doors[index].State = DoorState.Selected;
            return _gameModel.Doors[index];
        }
        #endregion
    }
}