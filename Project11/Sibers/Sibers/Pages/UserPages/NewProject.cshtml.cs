using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Sibers.Data;
using Sibers.Objects;
using System.Text.Json;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"DirectorPolicy")]
    
    public class NewProjectModel : PageModel
    {
        DBcontext db = new DBcontext();
        public List<Person> employee = new List<Person>();
        public List<Person> manager = new List<Person>();
        public void OnGet()
        {
            employee = db.getPerson().FindAll(x => x.role == Role.Employee);
            manager = db.getPerson().FindAll(x => x.role==Role.Manager);

        }
    }
}
