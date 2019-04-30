using System.Linq;
using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities;
using Timeify.Core.Entities.Specifications;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.Job;
using Timeify.Core.Mapper;

namespace Timeify.Core.UseCases.Job
{
    [Injectable(typeof(IGetAllJobsUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class GetAllJobsUseCase : IGetAllJobsUseCase
    {
        private readonly IJobRepository jobRepository;
        private readonly IMapper mapper;

        public GetAllJobsUseCase(IJobRepository jobRepository, IMapper mapper)
        {
            this.jobRepository = jobRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(GetAllJobRequest message, IOutputPort<GetAllJobResponse> outputPort)
        {
            var jobEntityList = await jobRepository
                .List(new UserJobSpecification(message.CallerId));

            var jobList = jobEntityList
                .Select(entity => mapper.MapFrom<JobEntity, Api.Shared.Models.Response.Job>(entity))
                .AsEnumerable();

            outputPort.Handle(new GetAllJobResponse(jobList));
            return true;
        }
    }
}