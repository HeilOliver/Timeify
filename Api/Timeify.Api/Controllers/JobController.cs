using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Extensions;
using Timeify.Api.Presenter;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Interfaces.UseCases.Job;
using CreateJobRequest = Timeify.Api.Shared.Models.Request.CreateJobRequest;
using DeleteJobRequest = Timeify.Api.Shared.Models.Request.DeleteJobRequest;
using UpdateJobRequest = Timeify.Api.Shared.Models.Request.UpdateJobRequest;

namespace Timeify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class JobController : Controller
    {
        private readonly CreateJobPresenter createJobPresenter;
        private readonly ICreateJobUseCase createJobUseCase;
        private readonly DeleteJobPresenter deleteJobPresenter;
        private readonly IDeleteJobUseCase deleteJobUseCase;
        private readonly GetAllJobPresenter getAllJobPresenter;
        private readonly IGetAllJobsUseCase getAllJobsUseCase;
        private readonly UpdateJobPresenter updateJobPresenter;
        private readonly IUpdateJobUseCase updateJobUseCase;

        public JobController(
            ICreateJobUseCase createJobUseCase, CreateJobPresenter createJobPresenter,
            IUpdateJobUseCase updateJobUseCase, UpdateJobPresenter updateJobPresenter,
            IDeleteJobUseCase deleteJobUseCase, DeleteJobPresenter deleteJobPresenter,
            IGetAllJobsUseCase getAllJobsUseCase, GetAllJobPresenter getAllJobPresenter)
        {
            this.createJobUseCase = createJobUseCase;
            this.createJobPresenter = createJobPresenter;
            this.updateJobUseCase = updateJobUseCase;
            this.updateJobPresenter = updateJobPresenter;
            this.deleteJobUseCase = deleteJobUseCase;
            this.deleteJobPresenter = deleteJobPresenter;
            this.getAllJobsUseCase = getAllJobsUseCase;
            this.getAllJobPresenter = getAllJobPresenter;
        }

        [HttpPut]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Create([FromBody] CreateJobRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await createJobUseCase.Handle(
                new Core.Dto.UseCaseRequests.CreateJobRequest(req.Name, req.Description,
                    this.GetUsername()), createJobPresenter);

            return createJobPresenter.ContentResult;
        }

        [HttpPost]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Update([FromBody] UpdateJobRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await updateJobUseCase.Handle(
                new Core.Dto.UseCaseRequests.UpdateJobRequest(req.JobId, req.Name, req.Description, this.GetUsername()),
                updateJobPresenter);

            return updateJobPresenter.ContentResult;
        }

        [HttpDelete]
        [Authorize(Roles = "creator")]
        public async Task<IActionResult> Delete([FromBody] DeleteJobRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await deleteJobUseCase.Handle(
                new Core.Dto.UseCaseRequests.DeleteJobRequest(req.JobId, this.GetUsername()), deleteJobPresenter);

            return deleteJobPresenter.ContentResult;
        }

        [HttpGet]
        [Authorize(Roles = "creator, contributor")]
        public async Task<IActionResult> GetAll()
        {
            await getAllJobsUseCase.Handle(
                new GetAllJobRequest(1, int.MaxValue, this.GetUsername()), getAllJobPresenter);

            return getAllJobPresenter.ContentResult;
        }
    }
}