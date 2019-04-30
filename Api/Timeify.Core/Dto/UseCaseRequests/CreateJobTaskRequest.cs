using System;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class CreateJobTaskRequest : IUseCaseRequest<CreateJobTaskResponse>
    {
        public CreateJobTaskRequest(int jobId, string name, string description, DateTime finishDate, string callerId)
        {
            JobId = jobId;
            Name = name;
            Description = description;
            FinishDate = finishDate;
            CallerId = callerId;
        }

        public int JobId { get; }

        public string Name { get; }

        public string Description { get; }

        public DateTime FinishDate { get; }

        public string CallerId { get; }
    }
}