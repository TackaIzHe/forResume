using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sibers.Data;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"DirectorPolicy")]
    public class TasksModel : PageModel
    {
        DBcontext db = new DBcontext();
        public List<Objects.Task> tasks = new List<Objects.Task>();
        public void OnGet()
        {
            tasks = db.getTask();
        }
        public void OnPost(string method, int id)
        {
            if(method == "delete")
            {
                db.delTask(id);
                OnGet();
            }
            else if(method == "put")
            {

            }
            
        }
    }
}
