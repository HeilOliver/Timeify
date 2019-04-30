using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.User
{
    public interface IGetAllUserUseCase : IUseCaseRequestHandler<GetAllUserRequest, GetAllUserResponse>
    {
    }
}