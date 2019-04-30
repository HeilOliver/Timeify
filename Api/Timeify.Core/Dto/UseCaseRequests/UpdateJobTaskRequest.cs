using System;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class UpdateJobTaskRequest : IUseCaseRequest<UpdateJobTaskResponse>
    {
        public UpdateJobTaskRequest(int taskId, string name, string description, DateTime finishDate, string callerId)
        {
            TaskId = taskId;
            Name = name;
            Description = description;
            FinishDate = finishDate;
            CallerId = callerId;
        }

        public int TaskId { get; }

        public string Name { get; }

        public string Description { get; }

        public DateTime FinishDate { get; }

        public string CallerId { get; }
    }
}