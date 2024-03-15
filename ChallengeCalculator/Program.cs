using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICalculatorService, R365CalculatorService>();
builder.Services.AddHostedService<CalculatorHost>();

var rootCommand = new RootCommand("Restaurant365 Code Challenge - String Calculator");

var positive = new Option<bool>(new[] { "--positive", "-p" }, "Only accepts positive inputs.");
var ceiling = new Option<bool>(new[] { "--ceiling", "-c" }, "Sets a limit of 1000 for input values.");
var formula = new Option<bool>(new[] { "--formula", "-f" }, "Displays the formula used to calculate the result.");
var loop = new Option<bool>(new[] { "--loop", "-l" }, "Enables loop mode, which allows for continuous calculations.");
var alternateDelimiter = new Option<string?>(new[] { "--alternateDelimiter", "-a" }, "Override newline with a different delimiter.");
var operation = new Option<string?>(new[] { "--operation", "-o" }, "Operation to use, can be Addition, Subtraction, Multiplication or Division.");

rootCommand.AddOption(positive);
rootCommand.AddOption(ceiling);
rootCommand.AddOption(formula);
rootCommand.AddOption(loop);
rootCommand.AddOption(alternateDelimiter);
rootCommand.AddOption(operation);

rootCommand.SetHandler((bool positive, bool ceiling, bool formula, bool loop, string? alternativeDelimtier, string? operation) =>
{
    if (operation != null && !new[] { "Addition", "Subtraction", "Multiplication", "Division" }.Contains(operation))
    {
        Console.WriteLine("Invalid operation, please use one of the following: Addition, Subtraction, Multiplication, Division");
        return;
    }

    var calculatorConfig = new CalculatorConfig { 
        OnlyAcceptPositiveInputs = positive,
        InputCeiling = ceiling ? 1000 : null,
        DisplayFormula = formula,
        LoopMode = loop,
        AlternateDelimiter = alternativeDelimtier,
        Operation = operation ?? "Addition"
    };

    builder.Services.AddSingleton<CalculatorConfig>(calculatorConfig);
}, positive, ceiling, formula, loop, alternateDelimiter, operation);

rootCommand.Invoke(args);

using IHost host = builder.Build();

await host.RunAsync();