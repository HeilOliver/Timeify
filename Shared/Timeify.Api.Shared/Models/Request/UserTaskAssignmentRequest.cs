namespace Timeify.Api.Shared.Models.Request
{
    public class UserTaskAssignmentRequest
    {
        public int TaskId { get; set; }

        public string Username { get; set; }
    }
}