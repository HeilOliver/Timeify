using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Presenter;
using Timeify.Api.Shared.Models.Response;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Interfaces.UseCases.User;

namespace Timeify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "api_access, user")]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class SessionController : Controller
    {
        private readonly GetUserPresenter getUserPresenter;
        private readonly IGetUserUseCase getUserUseCase;

        public SessionController(IGetUserUseCase getUserUseCase, GetUserPresenter getUserPresenter)
        {
            this.getUserUseCase = getUserUseCase;
            this.getUserPresenter = getUserPresenter;
        }


        [HttpGet]
        [ProducesResponseType(typeof(CompleteUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMe()
        {
            string username =
                HttpContext.User
                    .FindFirst(ClaimTypes.Name).Value;

            await getUserUseCase.Handle(new GetUserRequest(username), getUserPresenter);

            return getUserPresenter.ContentResult;
        }
    }
}