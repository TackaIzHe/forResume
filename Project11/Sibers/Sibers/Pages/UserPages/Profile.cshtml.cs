using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"EmployeePolicy")]
    public class ProfileModel : PageModel
    {
        public DBcontext dbcontext = new DBcontext();

        public void OnGet()
        {

        }
        public List<Project> findProjects(int id)
        {
            var projects = new List<Project>();
            foreach (var item in dbcontext.getProject())
            {
                if (item.idPersons.Contains(id))
                {
                    projects.Add(item);
                }
            }
            return projects;
        }
    }
}
