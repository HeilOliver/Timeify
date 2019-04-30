using System;
using System.Collections.Generic;
using Timeify.Core.Interfaces.Gateways;

namespace Timeify.Core.Entities
{
    public class JobEntity : BaseEntity
    {
        private readonly List<JobTaskEntity> jobTasks = new List<JobTaskEntity>();

        internal JobEntity()
        {
            /* Required by EF */
        }

        public JobEntity(string name, string description, string ownerId)
        {
            Name = name;
            Description = description;
            OwnerId = ownerId;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; }

        public IReadOnlyCollection<JobTaskEntity> JobTasks => jobTasks.AsReadOnly();

        public JobTaskEntity AddTask(string name, string description, DateTime finishDate)
        {
            var taskEntity = new JobTaskEntity(name, description, finishDate, Id);
            jobTasks.Add(taskEntity);
            return taskEntity;
        }
    }
}