namespace Timeify.Core.Entities.Specifications
{
    public sealed class JobWithTaskSpecification : BaseSpecification<JobEntity>
    {
        public JobWithTaskSpecification(int jobId) : base(u => u.Id.Equals(jobId))
        {
            AddInclude(i => i.JobTasks);
        }
    }
}