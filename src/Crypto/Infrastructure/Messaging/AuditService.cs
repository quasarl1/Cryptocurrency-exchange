using CryptoExchange.Data;
using CryptoExchange.Events;
using CryptoExchange.Models;
using Dapper;
using Hangfire.Storage.Monitoring;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace CryptoExchange.EventService;
public class AuditService
{
    private readonly DataBase _db;
    
    public AuditService(DataBase db) => _db = db;
    public async Task LogAsync<T>(T audit,CancellationToken token) where T : IAuditableInterface
    {
        switch(audit.Action)
        {
            case "UserRegistered":
                {
                 var auditLog = new UserAuditLogs
           (
            userId: audit.UserId,
            action: audit.Action,
            ipAddress: audit.IpAddress 
            );
            await _db.logs.AddAsync(auditLog);
            await _db.SaveChangesAsync();
            return;
                }
        }
    }
}