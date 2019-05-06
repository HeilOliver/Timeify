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

        public int JobTaskId { get; private set; }

        public JobTaskEntity JobTaskEntity { get; private set; }

        public string UserName { get; private set; }

        public UserEntity UserEntity { get; private set; }
    }
}