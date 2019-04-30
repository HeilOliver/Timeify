using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Interfaces.UseCases.Job;
using Timeify.Core.Services;

namespace Timeify.Core.UseCases.Job
{
    [Injectable(typeof(IDeleteJobUseCase), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class DeleteJobUseCase : IDeleteJobUseCase
    {
        private readonly IApplicationErrorFactory applicationErrorFactory;
        private readonly IJobOwnerShipValidator jobOwnerShipValidator;

        private readonly IJobRepository jobRepository;

        public DeleteJobUseCase(IJobRepository jobRepository, IJobOwnerShipValidator jobOwnerShipValidator,
            IApplicationErrorFactory applicationErrorFactory)
        {
            this.jobRepository = jobRepository;
            this.jobOwnerShipValidator = jobOwnerShipValidator;
            this.applicationErrorFactory = applicationErrorFactory;
        }

        public async Task<bool> Handle(DeleteJobRequest message, IOutputPort<DeleteJobResponse> outputPort)
        {
            bool isOwner = await jobOwnerShipValidator.IsJobOwner(message.CallerId, message.JobId);
            var loadedJob = await jobRepository.GetById(message.JobId);

            if (loadedJob == null)
            {
                outputPort.Handle(new DeleteJobResponse(new[]
                    {applicationErrorFactory.ResourceNotFound}));
                return false;
            }

            if (!isOwner)
            {
                outputPort.Handle(new DeleteJobResponse(new[]
                    {applicationErrorFactory.ResourceNotOwned}));
                return false;
            }

            await jobRepository.Delete(loadedJob);

            outputPort.Handle(new DeleteJobResponse());
            return true;
        }
    }
}