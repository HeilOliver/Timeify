using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class UpdateJobRequest : IUseCaseRequest<UpdateJobResponse>
    {
        public UpdateJobRequest(int id, string name, string description, string callerUserId)
        {
            Id = id;
            Name = name;
            Description = description;
            CallerUserId = callerUserId;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string CallerUserId { get; }
    }
}