internal class CalculatorConfig
{
    public bool OnlyAcceptPositiveInputs { get; set; }
    public int? InputCeiling { get; set; }
    public string? AlternateDelimiter { get; set; }
    public bool DisplayFormula { get; set; }
    public bool LoopMode { get; set; }
    public string Operation { get; set; } = "Addition";

}