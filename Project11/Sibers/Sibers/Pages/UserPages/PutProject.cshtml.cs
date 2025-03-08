using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"ManagerPolicy")]
    public class PutProjectModel : PageModel
    {
        int idProj;
        DBcontext db = new DBcontext();
        public Project project = new Project(); 
        public List<Person> employee = new List<Person>();
        public List<Person> manager = new List<Person>();
        public void OnGet()
        {
            int.TryParse(Request.Query.First().Key, out idProj);
            project = db.findProject(idProj);
            employee = db.getPerson().FindAll(x => x.role == Role.Employee);
            manager = db.getPerson().FindAll(x => x.role == Role.Manager);
        }
    }
}
