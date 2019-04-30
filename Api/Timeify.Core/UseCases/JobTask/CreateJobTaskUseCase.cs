using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.JobTask;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.JobTask
{
    [Injectable(typeof(ICreateJobTaskUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class CreateJobTaskUseCase : ICreateJobTaskUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;
        private readonly IJobRepository jobRepository;

        public CreateJobTaskUseCase(IJobRepository jobRepository, IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory)
        {
            this.jobRepository = jobRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
        }

        public async Task<bool> Handle(CreateJobTaskRequest message, IOutputPort<CreateJobTaskResponse> outputPort)
        {
            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerId, message.JobId);
            var loadedJob = await jobRepository.GetById(message.JobId);

            if (loadedJob == null)
            {
                outputPort.Handle(new CreateJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            if (!isOwner)
            {
                outputPort.Handle(new CreateJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            var taskEntity = loadedJob.AddTask(message.Name, message.Description, message.FinishDate);
            await jobRepository.Update(loadedJob);

            outputPort.Handle(new CreateJobTaskResponse(taskEntity.Id));
            return true;
        }
    }
}