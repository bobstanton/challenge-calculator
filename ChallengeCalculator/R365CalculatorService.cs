internal class R365CalculatorService : ICalculatorService
{
    private readonly CalculatorConfig _config;

    public R365CalculatorService(CalculatorConfig config)
    {
        _config = config;
    }

    public int Calculate(string input)
    {
        var operands = ParseExpression(input, '\n');

        if (_config.OnlyAcceptPositiveInputs)
        {
            var negativeNumbers = operands.Where(x => x < 0);
            if (negativeNumbers.Any())
                throw new ArgumentException($"Only positive inputs are supported, the following negative numbers were found: {string.Join(", ", negativeNumbers)}");
        }

        return operands.Sum();
    }

    private IList<int> ParseExpression(string input, params char[]? additionalDelimiters)
    {
        if (string.IsNullOrEmpty(input))
            return [0];

        var inputOperands = input.Split([',', ..additionalDelimiters]);

        var parsedOperands = new List<int>(inputOperands.Length);

        foreach (var operand in inputOperands)
        {
            if (int.TryParse(operand, out var result))
            {
                parsedOperands.Add(result);
            }
            else
            {
                parsedOperands.Add(0);
            }
        }

        return parsedOperands;
    }
}