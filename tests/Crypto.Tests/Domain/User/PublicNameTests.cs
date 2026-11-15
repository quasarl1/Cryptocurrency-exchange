
using System.ComponentModel.DataAnnotations;
using CryptoExchange;
using FluentAssertions;
using Xunit;

namespace Crypto.Tests.Domain;
public class PublicNameTests
{
        static PublicNameTests()
    {
        PublicName.LoadReserved(new[] {
      "admin",
      "test",
      "fuck",
      "shit",
      "developer"
      });
    }

    [Theory]
    [InlineData("admin")]
    [InlineData("test")]
    [InlineData("fuck")]
    [InlineData("shit")]
    [InlineData("developer")]

    public void PublicName_Should_Reject_Reserved(string reserved) => 
    Assert.Throws<ValidationException>(() => new PublicName(reserved));


    [Fact]
    public void PublicName_Should_Be_Trim() => 
    new PublicName(" PublicName").Value.Should().Be("PublicName");
    

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void PublicName_Should_Reject_Empty(string? input) => 
    Assert.Throws<ValidationException>(() => new PublicName(input!));

}