namespace Timeify.Core.Entities.Specifications
{
    public sealed class GetJobTaskSpecification : BaseSpecification<JobTaskEntity>
    {
        public GetJobTaskSpecification(int taskId) : base(u => u.Id.Equals(taskId))
        {
            AddInclude(c => c.AssignedUsers);
        }
    }
}