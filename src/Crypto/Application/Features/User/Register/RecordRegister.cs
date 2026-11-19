using CryptoExchange.Common.Response;
using CryptoExchange.Models;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
namespace CryptoExchange.Features;
public record RecordRegister(Password Password, Login Login,PublicName Name, UserTag UserTag, Email email) : IRequest<BaseResponse<UserDto>> {}