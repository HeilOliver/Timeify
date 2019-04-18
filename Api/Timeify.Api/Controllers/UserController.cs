using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Presenter;
using Timeify.Api.Shared.Models;
using Timeify.Api.Shared.Models.Response;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Interfaces.UseCases;

namespace Timeify.Api.Controllers
{
    [Authorize(Roles = "api_access, user")]
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UserController
    {
        private readonly IGetAllUserUseCase getAllUser;
        private readonly GetAllUserPresenter allUserPresenter;

        public UserController(IGetAllUserUseCase getAllUser, GetAllUserPresenter allUserPresenter)
        {
            this.getAllUser = getAllUser;
            this.allUserPresenter = allUserPresenter;
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsers()
        {
            await getAllUser.Handle(new GetAllUserRequest(), allUserPresenter);

            return allUserPresenter.ContentResult;
        }
    }
}