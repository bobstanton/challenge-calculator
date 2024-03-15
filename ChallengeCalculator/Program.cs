using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICalculatorService, R365CalculatorService>();
builder.Services.AddHostedService<CalculatorHost>();

var rootCommand = new RootCommand("Restaurant365 Code Challenge - String Calculator");
var positive = new Option<bool>(new[] { "--positive", "-p" }, "Only accepts positive inputs.");
rootCommand.AddOption(positive);
var ceiling = new Option<bool>(new[] { "--ceiling", "-c" }, "Sets a limit of 1000 for input values.");
rootCommand.AddOption(ceiling);

rootCommand.SetHandler((bool positive, bool ceiling) =>
{
    var calculatorConfig = new CalculatorConfig { 
        OnlyAcceptPositiveInputs = positive,
        InputCeiling = ceiling ? 1000 : null
    };

    builder.Services.AddSingleton<CalculatorConfig>(calculatorConfig);
}, positive, ceiling);

rootCommand.Invoke(args);

using IHost host = builder.Build();

await host.RunAsync();