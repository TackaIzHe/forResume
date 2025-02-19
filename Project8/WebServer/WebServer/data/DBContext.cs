using Microsoft.EntityFrameworkCore;
using WebServer.Objects;

namespace WebServer.data
{
	public class DBContext:DbContext
	{

		public void AplicatioConfig()
		{
			if(!Database.CanConnect())
			{
				Database.EnsureDeleted();
				Database.EnsureCreated();
			}
		}

		DbSet<User> users => Set<User>();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ServerData;Trusted_connection=True");
		}

		public User GetUser(int id)
		{
			var user = new DBContext().users.ToList();
			return user.Find(x => x.id == id);
		}

		public List<User> GetUsers()
		{
			return new DBContext().users.ToList();
		}

		public void AddUser(User user)
		{
			var db = new DBContext();
			db.users.Add(user);
			db.SaveChanges();
		}

		public void RemoveUser(User user)
		{
			var db = new DBContext();
			db.users.Remove(user);
			db.SaveChanges();
		}

		public void UpdateUser(User user)
		{
			var db = new DBContext();
			var oldUser = db.users.ToList().Find(x => x.id == user.id);
			db.users.Remove(oldUser);
			db.users.Add(user);
			db.SaveChanges();
		}
		public User FindUser(int id)
		{
			return new DBContext().users.ToList().Find(x => x.id == id);
		}


	}
}
