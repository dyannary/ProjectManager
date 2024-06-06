using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjectManager.Infrastructure.DataSeeder.Seeds
{
    public class ProjectTasksSeed
    {
        public static List<ProjectTask> Seed()
        {
            return new List<ProjectTask>()
            {
                new ProjectTask()
                {
                    Name = "Task for first project",
                    Description = "This is a task",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1, 
                    TaskTypeId = 1, 
                    TaskStateId = 1,
                    ProjectId = 1,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 4, // Assuming the user id
                            User = null, // You can set this to null as it will be overwritten by the context later
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "Another task for first project",
                    Description = "This is a task for second project",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 8, 22),
                    PriorityId = 2,
                    TaskTypeId = 1, 
                    TaskStateId = 1,
                    ProjectId = 1,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 6, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "Third task for first project",
                    Description = "This is a task",
                    TaskStartDate = new DateTime(2024, 8, 22),
                    TaskEndDate = new DateTime(2024, 9, 10),
                    PriorityId = 2,
                    TaskTypeId = 3,
                    TaskStateId = 1,
                    ProjectId = 1,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 6, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "2 project task seed",
                    Description = "This is a task for second project",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1,
                    TaskTypeId = 1,
                    TaskStateId = 1,
                    ProjectId = 2,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 4, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "1 Project task seed",
                    Description = "This is a task",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1,
                    TaskTypeId = 1,
                    TaskStateId = 1,
                    ProjectId = 1,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 6, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "2 project task seed",
                    Description = "This is a task for second project",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1,
                    TaskTypeId = 1,
                    TaskStateId = 1,
                    ProjectId = 2,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 4, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "1 Project task seed",
                    Description = "This is a task",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1,
                    TaskTypeId = 1,
                    TaskStateId = 1,
                    ProjectId = 2,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 6, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
                new ProjectTask()
                {
                    Name = "2 project task seed",
                    Description = "This is a task for third project",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1,
                    TaskTypeId = 1,
                    TaskStateId = 1,
                    ProjectId = 3,
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                    {
                        new UserProjectTask()
                        {
                            UserId = 9, // Assuming the user id
                            ProjectTaskId = 1 // Assuming the project task id
                        }
                    }
                },
            };
        }
    }
}
