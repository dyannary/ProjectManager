using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjectManager.Infrastructure.DataSeeder.Seeds
{
    public class ProjectsSeed
    {
        public static List<Project> Seed()
        {
            return new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    Name = "First Project",
                    Description = "First Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 6, 2),
                    ProjectEndDate = new DateTime(2024, 7, 3),
                    ProjectStateId = 1,
                    UserProjects = new List<UserProject>(),
                    ProjectTasks = new List<ProjectTask>() 
                },
                new Project
                {
                    Id = 2,
                    Name = "Second Project",
                    Description = "Second Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 5, 2),
                    ProjectEndDate = new DateTime(2024, 8, 12),
                    ProjectStateId = 2,
                    UserProjects = new List<UserProject>(),
                    ProjectTasks = new List<ProjectTask>()
                },
                new Project
                {
                    Id = 3,
                    Name = "Third Project",
                    Description = "Third Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 7, 1),
                    ProjectEndDate = new DateTime(2024, 9, 12),
                    ProjectStateId = 1,
                    UserProjects = new List<UserProject>(),
                    ProjectTasks = new List<ProjectTask>()
                }
            };
        }
    }
}
