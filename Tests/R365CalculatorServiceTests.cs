[TestClass]
public class R365CalculatorServiceTests
{
    [TestMethod]
    [DataRow("20", 20)]
    [DataRow("0", 0)]
    [DataRow("1", 1)]
    [DataRow("-1", -1)]
    [DataRow("-20", -20)]
    public void Calculate_SingleNumber_ReturnsNumber(string input, int expected)
    {
        var calculatorService = new R365CalculatorService();

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("1,5000", 5001)]
    [DataRow("4,-3", 1)]
    [DataRow("5,tytyt", 5)]
    public void Calculate_MultipleNumbers_ReturnsSum(string input, int expected)
    {
        var calculatorService = new R365CalculatorService();

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("invalid")]
    [DataRow(",0")]
    [DataRow("0,")]
    [DataRow(",")]
    public void Calculate_InvalidInput_ReturnsZero(string input)
    {
        var calculatorService = new R365CalculatorService();

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    [DataRow("1,2,3")]
    [DataRow("1,2,3,4")]
    [DataRow("1,,2,,3")]
    public void Calculate_ThrowsException_WhenNumberOfOperandsExceedsMax(string input)
    {
        var calculatorService = new R365CalculatorService();

        Assert.ThrowsException<InvalidOperationException>(() => calculatorService.Calculate(input));
    }
}