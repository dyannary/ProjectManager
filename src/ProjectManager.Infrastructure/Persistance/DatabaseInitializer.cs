using System.Data.Entity;

namespace ProjectManager.Infrastructure.Persistance
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            base.Seed(context);
        }
    }
}
