using System.Linq;

internal class R365CalculatorService : ICalculatorService
{
    private readonly CalculatorConfig _config;
    private readonly Dictionary<string, string> _operators = new Dictionary<string, string>
    {
        ["Addition"] = "+",
        ["Subtraction"] = "-",
        ["Multiplication"] = "*",
        ["Division"] = "/"
    };

    public R365CalculatorService(CalculatorConfig config)
    {
        _config = config;
    }

    public int Calculate(string input)
    {
        var (result, _) = CalculateWithFormula(input);
        return result;
    }

    public (int result, string formula) CalculateWithFormula(string input)
    {
        var (customDelimiters, remainingExpression) = ParseDelimiters(input);

        var operands = ParseExpression(remainingExpression, [_config.AlternateDelimiter ?? "\n", .. customDelimiters]);

        ValidateOperands(operands);

        var result = _config.Operation switch
        {
            "Addition" => operands.Sum(),
            "Subtraction" => operands.Aggregate((x, y) => x - y),
            "Multiplication" => operands.Aggregate(1, (x, y) => x * y),
            "Division" => operands.Aggregate((x, y) => x / y),
            _ => throw new ArgumentException($"Unsupported operation: {_config.Operation}")
        };

        var formula = string.Join(_operators[_config.Operation], operands);

        return (result, formula);
    }

    private IList<int> ParseExpression(string input, params string[]? additionalDelimiters)
    {
        if (string.IsNullOrEmpty(input))
            return [0];

        var inputOperands = input.Split([",", ..additionalDelimiters], StringSplitOptions.None);

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

    private (string[] customDelimiters, string remainingExpression) ParseDelimiters(string input)
    {
        //no delimiter
        if (!(input.StartsWith("//") && input[3] == '\n') && !input.StartsWith("//["))
            return ([], input);

        //single delimiter
        if (!input.StartsWith("//["))
        {
            var delimiter = input[2].ToString();

            return ([delimiter], input.Substring(4));
        }

        //multiple delimiters
        var delimiterEnd = input.IndexOf("]\n");

        //TODO: There should be a lot more validation around invalid delimiters, like a missing \n or missing brackets

        var delimiters = input.Substring(3, delimiterEnd - 3).Split("][");

        return (delimiters, input.Substring(delimiterEnd));
    }


}