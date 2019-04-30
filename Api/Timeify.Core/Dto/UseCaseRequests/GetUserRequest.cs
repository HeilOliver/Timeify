using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class GetUserRequest : IUseCaseRequest<GetUserResponse>
    {
        public GetUserRequest(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}