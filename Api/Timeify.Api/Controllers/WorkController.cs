using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Extensions;
using Timeify.Api.Presenter;
using Timeify.Api.Shared.Models.Request;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Interfaces.UseCases.JobTask;

namespace Timeify.Api.Controllers
{
    [Authorize(Roles = "api_access, user")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class WorkController : Controller
    {
        private readonly IAssignJobTaskUseCase assignJobTask;
        private readonly AssignJobTaskPresenter assignJobTaskPresenter;
        private readonly IFinishJobTaskUseCase finishJobTask;
        private readonly FinishJobTaskPresenter finishJobTaskPresenter;
        private readonly IUnAssignJobTaskUseCase unAssignJobTask;
        private readonly UnAssignJobTaskPresenter unAssignJobTaskPresenter;
        private readonly IUnFinishJobTaskUseCase unFinishJobTask;
        private readonly UnFinishJobTaskPresenter unFinishJobTaskPresenter;

        public WorkController(
            IAssignJobTaskUseCase assignJobTask, AssignJobTaskPresenter assignJobTaskPresenter,
            IUnAssignJobTaskUseCase unAssignJobTask, UnAssignJobTaskPresenter unAssignJobTaskPresenter,
            IFinishJobTaskUseCase finishJobTask, FinishJobTaskPresenter finishJobTaskPresenter,
            IUnFinishJobTaskUseCase unFinishJobTask, UnFinishJobTaskPresenter unFinishJobTaskPresenter)
        {
            this.assignJobTask = assignJobTask;
            this.assignJobTaskPresenter = assignJobTaskPresenter;
            this.unAssignJobTask = unAssignJobTask;
            this.unAssignJobTaskPresenter = unAssignJobTaskPresenter;
            this.finishJobTask = finishJobTask;
            this.finishJobTaskPresenter = finishJobTaskPresenter;
            this.unFinishJobTask = unFinishJobTask;
            this.unFinishJobTaskPresenter = unFinishJobTaskPresenter;
        }

        [HttpPost]
        [Authorize(Roles = "contributor")]
        [Route("api/[controller]/FinishTask")]
        public async Task<IActionResult> FinishTask(FinishTaskRequest request)
        {
            await finishJobTask.Handle(
                new FinishJobTaskRequest(request.TaskId, this.GetUsername()), finishJobTaskPresenter);

            return finishJobTaskPresenter.ContentResult;
        }

        [HttpPost]
        [Authorize(Roles = "contributor")]
        [Route("api/[controller]/UnFinishTask")]
        public async Task<IActionResult> UnFinishTask(FinishTaskRequest request)
        {
            await unFinishJobTask.Handle(
                new UnFinishJobTaskRequest(request.TaskId, this.GetUsername()), unFinishJobTaskPresenter);

            return unFinishJobTaskPresenter.ContentResult;
        }

        [HttpPost]
        [Route("api/[controller]/AssignJobTask")]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> AssignUserToJobTask(UserTaskAssignmentRequest request)
        {
            await assignJobTask.Handle(
                new AssignJobTaskRequest(request.Username, request.TaskId, this.GetUsername()), assignJobTaskPresenter);

            return assignJobTaskPresenter.ContentResult;
        }

        [HttpPost]
        [Route("api/[controller]/UnAssignJobTask")]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> UnAssignUserFromJobTask(UserTaskAssignmentRequest request)
        {
            await unAssignJobTask.Handle(
                new UnAssignJobTaskRequest(request.Username, request.TaskId, this.GetUsername()),
                unAssignJobTaskPresenter);

            return unAssignJobTaskPresenter.ContentResult;
        }
    }
}