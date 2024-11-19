using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RepairPC1.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            var cooc = Request.Cookies.ToList().Find(x => x.Key == "may");
            if (cooc.Value != null)
                Response.Cookies.Delete("may");
            Response.Redirect("/Login");
        }
    }
}
