
using System.ComponentModel.DataAnnotations;

namespace RepairPC1.Masters
{
    public record RegisterMasterRequest
        (
        [Required] string Name,
        [Required] string Email,
        [Required] string Password
    );
    
}
