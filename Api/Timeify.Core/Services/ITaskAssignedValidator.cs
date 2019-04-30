using System.Threading.Tasks;

namespace Timeify.Core.Services
{
    public interface ITaskAssignedValidator
    {
        Task<bool> IsAssigned(string username, int taskId);
    }
}