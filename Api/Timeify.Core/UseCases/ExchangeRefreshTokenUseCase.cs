using System.Linq;
using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities.Specifications;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases
{
    [Injectable(typeof(IExchangeRefreshTokenUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public sealed class ExchangeRefreshTokenUseCase : IExchangeRefreshTokenUseCase
    {
        private readonly IJwtFactory jwtFactory;
        private readonly IJwtTokenValidator jwtTokenValidator;
        private readonly ITokenFactory tokenFactory;
        private readonly IUserRepository userRepository;


        public ExchangeRefreshTokenUseCase(IJwtTokenValidator jwtTokenValidator, IUserRepository userRepository,
            IJwtFactory jwtFactory, ITokenFactory tokenFactory)
        {
            this.jwtTokenValidator = jwtTokenValidator;
            this.userRepository = userRepository;
            this.jwtFactory = jwtFactory;
            this.tokenFactory = tokenFactory;
        }

        public async Task<bool> Handle(ExchangeRefreshTokenRequest message,
            IOutputPort<ExchangeRefreshTokenResponse> outputPort)
        {
            var cp = jwtTokenValidator.GetPrincipalFromToken(message.AccessToken, message.SigningKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (cp != null)
            {
                var id = cp.Claims.First(c => c.Type == "id");
                var user = await userRepository.GetSingleBySpec(new UserSpecification(id.Value));

                if (user.HasValidRefreshToken(message.RefreshToken))
                {
                    var jwtToken = await jwtFactory.GenerateEncodedToken(user.IdentityId, user.UserName);
                    string refreshToken = tokenFactory.GenerateToken();
                    user.RemoveRefreshToken(message.RefreshToken); // delete the token we've exchanged
                    user.AddRefreshToken(refreshToken, user.Id, ""); // add the new one
                    await userRepository.Update(user);
                    outputPort.Handle(new ExchangeRefreshTokenResponse(jwtToken, refreshToken, true));
                    return true;
                }
            }

            outputPort.Handle(new ExchangeRefreshTokenResponse(false, "Invalid token."));
            return false;
        }
    }
}