using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"EmployeePolicy")]
    public class TaskModel : PageModel
    { 
        DBcontext db = new DBcontext();
        public Objects.Task task = new Objects.Task();
        int taskId;
        public void OnGet()
        {
            int.TryParse(Request.Query.First().Key, out taskId);
            task = db.findTask(taskId);
        }
    }
}
