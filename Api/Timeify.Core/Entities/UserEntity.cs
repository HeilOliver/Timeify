using System;
using System.Collections.Generic;
using System.Linq;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        private readonly List<JobTaskUserEntity> assignedTasks = new List<JobTaskUserEntity>();
        private readonly List<RefreshTokenEntity> refreshTokens = new List<RefreshTokenEntity>();

        internal UserEntity()
        {
            /* Required by EF */
        }

        public UserEntity(string firstName, string lastName, string identityId, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentityId = identityId;
            UserName = userName;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string IdentityId { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public IReadOnlyCollection<RefreshTokenEntity> RefreshTokens => refreshTokens.AsReadOnly();

        public IReadOnlyCollection<JobTaskUserEntity> AssignedTasks => assignedTasks.AsReadOnly();

        public bool HasValidRefreshToken(string refreshToken)
        {
            return refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
        }

        public void AddRefreshToken(string token, int userId, string remoteIpAddress, double daysToExpire = 5)
        {
            refreshTokens.Add(new RefreshTokenEntity(token, DateTime.UtcNow.AddDays(daysToExpire), userId,
                remoteIpAddress));
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            refreshTokens.Remove(refreshTokens.First(t => t.Token == refreshToken));
        }
    }
}