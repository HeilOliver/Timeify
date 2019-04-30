using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Services;

namespace Timeify.Infrastructure.Services
{
    [Injectable(typeof(IJobOwnerShipValidator), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class JobOwnerShipValidator : IJobOwnerShipValidator
    {
        private readonly IJobRepository jobRepository;

        public JobOwnerShipValidator(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public async Task<bool> IsJobOwner(string username, int jobId)
        {
            var job = await jobRepository.GetById(jobId);

            return job != null && job.OwnerId.Equals(username);
        }
    }
}