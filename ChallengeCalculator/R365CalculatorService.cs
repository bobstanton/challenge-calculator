internal class R365CalculatorService : ICalculatorService
{
    public int Calculate(string input)
    {
        var operands = ParseExpression(input, '\n');

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