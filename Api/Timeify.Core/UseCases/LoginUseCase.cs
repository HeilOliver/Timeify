using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases
{
    [Injectable(typeof(ILoginUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public sealed class LoginUseCase : ILoginUseCase
    {
        private readonly IJwtFactory jwtFactory;
        private readonly ITokenFactory tokenFactory;
        private readonly IUserRepository userRepository;

        public LoginUseCase(IUserRepository userRepository, IJwtFactory jwtFactory, ITokenFactory tokenFactory)
        {
            this.userRepository = userRepository;
            this.jwtFactory = jwtFactory;
            this.tokenFactory = tokenFactory;
        }

        public async Task<bool> Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.UserName) && !string.IsNullOrEmpty(message.Password))
            {
                // ensure we have a user with the given user name
                var user = await userRepository.FindByName(message.UserName);

                if (user != null)
                    if (await userRepository.CheckPassword(user, message.Password))
                    {
                        // generate refresh token
                        string refreshToken = tokenFactory.GenerateToken();
                        user.AddRefreshToken(refreshToken, user.Id, message.RemoteIpAddress);
                        await userRepository.Update(user);

                        // generate access token
                        outputPort.Handle(new LoginResponse(
                            await jwtFactory.GenerateEncodedToken(user.IdentityId, user.UserName), refreshToken, true));
                        return true;
                    }
            }

            outputPort.Handle(new LoginResponse(new[]
                {new ApplicationError("login_failure", "Invalid username or password.")}));
            return false;
        }
    }
}