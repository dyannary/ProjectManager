namespace ProjectManager.Infrastructure.Persistance
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_photoPath_In_Projects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "PhotoPath", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "PhotoPath");
        }
    }
}
