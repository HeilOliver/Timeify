using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases
{
    public interface IGetAllUserUseCase : IUseCaseRequestHandler<GetAllUserRequest, GetAllUserResponse>
    {
        
    }
}