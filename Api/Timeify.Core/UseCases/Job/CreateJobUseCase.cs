using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.Job;

namespace Timeify.Core.UseCases.Job
{
    [Injectable(typeof(ICreateJobUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class CreateJobUseCase : ICreateJobUseCase
    {
        private readonly IJobRepository jobRepository;

        public CreateJobUseCase(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public async Task<bool> Handle(CreateJobRequest message, IOutputPort<CreateJobResponse> outputPort)
        {
            var createdJobResponse = await jobRepository.Create(message.Name, message.Description, message.OwnerId);

            if (!createdJobResponse.Success)
            {
                outputPort.Handle(new CreateJobResponse(createdJobResponse.Errors));
                return false;
            }

            outputPort.Handle(new CreateJobResponse(createdJobResponse.Id));
            return true;
        }
    }
}