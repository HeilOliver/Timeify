using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.JobTask;
using Timeify.Core.Mapper;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.JobTask
{
    [Injectable(typeof(IUpdateJobTaskUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UpdateJobTaskUseCase : IUpdateJobTaskUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;
        private readonly IMapper mapper;
        private readonly IJobTaskRepository taskRepository;

        public UpdateJobTaskUseCase(IJobTaskRepository taskRepository, IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateJobTaskRequest message, IOutputPort<UpdateJobTaskResponse> outputPort)
        {
            var taskEntity = await taskRepository.GetById(message.TaskId);

            if (taskEntity == null)
            {
                outputPort.Handle(new UpdateJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerId, taskEntity.JobEntityId);
            if (!isOwner)
            {
                outputPort.Handle(new UpdateJobTaskResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            mapper.Map(message, taskEntity);
            await taskRepository.Update(taskEntity);

            outputPort.Handle(new UpdateJobTaskResponse());
            return true;
        }
    }
}