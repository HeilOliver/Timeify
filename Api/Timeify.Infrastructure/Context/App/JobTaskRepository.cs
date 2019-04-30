using Timeify.Common.DI;
using Timeify.Core.Entities;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Infrastructure.Context.App
{
    [Injectable(typeof(IJobTaskRepository), InjectableAttribute.LifeTimeType.Hierarchical)]
    internal sealed class JobTaskRepository : CrudRepository<JobTaskEntity>, IJobTaskRepository
    {
        public JobTaskRepository(AppDbContext context) : base(context)
        {
        }
    }
}