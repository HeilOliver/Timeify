using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class DeleteJobTaskRequest : IUseCaseRequest<DeleteJobTaskResponse>
    {
        public DeleteJobTaskRequest(int taskId, string callerId)
        {
            TaskId = taskId;
            CallerId = callerId;
        }

        public int TaskId { get; }

        public string CallerId { get; }
    }
}