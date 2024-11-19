using Microsoft.AspNetCore.Identity;
using RepairPC1.Data;
using RepairPC1.Hashing;

namespace RepairPC1.Services
{
    public class UsersServices
    {
        RPCDbContext db = new RPCDbContext();

        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public UsersServices(
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string name, string email, string password, string Role)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            
            var master = new Master { Name = name,Email= email,Password=hashedPassword, Role = Role};
            db.masters.Add(master);
            await db.SaveChangesAsync();
        }

        public async Task<string> Login(string Email, string Password)
        {
            var master = db.GetMaster(Email);
            if (master == null)
            {
                return "UserNotFound";
            }
            var result = _passwordHasher.Verify(Password, master.Password);

            if(result == false)
            {
                return "ErrorPass";
            }
            else
            {
            var token = _jwtProvider.GenerateToken(master);
            return token;
            }
            return "error";
        }
    }
}
