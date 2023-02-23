//// See https://aka.ms/new-console-template for more information
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Qliro.MontyHall.Engine.Extensions;
using Qliro.MontyHall.Engine.Services;
using System.Reflection;

var app = new CommandLineApplication();
app.HelpOption();

var games = app.Option<int>("-g|--games <N>", " How many times you want to run that game.", CommandOptionType.SingleValue);
games.DefaultValue = 1;

var iSwitch = app.Option<bool>("-s|--switch <boolean>", "Do you want to switch doors or no for all tries", CommandOptionType.SingleValue);
iSwitch.DefaultValue = false;

app.OnExecute(() =>
{
    var serviceProvider = new ServiceCollection()
                                     .AddGameEngine(Assembly.GetExecutingAssembly().GetType())
                                     .BuildServiceProvider();
    var engine = serviceProvider.GetService<IGameConsole>();
    if (engine == null)
        throw new InvalidOperationException();

    engine.Init(new Qliro.MontyHall.Engine.Models.GameModel { Tries =10 /*games.ParsedValue*/, Switch =true /*iSwitch.ParsedValue*/ });
    engine.Run();

  
});


return app.Execute(args);

