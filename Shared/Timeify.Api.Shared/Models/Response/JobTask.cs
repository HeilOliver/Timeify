using System;

namespace Timeify.Api.Shared.Models.Response
{
    public class JobTask
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FinishDate { get; set; }
    }
}