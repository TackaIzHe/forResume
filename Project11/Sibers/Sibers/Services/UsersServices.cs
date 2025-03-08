using Sibers.Data;
using Sibers.Hashing;
using Sibers.Objects;
using Task = System.Threading.Tasks.Task;

namespace Sibers.Services
{
    public class UsersServices
    {
        DBcontext db = new DBcontext();
        JwtProvider jwtProvider;
        PasswordHasher passwordHasher;

        public UsersServices()
        {
            passwordHasher = new PasswordHasher();
            jwtProvider = new JwtProvider();
        }
        public async Task Register(Person person)
        {
            var hashedPassword = await passwordHasher.Generate(person.password);
            person.password = hashedPassword;
            
            db.postPerson(person);
        }

        public async Task<string> Login(string Email, string Password)
        {
            var person = db.findPersonEmail(Email);
            if (person == null)
            {
                return StatusCodes.Status404NotFound.ToString();
            }
            var result = await passwordHasher.Verify(Password, person.password);

            if (result == false)
            {
                return StatusCodes.Status404NotFound.ToString();
            }
            else
            {
                var token = jwtProvider.GenerateToken(person);
                return token;
            }
        }
    }
}
