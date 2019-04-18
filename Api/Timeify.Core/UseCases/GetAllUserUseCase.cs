using System.Linq;
using System.Threading.Tasks;
using Timeify.Api.Shared.Models.Response;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases;

namespace Timeify.Core.UseCases
{
    [Injectable(typeof(IGetAllUserUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class GetAllUserUseCase : IGetAllUserUseCase
    {
        private readonly IUserRepository userRepository;

        public GetAllUserUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<bool> Handle(GetAllUserRequest message, IOutputPort<GetAllUserResponse> outputPort)
        {
            var userEntities = await userRepository.ListAll();

            var allUserResponse = userEntities.Select(user => new GetUserResponse()
            {
                Email = user.Email,
                Username = user.UserName
            }).ToList();

            outputPort.Handle(new GetAllUserResponse(true){Users = allUserResponse});
            return true;
        }
    }
}