using Microsoft.AspNetCore.Mvc;
using Sibers.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Security.Claims;

namespace Sibers.Pages.UserPages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/UserPages/LogOut");
            }
        }

        public async void OnPost(
            string email,
            string password) 
        {
            string token = await new UsersServices().Login(email, password);
            Response.Cookies.Append("Cookie",token);
            Response.Redirect("/UserPages/Profile");
        }
    }
}
