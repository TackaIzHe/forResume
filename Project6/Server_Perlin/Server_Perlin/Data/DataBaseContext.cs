using Microsoft.EntityFrameworkCore;


namespace Server_Perlin.Data
{
    public class DataBaseContext : DbContext
    {
        public void AplicationConfig()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Post> posts => Set<Post>();
        public DbSet<comment> comments => Set<comment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=POSTS.MDF;Trusted_Connection=True;");
        }
    }
}
