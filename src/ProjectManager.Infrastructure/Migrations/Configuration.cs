using System.Data.Entity.Migrations;

namespace ProjectManager.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProjectManager.Infrastructure.Persistance.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectManager.Infrastructure.Persistance.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
