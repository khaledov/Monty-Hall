using MediatR;
using Microsoft.Extensions.Logging;
using MontyHall.Engine.Events;
using MontyHall.Engine.Exceptions;
using MontyHall.Engine.Extensions;
using MontyHall.Engine.Models;

namespace MontyHall.Engine.Services
{
    public class GameConsole : IGameConsole
    {
        #region Attributes
      
        private readonly IMediator _bus;
        private readonly ILogger<GameConsole> _logger;
        private readonly Random _random;
        #endregion

        public GameModel? GameModel { get; private set; }
        public Statistics? Statistics { get;private set; }

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsole" /> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="logger">The logger.</param>
        public GameConsole(IMediator bus, ILogger<GameConsole> logger)
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
            if (gameModel.Tries <= 0)
                throw new InvalidNoOfTriesException(gameModel.Tries);

            GameModel = gameModel;
            Statistics = new Statistics
            {
                GamesCount = gameModel.Tries,
                Strategy = gameModel.Switch ? "Switch selection" : "Keep selection"
            };

        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <exception cref="MontyHall.Engine.Exceptions.EngineNotInitializedException">You should invoke Init() method before start</exception>
        public async Task Run()
        {
            if (GameModel == null || Statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");
            try
            {
                Statistics.WinsCount = 0;
                await _bus.Publish(new EngineStartedEvent());
                long step = (long)Statistics.GamesCount / 100;
                long incremental = 0;
                long progress = 0;

                for (var i = 0; i < GameModel.Tries; i++)
                {
                    var initialChoose = _random.Next(0, 3);
                    SelectDoor(initialChoose);
                    RevealGoat();

                    var chosenDoor = GameModel.Switch ?
                    SelectDoor(DoorState.Default) :
                    SelectDoor(DoorState.Selected);

                    if (chosenDoor.Prize == GameModel.GOLD)
                        Statistics.IncrementWins();

                    incremental++;
                    //await _bus.Publish(new RoundDone(incremental));
                    Reset();

                    if (step>0 && incremental % step == 0)
                    {
                        progress++;
                        await _bus.Publish(new ProgressIncrementedEvent(progress));
                    }
                }


                await _bus.Publish(new EngineCompletedEvent(Statistics));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        } 
        #endregion

        #region Private helper methods

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset()
        {
            if (GameModel == null || Statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");
            GameModel.Doors.ForEach(door => door.State = DoorState.Default);
            GameModel.Doors.Shuffle();
        }

        /// <summary>
        /// Reveals the goat.
        /// </summary>
        /// <returns></returns>
        DoorModel RevealGoat()
        {
            if (GameModel == null || Statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            var goatDoor = GameModel.Doors.First(door => door.Prize == GameModel.GOAT &&
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
            if (GameModel == null || Statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            var door = GameModel.Doors.First(x => x.State == state);

            door.State = DoorState.Selected;

            return door;
        }


        /// <summary>
        /// Selects the door.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="MontyHall.Engine.Exceptions.InvalidDoorNumberException"></exception>
        /// <exception cref="MontyHall.Engine.Exceptions.DoorOpenedBeforeException"></exception>
        DoorModel SelectDoor(int index)
        {
            if (GameModel == null || Statistics == null)
                throw new EngineNotInitializedException("You should invoke Init() method before start");

            if (index < 0 || index > 2)
            {
                throw new InvalidDoorNumberException(index);
            }

            if (GameModel.Doors[index].State == DoorState.Opened)
            {
                throw new DoorOpenedBeforeException(index);
            }

            GameModel.Doors[index].State = DoorState.Selected;
            return GameModel.Doors[index];
        }
        #endregion
    }
}