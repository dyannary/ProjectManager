namespace ProjectManager.Infrastructure.Persistance
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPhotoPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhotoPath");
        }
    }
}
