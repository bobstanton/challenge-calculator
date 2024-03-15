[TestClass]
public class R365CalculatorServiceTests
{
    private readonly CalculatorConfig _defaultConfig = new CalculatorConfig { OnlyAcceptPositiveInputs = false };

    [TestMethod]
    [DataRow("20", 20)]
    [DataRow("0", 0)]
    [DataRow("1", 1)]
    [DataRow("-1", -1)]
    [DataRow("-20", -20)]
    public void Calculate_SingleNumber_ReturnsNumber(string input, int expected)
    {
        var calculatorService = new R365CalculatorService(_defaultConfig);

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("1,5000", 5001)]
    [DataRow("4,-3", 1)]
    [DataRow("5,tytyt", 5)]
    [DataRow("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
    [DataRow("1,2,3", 6)]
    [DataRow("1,2,3,4", 10)]
    [DataRow("1,,2,,3", 6)]
    [DataRow(",1,2,3", 6)]
    [DataRow("1,2,3,", 6)]
    [DataRow(",1,2,3,", 6)]
    [DataRow(",,1,2,3,,", 6)]
    [DataRow("1\n2,3", 6)]
    [DataRow("1\n2\n3", 6)]
    [DataRow("\n1\n\n2\n3\n", 6)]
    public void Calculate_MultipleNumbers_ReturnsSum(string input, int expected)
    {
        var calculatorService = new R365CalculatorService(_defaultConfig);

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("invalid")]
    [DataRow("invalid,invalid")]
    [DataRow(",0")]
    [DataRow(",,0")]
    [DataRow("0,")]
    [DataRow("0,,")]
    [DataRow(",")]
    [DataRow(",,")]
    [DataRow(",,,")]
    public void Calculate_InvalidInput_ReturnsZero(string input)
    {
        var calculatorService = new R365CalculatorService(_defaultConfig);

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    [DataRow("-1")]
    [DataRow("-1,-1")]
    [DataRow("-1,1,-1")]
    [DataRow("1,-1")]
    [DataRow("-1,1")]
    public void Calculate_WithNegativeInput_ThrowsArgumentException(string input)
    {
        var calculatorService = new R365CalculatorService(new CalculatorConfig() { OnlyAcceptPositiveInputs = true });

        Assert.ThrowsException<ArgumentException>(() => calculatorService.Calculate(input));
    }

    [TestMethod]
    [DataRow("2,1001,6", 8)]
    public void Calculate_WithInputCeiling_ShouldReplaceOperandsGreaterThanCeilingWithZero(string input, int expected)
    {
        var calculatorService = new R365CalculatorService(new CalculatorConfig() { InputCeiling = 1000 });

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("//#\n2#5", 7)]
    [DataRow("//,\n2,ff,100", 102)]
    public void Calculate_WithCustomDelimiter_ShouldReturnCorrectResult(string input, int expected)
    {
        var calculatorService = new R365CalculatorService(_defaultConfig);

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("//[***]\n11***22***33", 66)]
    [DataRow("//[*][!!][r9r]\n11r9r22*hh*33!!44", 110)]
    public void Calculate_WithMultipleCustomDelimiters_ShouldReturnCorrectResult(string input, int expected)
    {
        var calculatorService = new R365CalculatorService(_defaultConfig);

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }


}