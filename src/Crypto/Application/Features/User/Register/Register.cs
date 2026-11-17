using System.Reflection.Metadata;
using CryptoExchange.Common.Response;
using CryptoExchange.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace CryptoExchange.Features.User;
/* public class RegisterHandler : IRequestHandler<RecordRegister,BaseResponse<UserDto>>
{
    private readonly Database db;
    public RegisterHandler(Database _db) => db = _db;
    public async Task<BaseResponse< UserDto>> Handle(RecordRegister record, CancellationToken cancellationToken)
}
*/