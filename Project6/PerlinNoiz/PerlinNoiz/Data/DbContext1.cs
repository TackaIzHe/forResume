using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PerlinNoiz.Data
{
    public class DbContext1 : DbContext
    {
        public void ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<BitmapNoiz> bitmapNoizs => Set<BitmapNoiz>();
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DATABASEIMAGE.MDF;Trusted_Connection=True;");
    }
}
}
