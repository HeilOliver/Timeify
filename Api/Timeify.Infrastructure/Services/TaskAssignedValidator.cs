using System.Linq;
using System.Threading.Tasks;
using Timeify.Common.DI;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.Services;

namespace Timeify.Infrastructure.Services
{
    [Injectable(typeof(ITaskAssignedValidator), InjectableAttribute.LifeTimeType.Hierarchical)]
    public class TaskAssignedValidator : ITaskAssignedValidator
    {
        private readonly IJobTaskRepository jobTaskRepository;

        public TaskAssignedValidator(IJobTaskRepository jobTaskRepository)
        {
            this.jobTaskRepository = jobTaskRepository;
        }

        public async Task<bool> IsAssigned(string username, int taskId)
        {
            var jobTask = await jobTaskRepository.GetById(taskId);
            if (jobTask == null) return false;

            return jobTask.AssignedUsers
                .Any(user => user.UserName.Equals(username));
        }
    }
}