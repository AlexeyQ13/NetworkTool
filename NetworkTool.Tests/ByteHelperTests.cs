using NetworkTool.Utilities;

namespace NetworkTool.Tests;

[TestClass]
public class ByteHelperTests
{
    [TestMethod]
    public void Compare_EqualArrays_ReturnsZero()
    {
        byte[] x = { 1, 2, 3 };
        byte[] y = { 1, 2, 3 };

        var result = ByteHelper.Compare(x, y);

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Compare_FirstArrayLarger_ReturnsPositive()
    {
        byte[] x = { 1, 2, 3, 4 };
        byte[] y = { 1, 2, 3 };

        var result = ByteHelper.Compare(x, y);

        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void Compare_SecondArrayLarger_ReturnsNegative()
    {
        byte[] x = { 1, 2, 3 };
        byte[] y = { 1, 2, 3, 4 };

        var result = ByteHelper.Compare(x, y);

        Assert.IsTrue(result < 0);
    }

    [TestMethod]
    public void Compare_ArraysDifferAtPosition_ReturnsDifference()
    {
        byte[] x = { 1, 2, 4 };
        byte[] y = { 1, 2, 3 };

        var expected = x[2] - y[2];
        var result = ByteHelper.Compare(x, y);

        Assert.AreEqual(expected, result);
    }
}