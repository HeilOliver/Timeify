using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.User
{
    public interface IGetUserUseCase : IUseCaseRequestHandler<GetUserRequest, GetUserResponse>
    {
    }
}