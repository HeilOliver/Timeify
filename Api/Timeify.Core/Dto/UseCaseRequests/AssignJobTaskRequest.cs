using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class AssignJobTaskRequest : IUseCaseRequest<AssignJobTaskResponse>
    {
        public AssignJobTaskRequest(string assignUsername, int taskId, string callerId)
        {
            AssignUsername = assignUsername;
            TaskId = taskId;
            CallerId = callerId;
        }

        public string AssignUsername { get; }

        public int TaskId { get; }

        public string CallerId { get; }
    }
}