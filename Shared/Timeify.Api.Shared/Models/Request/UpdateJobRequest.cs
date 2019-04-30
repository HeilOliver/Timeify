namespace Timeify.Api.Shared.Models.Request
{
    public class UpdateJobRequest
    {
        public int JobId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}