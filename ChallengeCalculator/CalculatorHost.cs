using Microsoft.Extensions.Hosting;

internal class CalculatorHost : IHostedService
{
    private bool _isStopped = false;
    private ICalculatorService _calculatorService;

    public CalculatorHost(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        while (!_isStopped)
        {
            var input = Console.ReadLine();
            var result = _calculatorService.Calculate(input);
            Console.WriteLine(result);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _isStopped = false;
        return Task.CompletedTask;
    }
}