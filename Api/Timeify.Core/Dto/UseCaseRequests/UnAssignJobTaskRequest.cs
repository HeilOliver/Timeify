using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class UnAssignJobTaskRequest : IUseCaseRequest<UnAssignJobTaskResponse>
    {
        public UnAssignJobTaskRequest(string assignUsername, int taskId, string callerId)
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