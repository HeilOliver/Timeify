namespace Timeify.Core.Entities
{
    public class JobTaskUserEntity
    {
        internal JobTaskUserEntity()
        {
            /* Required by EF */
        }

        public JobTaskUserEntity(int jobTaskId, string userName)
        {
            JobTaskId = jobTaskId;
            UserName = userName;
        }

        public int JobTaskId { get; }

        public JobTaskEntity JobTaskEntity { get; private set; }

        public string UserName { get; }

        public UserEntity UserEntity { get; private set; }
    }
}