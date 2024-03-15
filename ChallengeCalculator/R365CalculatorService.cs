internal class R365CalculatorService : ICalculatorService
{
    private static readonly int MaxNumberOfOperands = 2;

    public int Calculate(string input)
    {
        var operands = ParseExpression(input);

        //JIRA-1 - Throw an exception when more than 2 numbers are provided
        if (operands.Count > MaxNumberOfOperands)
            throw new InvalidOperationException($"Maximum number of operands supported is {MaxNumberOfOperands} but {operands.Count} were found.");

        return operands.Sum();
    }

    private IList<int> ParseExpression(string input)
    {
        if (string.IsNullOrEmpty(input))
            return [0];

        var inputOperands = input.Split([',']);

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