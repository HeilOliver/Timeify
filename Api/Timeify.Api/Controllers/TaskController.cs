using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Extensions;
using Timeify.Api.Presenter;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Interfaces.UseCases.JobTask;
using CreateJobTaskRequest = Timeify.Api.Shared.Models.Request.CreateJobTaskRequest;
using DeleteJobTaskRequest = Timeify.Api.Shared.Models.Request.DeleteJobTaskRequest;
using UpdateJobTaskRequest = Timeify.Api.Shared.Models.Request.UpdateJobTaskRequest;

namespace Timeify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class TaskController : Controller
    {
        private readonly CreateJobTaskPresenter createJobTaskPresenter;
        private readonly ICreateJobTaskUseCase createJobTaskUseCase;
        private readonly DeleteJobTaskPresenter deleteJobTaskPresenter;
        private readonly IDeleteJobTaskUseCase deleteJobTaskUseCase;
        private readonly GetJobTaskForJobPresenter getJobTaskForJobPresenter;
        private readonly IGetJobTaskForJobUseCase getJobTaskForUseCase;
        private readonly UpdateJobTaskPresenter updateJobTaskPresenter;
        private readonly IUpdateJobTaskUseCase updateJobTaskUseCase;

        public TaskController(
            ICreateJobTaskUseCase jobTaskUseCase, CreateJobTaskPresenter jobTaskPresenter,
            IUpdateJobTaskUseCase updateJobTaskUseCase, UpdateJobTaskPresenter updateJobTaskPresenter,
            IDeleteJobTaskUseCase deleteJobTaskUseCase, DeleteJobTaskPresenter deleteJobTaskPresenter,
            IGetJobTaskForJobUseCase jobTaskForUseCase, GetJobTaskForJobPresenter jobTaskForJobPresenter)
        {
            createJobTaskUseCase = jobTaskUseCase;
            createJobTaskPresenter = jobTaskPresenter;
            this.updateJobTaskUseCase = updateJobTaskUseCase;
            this.updateJobTaskPresenter = updateJobTaskPresenter;
            this.deleteJobTaskUseCase = deleteJobTaskUseCase;
            this.deleteJobTaskPresenter = deleteJobTaskPresenter;
            getJobTaskForUseCase = jobTaskForUseCase;
            getJobTaskForJobPresenter = jobTaskForJobPresenter;
        }

        [HttpPut]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Create([FromBody] CreateJobTaskRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await createJobTaskUseCase.Handle(
                new Core.Dto.UseCaseRequests.CreateJobTaskRequest(
                    request.JobId, request.Name, request.Description, request.FinishDate, this.GetUsername()),
                createJobTaskPresenter);

            return createJobTaskPresenter.ContentResult;
        }

        [HttpPost]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Update([FromBody] UpdateJobTaskRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await updateJobTaskUseCase.Handle(
                new Core.Dto.UseCaseRequests.UpdateJobTaskRequest(request.TaskId, request.Name, request.Description,
                    request.FinishDate,
                    this.GetUsername()), updateJobTaskPresenter);

            return updateJobTaskPresenter.ContentResult;
        }


        [HttpDelete]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Delete([FromBody] DeleteJobTaskRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await deleteJobTaskUseCase.Handle(
                new Core.Dto.UseCaseRequests.DeleteJobTaskRequest(request.TaskId, this.GetUsername()),
                deleteJobTaskPresenter);

            return deleteJobTaskPresenter.ContentResult;
        }


        [HttpGet]
        [Authorize(Roles = "creator, contributor")]
        [Route("ForJob")]
        public async Task<IActionResult> GetTasksForJob(int jobId)
        {
            await getJobTaskForUseCase.Handle(
                new GetJobTaskForJobRequest(jobId, this.GetUsername()),
                getJobTaskForJobPresenter);

            return getJobTaskForJobPresenter.ContentResult;
        }
    }
}