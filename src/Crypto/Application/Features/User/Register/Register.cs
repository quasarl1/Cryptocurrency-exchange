using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using CryptoExchange.Common.Response;
using CryptoExchange.Models;
using MediatR;
using CryptoExchange;
using CryptoExchange.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Superpower.Model;
using UserDomain = CryptoExchange.Models.User;
using System.Runtime.CompilerServices;
using CryptoExchange.Events;
using System.Net;
namespace CryptoExchange.Features.User;
public class RegisterHandler : IRequestHandler<RecordRegister,BaseResponse<UserDto>>
{
    private readonly IEventPublisher publisher;
    private readonly DataBase db;
    public RegisterHandler(IEventPublisher _publisher, DataBase _db)
    {
        publisher = _publisher;
        db = _db;
    }
    public async Task<BaseResponse< UserDto>> Handle(RecordRegister record, CancellationToken cancellationToken)
    {
        var user = await db.authUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Login.Value == record.Login.Value,cancellationToken);
        if( user != null) throw new ValidationException("Login is already used");
        var password = BCrypt.Net.BCrypt.HashPassword(record.Password.Value);
        var userModel = new UserDomain
        (
            record.email,
            record.Name,
            record.UserTag,
            false
        );
        var authModel = new AuthUser
        (
            userModel.Id,
            record.Login,
            password
        );
        await db.users.AddAsync(userModel,cancellationToken);
//have to add jwt token 
        await db.authUsers.AddAsync(authModel,cancellationToken);
        await publisher.PublishAsync(new UserRegisteredEvent(userId: user.Id,registrationTime: DateTime.UtcNow), cancellationToken);
        await db.SaveChangesAsync();
        var UserDto = new UserDto
        {
          PublicName = userModel.PublicName.ToString(),
          UserTag = userModel.Tag.ToString(),
          // Email = userModel.email 
          Id = userModel.Id,
          IsBlocked = false,
        };
        return BaseResponse<UserDto>.Ok(UserDto);
    }
}