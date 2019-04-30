using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities.Specifications;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.JobTask;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.JobTask
{
    [Injectable(typeof(IUnAssignJobTaskUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UnAssignJobTaskUseCase : IUnAssignJobTaskUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;
        private readonly IJobTaskRepository jobTaskRepository;

        public UnAssignJobTaskUseCase(
            IJobTaskRepository jobTaskRepository,
            IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory)
        {
            this.jobTaskRepository = jobTaskRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
        }

        public async Task<bool> Handle(UnAssignJobTaskRequest message, IOutputPort<UnAssignJobTaskResponse> outputPort)
        {
            var taskEntity = await jobTaskRepository.GetSingleBySpec(new GetJobTaskSpecification(message.TaskId));

            if (taskEntity == null)
            {
                outputPort.Handle(new UnAssignJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerId, taskEntity.JobEntityId);
            if (!isOwner)
            {
                outputPort.Handle(new UnAssignJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            taskEntity.UnAssignUser(message.AssignUsername);
            await jobTaskRepository.Update(taskEntity);

            outputPort.Handle(new UnAssignJobTaskResponse());
            return true;
        }
    }
}