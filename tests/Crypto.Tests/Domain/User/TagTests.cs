using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using CryptoExchange;
using FluentAssertions;
using Xunit;
using CryptoExchange.Models;

namespace Crypto.Tests.Domain;
public class TagTests
{
    // test for checking the tag is null => ("")

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Tag_Should_Reject_Empty(string? input) =>
    Assert.Throws<ValidationException>(()=> new UserTag(input!));


    [Theory]
    // [InlineData("TagForTestingThis")] // 17 symbols
    [InlineData("ForTestingThisBeautifulTagThatIneverHaveSeen")]//44 symbols
    public void Tag_Should_Reject_Reserved(string reserved) =>
    Assert.Throws<ValidationException>(()=> new UserTag(reserved));
};