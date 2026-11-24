using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoExchange.Data;
using CryptoExchange.Events;
using CryptoExchange.EventService;
using CryptoExchange.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualBasic;
namespace CryptoExchange.Jobs;
public class ProccessOutBoxJob
{
    private readonly IServiceScopeFactory _scope;
    private readonly AuditService _service;
    private readonly ILogger<ProccessOutBoxJob> _logger;
    public ProccessOutBoxJob(AuditService service, IServiceScopeFactory scope, ILogger<ProccessOutBoxJob> iogger)
    {
        _service = service;
        _scope = scope;
        _logger = iogger;
    }
    [AutomaticRetry(Attempts = 3)]
    [DisableConcurrentExecution(timeoutInSeconds: 60)]
    [Queue("default")] // can change to another type in further develop
    public async Task ExecuteAsync(CancellationToken token)
    {
        await using var scope = _scope.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<DataBase>();
        var messages = await db.outboxMessages.Where(u => u.ProcessedAt == null).OrderBy(u => u.CreatedAt).Take(100).ToListAsync();
        foreach(var message in messages)
        {
            try
            {
                await ProccessMessageAsync(message, token);
                message.MarkAsProcessed();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to process message ${MessageId}", message.Id);
                message.MarkAsFailed(ex.Message);
            }
            await db.SaveChangesAsync();
        }
    }
    public async Task ProccessMessageAsync(OutboxMessage message, CancellationToken token)
    {
        switch(message.Type)
        {
            case "UserRegistered":
                var userEvent = new UserRegisteredEvent(
                    userId: message.UserId!,
                    //ipAddress: message.IpAddress,
                    // email: message.Email,
                    // publicName: message.
                    // userTag: message.UserTag
                    registrationTime: message.CreatedAt!
                );
                await _service.LogAsync(userEvent,token);
                break;
        
            default:
                _logger.LogWarning("Unknown message type: {Type}", message.Type);
                break;
        }
    }
}