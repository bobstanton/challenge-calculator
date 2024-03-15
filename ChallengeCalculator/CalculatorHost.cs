using Microsoft.Extensions.Hosting;

internal class CalculatorHost : IHostedService
{
    private bool _isStopped = false;
    private bool _runOnce = false;
    private bool _displayFormula;

    private ICalculatorService _calculatorService;

    public CalculatorHost(ICalculatorService calculatorService, CalculatorConfig config)
    {
        _calculatorService = calculatorService;
        _runOnce = !config.LoopMode;
        _displayFormula = config.DisplayFormula;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        while (!_isStopped)
        {
            var input = Console.ReadLine();
            var (result, formula) = _calculatorService.CalculateWithFormula(input);

            if (_displayFormula)
                Console.WriteLine($"{formula} = {result}");
            else
                Console.WriteLine(result);

            if (_runOnce)
                _isStopped = true;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _isStopped = false;
        return Task.CompletedTask;
    }
}