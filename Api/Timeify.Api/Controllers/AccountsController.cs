using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Presenter;
using Timeify.Api.Shared.Models.Request;
using Timeify.Common.DI;
using Timeify.Core.Interfaces.UseCases;

namespace Timeify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class AccountsController : ControllerBase
    {
        private readonly RegisterUserPresenter registerUserPresenter;
        private readonly IRegisterUserUseCase registerUserUseCase;

        public AccountsController(IRegisterUserUseCase registerUserUseCase, RegisterUserPresenter registerUserPresenter)
        {
            this.registerUserUseCase = registerUserUseCase;
            this.registerUserPresenter = registerUserPresenter;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await registerUserUseCase.Handle(
                new Core.Dto.UseCaseRequests.RegisterUserRequest(request.FirstName, request.LastName, request.Email,
                    request.UserName, request.Password), registerUserPresenter);

            return registerUserPresenter.ContentResult;
        }
    }
}