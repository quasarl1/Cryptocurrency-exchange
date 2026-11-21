using System.Net;
using System.Data.Common;
using System.ComponentModel.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoExchange.Data;
using CryptoExchange.Events;
using CryptoExchange.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace CryptoExchange.EventService;
public class OutboxEventPublisher : IEventPublisher
{
    private readonly DataBase _db;
    public OutboxEventPublisher(DataBase db)
    {
        _db = db;
    }
    public async Task PublishAsync<T>(T domainEvent, CancellationToken ct = default) where T : class
    {
        var message = new OutboxMessage
        (
            type: domainEvent.GetType().Name,
            load: JsonSerializer.Serialize(domainEvent, typeof(T))
            // ipAddress: from middleware 
        );
            await _db.Set<OutboxMessage>().AddAsync(message,ct);
            await Task.CompletedTask;
            return;
    }
}