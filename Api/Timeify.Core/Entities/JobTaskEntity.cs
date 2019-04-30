using System;
using System.Collections.Generic;
using System.Linq;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Core.Entities
{
    public class JobTaskEntity : BaseEntity
    {
        private readonly List<JobTaskUserEntity> assignedUsers = new List<JobTaskUserEntity>();

        internal JobTaskEntity()
        {
            /* Required by EF */
        }

        public JobTaskEntity(string name, string description, DateTime finishDate, int jobEntityId)
        {
            Name = name;
            Description = description;
            FinishDate = finishDate;
            JobEntityId = jobEntityId;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

        public int JobEntityId { get; }

        public IReadOnlyCollection<JobTaskUserEntity> AssignedUsers => assignedUsers.AsReadOnly();

        public void AssignUser(string userName)
        {
            var entity = GetAssignedUser(userName);
            if (entity != null) return;

            assignedUsers.Add(new JobTaskUserEntity(Id, userName));
        }

        public void UnAssignUser(string userName)
        {
            var entity = GetAssignedUser(userName);
            if (entity == null) return;

            assignedUsers.Remove(entity);
        }

        private JobTaskUserEntity GetAssignedUser(string username)
        {
            var userEntity = assignedUsers
                .FirstOrDefault(link => link.UserName.Equals(username));
            return userEntity;
        }
    }
}