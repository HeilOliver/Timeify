using System.Linq;
using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities;
using Timeify.Core.Entities.Specifications;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.JobTask;
using Timeify.Core.Mapper;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.JobTask
{
    [Injectable(typeof(IGetJobTaskForJobUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class GetTasksForJobUseCase : IGetJobTaskForJobUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobRepository jobRepository;
        private readonly IMapper mapper;

        public GetTasksForJobUseCase(IJobRepository jobRepository, IApplicationErrorFactory applicationErrorFactory,
            IMapper mapper)
        {
            this.jobRepository = jobRepository;
            this.applicationErrorFactory = applicationErrorFactory;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(GetJobTaskForJobRequest message,
            IOutputPort<GetJobTaskForJobResponse> outputPort)
        {
            var loadedJob = await jobRepository.GetSingleBySpec(new JobWithTaskSpecification(message.JobId));

            if (loadedJob == null)
            {
                outputPort.Handle(new GetJobTaskForJobResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            var jobTasks = loadedJob.JobTasks
                .Select(task => mapper.MapFrom<JobTaskEntity, Api.Shared.Models.Response.JobTask>(task))
                .ToList();

            outputPort.Handle(new GetJobTaskForJobResponse(jobTasks));
            return true;
        }
    }
}