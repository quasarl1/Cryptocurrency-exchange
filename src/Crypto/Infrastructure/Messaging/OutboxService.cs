using System.Net;
using System.Data.Common;
using System.ComponentModel.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoExchange.Data;
using CryptoExchange.Events;
using CryptoExchange.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;

namespace CryptoExchange.EventService;
public class OutboxEventPublisher : IEventPublisher
{
    private readonly DataBase _db;
    public OutboxEventPublisher(DataBase db)
    {
        _db = db;
    }
    public async Task PublishAsync(UserRegisteredEvent domainEvent, CancellationToken ct = default)
    {
        await _db.outboxMessages.AddAsync(new OutboxMessage 
        (
            userId: domainEvent.UserId,
            type: "UserRegistered" 
        ));
        await _db.SaveChangesAsync();
        await Task.CompletedTask;
        return;
    }
}