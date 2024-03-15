using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICalculatorService, R365CalculatorService>();
builder.Services.AddHostedService<CalculatorHost>();

using IHost host = builder.Build();

await host.RunAsync();