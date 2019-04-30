using System.Threading.Tasks;
using Timeify.Core.Dto.GatewayResponses;
using Timeify.Core.Entities;

namespace Timeify.Core.Interfaces.Gateways
{
    public interface IJobRepository : ICrudRepository<JobEntity>
    {
        Task<CreateJobResponse> Create(
            string name,
            string description,
            string ownerId);
    }
}