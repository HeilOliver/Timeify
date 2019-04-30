using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.Auth
{
    public interface
        IExchangeRefreshTokenUseCase : IUseCaseRequestHandler<ExchangeRefreshTokenRequest, ExchangeRefreshTokenResponse>
    {
    }
}