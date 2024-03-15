using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICalculatorService, R365CalculatorService>();
builder.Services.AddHostedService<CalculatorHost>();

var rootCommand = new RootCommand("Restaurant365 Code Challenge - String Calculator");
var positive = new Option<bool>(new[] { "--positive", "-p" }, "Only accepts positive inputs.");
rootCommand.AddOption(positive);

rootCommand.SetHandler((bool positive) =>
{
    var calculatorConfig = new CalculatorConfig { OnlyAcceptPositiveInputs = positive };
    builder.Services.AddSingleton<CalculatorConfig>(calculatorConfig);
}, positive);

rootCommand.Invoke(args);

using IHost host = builder.Build();

await host.RunAsync();