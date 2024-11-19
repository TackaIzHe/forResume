using RepairPC1.Data;

namespace RepairPC1.Hashing
{
    public interface IJwtProvider
    {
        string GenerateToken(Master master);
    }
}
