using System;

namespace Timeify.Api.Shared.Models.Request
{
    public class UpdateJobTaskRequest
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FinishDate { get; set; }
    }
}