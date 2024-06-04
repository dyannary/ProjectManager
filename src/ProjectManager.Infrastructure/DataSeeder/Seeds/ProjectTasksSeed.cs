using ProjectManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectManager.Infrastructure.DataSeeder.Seeds
{
    public class ProjectTasksSeed
    {
        public static List<ProjectTask> SeedProjectTasks()
        {
            return new List<ProjectTask>()
            {
                new ProjectTask()
                {
                    Id = 0,
                    Name = "Add project task seed",
                    Description = "This is a task",
                    PriorityId = 0,
                }
            };
        }
    }
}
