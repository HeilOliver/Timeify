using System.Threading.Tasks;
using Timeify.Api.Shared.Models.Response;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.User;
using Timeify.Core.Mapper;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.User
{
    [Injectable(typeof(IGetUserUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IApplicationErrorFactory errorFactory;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public GetUserUseCase(IUserRepository userRepository, IMapper mapper, IApplicationErrorFactory errorFactory)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.errorFactory = errorFactory;
        }

        public async Task<bool> Handle(GetUserRequest message, IOutputPort<GetUserResponse> outputPort)
        {
            var userEntities = await userRepository.FindByName(message.Username);

            // If no User is found / should never happen because we got this from claim
            if (userEntities == null)
            {
                outputPort.Handle(new GetUserResponse(new[]
                    {errorFactory.InvalidUser}));
                return false;
            }

            var completeUser = mapper
                .MapFrom<UserEntity, CompleteUser>(userEntities);

            outputPort.Handle(new GetUserResponse(completeUser));
            return true;
        }
    }
}