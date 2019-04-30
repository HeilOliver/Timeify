using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Dto.GatewayResponses;
using Timeify.Core.Entities;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Infrastructure.Context.App
{
    [Injectable(typeof(IJobRepository), InjectableAttribute.LifeTimeType.Hierarchical)]
    internal sealed class JobRepository : CrudRepository<JobEntity>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CreateJobResponse> Create(string name, string description, string ownerId)
        {
            var jobEntity = new JobEntity(name, description, ownerId);

            Context.Jobs.Add(jobEntity);
            await Context.SaveChangesAsync();

            int newJobId = jobEntity.Id;

            return new CreateJobResponse(newJobId, true);
        }
    }
}