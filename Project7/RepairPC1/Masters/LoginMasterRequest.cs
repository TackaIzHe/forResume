using System.ComponentModel.DataAnnotations;

namespace RepairPC1.Masters
{
    public record LoginMasterRequest
    (
        [Required] string Email,
        [Required] string Password
    );
}
