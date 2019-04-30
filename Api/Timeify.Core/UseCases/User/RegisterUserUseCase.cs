using System.Linq;
using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.User;

namespace Timeify.Core.UseCases.User
{
    [Injectable(typeof(IRegisterUserUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public sealed class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterUserRequest message, IOutputPort<RegisterUserResponse> outputPort)
        {
            var response = await userRepository
                .Create(message.FirstName, message.LastName, message.Email, message.UserName, message.Password);

            outputPort
                .Handle(response.Success
                    ? new RegisterUserResponse(response.Id, true)
                    : new RegisterUserResponse(response.Errors.Select(e => e.Description)));
            return response.Success;
        }
    }
}