using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;
using Sibers.Services;
using System.Text.Json;

namespace Sibers.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            if(!User.Identity.IsAuthenticated)
            {
                Response.Redirect("UserPages/Login");
            }
            

        }


    }
}
