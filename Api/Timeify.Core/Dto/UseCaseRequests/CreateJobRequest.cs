using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class CreateJobRequest : IUseCaseRequest<CreateJobResponse>
    {
        public CreateJobRequest(string name, string description, string ownerId)
        {
            Name = name;
            Description = description;
            OwnerId = ownerId;
        }

        public string Name { get; }

        public string Description { get; }

        public string OwnerId { get; }
    }
}