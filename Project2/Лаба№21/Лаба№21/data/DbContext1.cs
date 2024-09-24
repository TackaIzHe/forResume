using Microsoft.EntityFrameworkCore;

namespace Лаба_21.data
{
    public class DbContext1 : DbContext
    {

        public void ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Facul> Faculs => Set<Facul>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Student> Students => Set<Student>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DATABASE5.MDF;Trusted_Connection=True;");
        }
    }
}
