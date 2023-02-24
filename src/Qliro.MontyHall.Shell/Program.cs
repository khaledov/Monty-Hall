//// See https://aka.ms/new-console-template for more information
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qliro.MontyHall.Engine.Formatters;
using Qliro.MontyHall.Engine.IoC;
using Qliro.MontyHall.Engine.Services;
using System.Reflection;

var app = new CommandLineApplication();
app.HelpOption();

var games = app.Option<int>("-g|--games <N>", " How many times you want to run that game.", CommandOptionType.SingleValue);
games.DefaultValue = 1;

var iSwitch = app.Option<bool>("-s|--switch <boolean>", "Do you want to switch doors or no for all tries", CommandOptionType.SingleValue);
iSwitch.DefaultValue = false;
ILogger logger = null;
app.OnExecute(() =>
{
    try
    {
        var serviceProvider = new ServiceCollection()
                                    .AddGameEngine(Assembly.GetExecutingAssembly().GetType())
                                    .BuildServiceProvider();
        var engine = serviceProvider.GetService<IGameConsole>();
        logger = serviceProvider.GetService<ILogger>();
        if (engine == null)
            throw new InvalidOperationException();
        Task.Run(async () =>
        {
            engine.Init(new Qliro.MontyHall.Engine.Models.GameModel { Tries = 10000000 /*games.ParsedValue*/, Switch = true /*iSwitch.ParsedValue*/ });
            await engine.Run();
        }).Wait();

    }
    catch (Exception ex)
    {
        if (logger != null)
            logger.LogError(ex, ex.Message);
        ConsoleFormatter.WriteException(ex);
    }



});


return app.Execute(args);

