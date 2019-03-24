namespace Timeify.Core.Entities.Specifications
{
    public sealed class UserSpecification : BaseSpecification<UserEntity>
    {
        public UserSpecification(string identityId) : base(u => u.IdentityId == identityId)
        {
            AddInclude(u => u.RefreshTokens);
        }
    }
}