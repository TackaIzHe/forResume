using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Endpoints;
using RepairPC1.Hashing;

namespace RepairPC1.Pages
{
    [Authorize(Policy ="AdminPolicy")]
    public class RegisterModel : PageModel
    {
        Services.UsersServices userServices =new Services.UsersServices(
            new PasswordHasher(), 
            new JwtProvider());
        public void OnGet()
        {
        }
        public void OnPost(string Name, string Email, string Password, string Role)
        {
            UsersEndpoints.Register(Name, Email, Password, Role, userServices);
        }
    }
}
