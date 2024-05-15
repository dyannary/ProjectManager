namespace ProjectManager.Infrastructure.Persistance
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 70),
                        FileData = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        FileTypeId = c.Int(nullable: false),
                        ProjectTaskId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileTypes", t => t.FileTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTasks", t => t.FileTypeId, cascadeDelete: true)
                .Index(t => t.FileTypeId);
            
            CreateTable(
                "dbo.FileTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        TaskStartDate = c.DateTime(nullable: false),
                        TaskEndDate = c.DateTime(nullable: false),
                        TaskTypeId = c.Int(nullable: false),
                        TaskStateId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        Created = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Priorities", t => t.PriorityId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTaskStates", t => t.TaskStateId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTaskTypes", t => t.TaskTypeId, cascadeDelete: true)
                .Index(t => t.TaskTypeId)
                .Index(t => t.TaskStateId)
                .Index(t => t.ProjectId)
                .Index(t => t.PriorityId);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PriorityValue = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        ProjectEndDate = c.DateTime(nullable: false),
                        ProjectStartDate = c.DateTime(nullable: false),
                        ProjectStateId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        Created = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectStates", t => t.ProjectStateId, cascadeDelete: true)
                .Index(t => t.ProjectStateId);
            
            CreateTable(
                "dbo.ProjectStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ProjectRoleId = c.Int(nullable: false),
                        UserProjectRole_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserProjectRoles", t => t.UserProjectRole_Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserProjectRole_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 150),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        IsEnabled = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        Created = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProjectTasks",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectTaskId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTasks", t => t.ProjectTaskId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectTaskId);
            
            CreateTable(
                "dbo.UserProjectRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTaskStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProjectTasks", "ProjectTaskId", "dbo.ProjectTasks");
            DropForeignKey("dbo.ProjectTasks", "TaskTypeId", "dbo.ProjectTaskTypes");
            DropForeignKey("dbo.ProjectTasks", "TaskStateId", "dbo.ProjectTaskStates");
            DropForeignKey("dbo.UserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.UserProjects", "UserProjectRole_Id", "dbo.UserProjectRoles");
            DropForeignKey("dbo.UserProjectTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ProjectTasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectStateId", "dbo.ProjectStates");
            DropForeignKey("dbo.ProjectTasks", "PriorityId", "dbo.Priorities");
            DropForeignKey("dbo.Files", "FileTypeId", "dbo.ProjectTasks");
            DropForeignKey("dbo.Files", "FileTypeId", "dbo.FileTypes");
            DropIndex("dbo.UserProjectTasks", new[] { "ProjectTaskId" });
            DropIndex("dbo.UserProjectTasks", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.UserProjects", new[] { "UserProjectRole_Id" });
            DropIndex("dbo.UserProjects", new[] { "ProjectId" });
            DropIndex("dbo.UserProjects", new[] { "UserId" });
            DropIndex("dbo.Projects", new[] { "ProjectStateId" });
            DropIndex("dbo.ProjectTasks", new[] { "PriorityId" });
            DropIndex("dbo.ProjectTasks", new[] { "ProjectId" });
            DropIndex("dbo.ProjectTasks", new[] { "TaskStateId" });
            DropIndex("dbo.ProjectTasks", new[] { "TaskTypeId" });
            DropIndex("dbo.Files", new[] { "FileTypeId" });
            DropTable("dbo.ProjectTaskTypes");
            DropTable("dbo.ProjectTaskStates");
            DropTable("dbo.UserProjectRoles");
            DropTable("dbo.UserProjectTasks");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.UserProjects");
            DropTable("dbo.ProjectStates");
            DropTable("dbo.Projects");
            DropTable("dbo.Priorities");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.FileTypes");
            DropTable("dbo.Files");
        }
    }
}
