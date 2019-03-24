using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timeify.Api.Models.Setting;
using Timeify.Api.Presenter;
using Timeify.Api.Shared.Models.Request;
using Timeify.Common.DI;
using Timeify.Core.Interfaces.UseCases;

namespace Timeify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings authSettings;
        private readonly ExchangeRefreshTokenPresenter exchangeRefreshTokenPresenter;
        private readonly IExchangeRefreshTokenUseCase exchangeRefreshTokenUseCase;
        private readonly LoginPresenter loginPresenter;
        private readonly ILoginUseCase loginUseCase;

        public AuthController(ILoginUseCase loginUseCase, LoginPresenter loginPresenter,
            IExchangeRefreshTokenUseCase exchangeRefreshTokenUseCase,
            ExchangeRefreshTokenPresenter exchangeRefreshTokenPresenter, IOptions<AuthSettings> authSettings)
        {
            this.loginUseCase = loginUseCase;
            this.loginPresenter = loginPresenter;
            this.exchangeRefreshTokenUseCase = exchangeRefreshTokenUseCase;
            this.exchangeRefreshTokenPresenter = exchangeRefreshTokenPresenter;
            this.authSettings = authSettings.Value;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await loginUseCase.Handle(
                new Core.Dto.UseCaseRequests.LoginRequest(request.UserName, request.Password,
                    Request.HttpContext.Connection.RemoteIpAddress?.ToString()), loginPresenter);
            return loginPresenter.ContentResult;
        }

        // POST api/auth/refreshtoken
        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] ExchangeRefreshTokenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await exchangeRefreshTokenUseCase.Handle(
                new Core.Dto.UseCaseRequests.ExchangeRefreshTokenRequest(request.AccessToken, request.RefreshToken,
                    authSettings.SecretKey), exchangeRefreshTokenPresenter);
            return exchangeRefreshTokenPresenter.ContentResult;
        }
    }
}