internal interface ICalculatorService
{
    public int Calculate(string input);

    public (int result, string formula) CalculateWithFormula(string input);

}