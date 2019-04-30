using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class FinishJobTaskRequest : IUseCaseRequest<FinishJobTaskResponse>
    {
        public FinishJobTaskRequest(int taskId, string callerId)
        {
            TaskId = taskId;
            CallerId = callerId;
        }

        public int TaskId { get; }

        public string CallerId { get; }
    }
}