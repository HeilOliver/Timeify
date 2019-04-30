using System.Threading.Tasks;

namespace Timeify.Core.Services
{
    public interface IJobOwnerShipValidator
    {
        Task<bool> IsJobOwner(string username, int jobId);
    }
}