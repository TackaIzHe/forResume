using CRUDServis.Objects;
using Microsoft.EntityFrameworkCore;

namespace CRUDServis.Data
{
    public class DBcontext:DbContext
    {

        public DBcontext() 
        { 
            if(!Database.CanConnect())
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=list;Trusted_connection=True");
        }

        DbSet<User> Users => Set<User>();

        public List<User> get()
        {
            return new DBcontext().Users.ToList();
        }
        public void put(User user)
        {
            var db = new DBcontext();
            db.Users.Update(user);
            db.SaveChanges();

        }

        public void delete(int id)
        {
            var db = new DBcontext();
            db.Users.Remove(db.find(id));
            db.SaveChanges();
        }

        public int post(User user)
        {
            if
            (
                user.Name == null ||
                user.Department == null ||
                user.Statys == null ||
                user.DB == null ||
                user.DSW == null ||
                user.Salary == 0
            )
            {
                return StatusCodes.Status400BadRequest;
            }
            else
            {
                var db = new DBcontext();
                db.Users.Add(user);
                db.SaveChanges();
                return StatusCodes.Status200OK;
            }
        }

        public User find(int id)
        {
            return new DBcontext().Users.ToList().Find(x => x.Id == id);
        }

    }
    
}
