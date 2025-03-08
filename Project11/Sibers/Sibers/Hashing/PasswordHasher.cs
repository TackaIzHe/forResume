namespace Sibers.Hashing
{
    public class PasswordHasher
    {
        public async Task<string> Generate(string password)
        {
            await Task.Delay(0);
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public async Task<bool> Verify(string password, string hashedPassword)
        {
            await Task.Delay(0);
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
