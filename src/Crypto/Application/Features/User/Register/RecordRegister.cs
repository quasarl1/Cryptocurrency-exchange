using CryptoExchange.Common.Response;
using CryptoExchange.Models;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
namespace CryptoExchange.Features;
public record RecordRegister(string Password, string Login) : IRequest<BaseResponse<UserDto>> {}