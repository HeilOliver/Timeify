using System;

namespace Timeify.Api.Shared.Models.Request
{
    public class CreateJobTaskRequest
    {
        public int JobId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FinishDate { get; set; }
    }
}