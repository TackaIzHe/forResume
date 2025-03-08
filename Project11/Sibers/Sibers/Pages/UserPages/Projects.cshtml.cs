using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"DirectorPolicy")]
    public class ProjectsModel : PageModel
    {
        DBcontext dbcontext = new DBcontext();
        public List<Project> projects = new List<Project>();
        public void OnGet()
        {
            projects = dbcontext.getProject();
            
        }
        public void OnPost(string method,int id)
        {
            if (method == "delete")
            {
                dbcontext.delProject(id);
                OnGet();
            }
            else if (method == "put")
                Response.Redirect($"/UserPages/PutProject?{id}");
        }
    }
}
