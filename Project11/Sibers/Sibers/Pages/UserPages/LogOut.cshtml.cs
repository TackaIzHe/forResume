using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"EmployeePolicy")]
    public class LogOutModel : PageModel
    {
        public void OnGet()
        {
            Response.Cookies.Delete("Cookie");
            Response.Redirect("/UserPages/Login");
        }
    }
}
