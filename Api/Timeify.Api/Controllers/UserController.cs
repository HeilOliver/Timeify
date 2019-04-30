using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Presenter;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces.UseCases.User;

namespace Timeify.Api.Controllers
{
    [Authorize(Roles = "api_access, user")]
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UserController
    {
        private readonly GetAllUserPresenter allUserPresenter;
        private readonly IGetAllUserUseCase getAllUser;

        public UserController(IGetAllUserUseCase getAllUser, GetAllUserPresenter allUserPresenter)
        {
            this.getAllUser = getAllUser;
            this.allUserPresenter = allUserPresenter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetUserResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            await getAllUser.Handle(new GetAllUserRequest(), allUserPresenter);
            return allUserPresenter.ContentResult;
        }
    }
}