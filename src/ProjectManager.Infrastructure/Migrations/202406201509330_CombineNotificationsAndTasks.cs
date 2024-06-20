namespace ProjectManager.Infrastructure.Persistance
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CombineNotificationsAndTasks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "FileTypeId", "dbo.FileTypes");
            DropIndex("dbo.Files", new[] { "FileTypeId" });
            DropColumn("dbo.Files", "ProjectTaskId");
            RenameColumn(table: "dbo.Files", name: "FileTypeId", newName: "ProjectTaskId");
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "ProjectTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "ProjectTaskId");
            DropTable("dbo.FileTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Files", new[] { "ProjectTaskId" });
            AlterColumn("dbo.Files", "ProjectTaskId", c => c.Int());
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false, maxLength: 70));
            RenameColumn(table: "dbo.Files", name: "ProjectTaskId", newName: "FileTypeId");
            AddColumn("dbo.Files", "ProjectTaskId", c => c.Int());
            CreateIndex("dbo.Files", "FileTypeId");
            AddForeignKey("dbo.Files", "FileTypeId", "dbo.FileTypes", "Id", cascadeDelete: true);
        }
    }
}
