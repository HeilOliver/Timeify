using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class UnFinishJobTaskRequest : IUseCaseRequest<UnFinishJobTaskResponse>
    {
        public UnFinishJobTaskRequest(int taskId, string callerId)
        {
            TaskId = taskId;
            CallerId = callerId;
        }

        public int TaskId { get; }

        public string CallerId { get; }
    }
}