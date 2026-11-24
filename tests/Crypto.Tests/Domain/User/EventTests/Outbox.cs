using CryptoExchange;
using CryptoExchange.Data;
using CryptoExchange.Jobs;
using CryptoExchange.Models;
using Hangfire.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
namespace Crypto.Tests;
public class OutboxTests(   )
{

    [Fact]
    public async Task OutboxShouldProcessPendingOutboxMessages()
    {
        var options = new DbContextOptionsBuilder<DataBase>()
            .UseNpgsql("Host=localhost;Database=my_test_db;Username=postgres;Password=123")
            .Options;
        await using var db =new DataBase(options);

        var mediatorMock = new Mock<IMediator>();
    }
}