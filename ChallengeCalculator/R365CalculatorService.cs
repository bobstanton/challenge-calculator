internal class R365CalculatorService : ICalculatorService
{
    private readonly CalculatorConfig _config;

    public R365CalculatorService(CalculatorConfig config)
    {
        _config = config;
    }

    public int Calculate(string input)
    {
        var (customDelimiter, remainingExpression) = ParseDelimiter(input);

        char[] delimiters = customDelimiter.HasValue ? ['\n', customDelimiter.Value] : ['\n'];
        var operands = ParseExpression(remainingExpression, delimiters);

        ValidateOperands(operands);

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

    private void ValidateOperands(IList<int> operands)
    {
        if (_config.OnlyAcceptPositiveInputs)
        {
            var negativeNumbers = operands.Where(x => x < 0);
            if (negativeNumbers.Any())
                throw new ArgumentException($"Only positive inputs are supported, the following negative numbers were found: {string.Join(", ", negativeNumbers)}");
        }

        if (_config.InputCeiling.HasValue)
        {
            for (int i = 0; i < operands.Count; i++)
            {
                if (operands[i] > _config.InputCeiling)
                    operands[i] = 0;
            }
        }
    }

    private (char? customDelimiter, string remainingExpression) ParseDelimiter(string input)
    {
        if (!(input.StartsWith("//") && input[3] == '\n'))
            return (null, input);

        var delimiter = input[2];
        
        return (delimiter, input.Substring(4));
    }


}