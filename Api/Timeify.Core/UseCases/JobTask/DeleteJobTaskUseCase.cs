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
    [Injectable(typeof(IDeleteJobTaskUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class DeleteJobTaskUseCase : IDeleteJobTaskUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;
        private readonly IJobTaskRepository taskRepository;

        public DeleteJobTaskUseCase(IJobTaskRepository taskRepository, IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory)
        {
            this.taskRepository = taskRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
        }

        public async Task<bool> Handle(DeleteJobTaskRequest message, IOutputPort<DeleteJobTaskResponse> outputPort)
        {
            var taskEntity = await taskRepository.GetById(message.TaskId);

            if (taskEntity == null)
            {
                outputPort.Handle(new DeleteJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerId, taskEntity.JobEntityId);
            if (!isOwner)
            {
                outputPort.Handle(new DeleteJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            await taskRepository.Delete(taskEntity);

            outputPort.Handle(new DeleteJobTaskResponse());
            return true;
        }
    }
}