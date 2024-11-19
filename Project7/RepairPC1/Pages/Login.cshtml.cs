using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Data;
using RepairPC1.Endpoints;
using RepairPC1.Hashing;
using RepairPC1.Masters;
using RepairPC1.Services;

namespace RepairPC1.Pages
{

    
    public class LoginModel : PageModel
    {
       
        UsersServices usersServices = new UsersServices(
            new PasswordHasher(), 
            new JwtProvider());
        public string Result {  get; set; }


        public void OnGet()
        {
            var cooc = Request.Cookies.ToList().Find(x => x.Key == "may");
            if (cooc.Value != null)
                Response.Cookies.Delete("may");
        }
        public void OnPost(string Email, string Password)
        {
           Task<string> a = UsersEndpoints.Login(Email, Password, usersServices, this.Response);
            Result=a.Result;
        }


        

    }
}
