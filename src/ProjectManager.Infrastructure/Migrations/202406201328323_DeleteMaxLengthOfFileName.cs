namespace ProjectManager.Infrastructure.Persistance
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMaxLengthOfFileName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
