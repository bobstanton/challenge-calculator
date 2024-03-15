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
        var calculatorService = new R365CalculatorService();

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
        var calculatorService = new R365CalculatorService();

        var actual = calculatorService.Calculate(input);

        Assert.AreEqual(0, actual);
    }

}