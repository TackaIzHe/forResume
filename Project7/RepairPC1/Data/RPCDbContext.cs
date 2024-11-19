using Microsoft.EntityFrameworkCore;

namespace RepairPC1.Data
{
    public class RPCDbContext:DbContext
    {
        public void AplicationConfig() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Order> orders => Set<Order>();
        public DbSet<Master> masters => Set<Master>();

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(Localdb)\mssqllocaldb;Database=Site;Trusted_connection=True;");
        }

        public Master GetMaster(string Email)
        {
            var masters =new RPCDbContext().masters.ToList();

            return masters.Find(x => x.Email == Email) ?? null;       
        }
        public List<Order> GetOrders()
        {
            return new RPCDbContext().orders.ToList();
        }
        public Order GetOrder(int id)
        {
            return new RPCDbContext().orders.ToList().Find(x => x.Id == id);
        }
        public void SetOrder(Order order)
        {
            RPCDbContext db = new RPCDbContext();
            db.orders.Add(order);
			db.SaveChanges();
        }
        public void EditOrder(int id, int materId, string stat)
        {
            RPCDbContext db = new RPCDbContext();
            var order = db.orders.ToList().Find(x => x.Id == id);
            int index = db.orders.ToList().IndexOf(order);
            order.Stat = stat;
            order.MasterId = materId;
            db.orders.ToList().Insert(index, order);
            db.SaveChanges();
        }

    }
    
}
