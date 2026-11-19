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
namespace CryptoExchange.Features.User;
public class RegisterHandler : IRequestHandler<RecordRegister,BaseResponse<UserDto>>
{
    private readonly DataBase db;
    public RegisterHandler(DataBase _db) => db = _db;
    public async Task<BaseResponse< UserDto>> Handle(RecordRegister record, CancellationToken cancellationToken)
    {
        var user = await db.authUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Login.Value == record.Login.Value,cancellationToken);
        if( user != null) throw new ValidationException("Login is already used");
        var password = BCrypt.Net.BCrypt.HashPassword(record.Password.Value);
        var userModel = new UserDomain(
    record.email,
    record.Name,
    record.UserTag,
    false
);


    }
}