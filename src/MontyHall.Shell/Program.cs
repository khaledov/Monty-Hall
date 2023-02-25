//// See https://aka.ms/new-console-template for more information
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MontyHall.Engine.Formatters;
using MontyHall.Engine.IoC;
using MontyHall.Engine.Services;
using System.Reflection;

ILogger<Program>? logger = null;
var app = new CommandLineApplication();
try
{

    app.HelpOption();

    var games = app.Option("-g|--games <N>", " How many times you want to run that game.", CommandOptionType.SingleValue);


    var iSwitch = app.Option("-s|--switch <boolean>", "Do you want to switch doors or no for all tries", CommandOptionType.SingleValue);


    app.OnExecute(() =>
    {

        var serviceProvider = new ServiceCollection()
                                   .AddGameEngine(Assembly.GetExecutingAssembly().GetType())
                                   .BuildServiceProvider();
        var engine = serviceProvider.GetService<IGameConsole>();
        logger = serviceProvider.GetService<ILogger<Program>>();
        if (engine == null)
            throw new InvalidOperationException();
        Task.Run(async () =>
        {
            try
            {
                engine.Init(new MontyHall.Engine.Models.GameModel { Tries = long.Parse(games.Value()), Switch = bool.Parse(iSwitch.Value()) });
                await engine.Run();
            }
            catch (Exception ex)
            {
                if (logger != null)
                    logger.LogError(ex, ex.Message);
                ConsoleFormatter.WriteException(ex);
            }
        }).Wait();







    });
}
catch (Exception ex)
{

    if (logger != null)
        logger.LogError(ex, ex.Message);
    ConsoleFormatter.WriteException(ex);
}


return app.Execute(args);

