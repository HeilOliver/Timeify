using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.Job;
using Timeify.Core.Mapper;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.Job
{
    [Injectable(typeof(IUpdateJobUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class UpdateJobUseCase : IUpdateJobUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;
        private readonly IJobRepository jobRepository;
        private readonly IMapper mapper;

        public UpdateJobUseCase(IJobRepository jobRepository, IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory, IMapper mapper)
        {
            this.jobRepository = jobRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateJobRequest message, IOutputPort<UpdateJobResponse> outputPort)
        {
            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerUserId, message.Id);
            var loadedJob = await jobRepository.GetById(message.Id);

            if (loadedJob == null)
            {
                outputPort.Handle(new UpdateJobResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            if (!isOwner)
            {
                outputPort.Handle(new UpdateJobResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            mapper.Map(message, loadedJob);
            await jobRepository.Update(loadedJob);

            outputPort.Handle(new UpdateJobResponse());
            return true;
        }
    }
}