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
    [Injectable(typeof(IUnFinishJobTaskUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UnFinishJobTaskUseCase : IUnFinishJobTaskUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobTaskRepository jobTaskRepository;
        private readonly ITaskAssignedValidator taskAssignedValidator;

        public UnFinishJobTaskUseCase(
            IJobTaskRepository jobTaskRepository,
            IApplicationErrorFactory applicationErrorFactory,
            ITaskAssignedValidator taskAssignedValidator)
        {
            this.jobTaskRepository = jobTaskRepository;
            this.applicationErrorFactory = applicationErrorFactory;
            this.taskAssignedValidator = taskAssignedValidator;
        }

        public async Task<bool> Handle(UnFinishJobTaskRequest message, IOutputPort<UnFinishJobTaskResponse> outputPort)
        {
            var taskEntity = await jobTaskRepository
                .GetSingleBySpec(new GetJobTaskSpecification(message.TaskId));
            bool isAssigned = await taskAssignedValidator.IsAssigned(message.CallerId, message.TaskId);

            if (taskEntity == null)
            {
                outputPort.Handle(new UnFinishJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            if (!isAssigned)
            {
                outputPort.Handle(new UnFinishJobTaskResponse(new[]
                    {applicationErrorFactory.ChangeNotAllowed}));
                return false;
            }

            taskEntity.Finished = false;
            await jobTaskRepository.Update(taskEntity);

            outputPort.Handle(new UnFinishJobTaskResponse());
            return true;
        }
    }
}