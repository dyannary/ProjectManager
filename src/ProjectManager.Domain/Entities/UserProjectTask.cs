using ProjectManager.Domain.Common;

namespace ProjectManager.Domain.Entities
{
    public class UserProjectTask
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProjectTaskId { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
    }
}
