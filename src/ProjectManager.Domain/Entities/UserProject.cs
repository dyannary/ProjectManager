﻿namespace ProjectManager.Domain.Entities
{
    public class UserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public int ProjectRoleId { get; set; }
        public virtual UserProjectRole UserProjectRole { get; set; }
    }
}
