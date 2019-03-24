using System.Threading.Tasks;
using Timeify.Api.Shared.Auth;
using Timeify.Core.Dto;

namespace Timeify.Core.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}