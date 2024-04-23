using System.Text.RegularExpressions;
using NetworkTool.Utilities;

namespace NetworkTool.Tests;

[TestClass]
public class RegexHelperTests
{
    [TestMethod]
    public void IPv4AddressRegex_ShouldValidateIPv4Address()
    {
        var inputs = new[]
        {
            "192.168.1.1",
            "10.0.0.255",
            "172.16.42.99"
        };

        foreach (var input in inputs) Assert.IsTrue(Regex.IsMatch(input, RegexHelper.IPv4AddressRegex));
    }

    [TestMethod]
    public void IPv4AddressRegex_ShouldNotValidateInvalidIPv4Address()
    {
        var inputs = new[]
        {
            "192.168.1.256",
            "10.0.0.256",
            "172.16.42.999",
            "text",
            "192.168.1."
        };

        foreach (var input in inputs) Assert.IsFalse(Regex.IsMatch(input, RegexHelper.IPv4AddressRegex));
    }

    [TestMethod]
    public void MACAddressRegex_ShouldValidateMACAddress()
    {
        var inputs = new[]
        {
            "00:11:22:33:44:55",
            "FF:FF:FF:FF:FF:FF",
            "00-0F-66-E3-1B-9C"
        };

        foreach (var input in inputs) Assert.IsTrue(Regex.IsMatch(input, RegexHelper.MACAddressRegex));
    }

    [TestMethod]
    public void MACAddressRegex_ShouldNotValidateInvalidMACAddress()
    {
        var inputs = new[]
        {
            "00:11:22:33:44:",
            "GG:HH:11:22:33:44",
            "text",
            "00-11-22-33-44-55-66"
        };

        foreach (var input in inputs) Assert.IsFalse(Regex.IsMatch(input, RegexHelper.MACAddressRegex));
    }
}