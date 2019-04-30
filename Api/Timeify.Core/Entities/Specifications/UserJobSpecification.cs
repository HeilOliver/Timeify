namespace Timeify.Core.Entities.Specifications
{
    public sealed class UserJobSpecification : BaseSpecification<JobEntity>
    {
        public UserJobSpecification(string identityId) : base(u => u.OwnerId.Equals(identityId))
        {
        }
    }
}