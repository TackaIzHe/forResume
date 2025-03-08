using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;
using System.Text.Json;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"EmployeePolicy")]
    public class ProjectModel : PageModel
    {
        DBcontext db = new DBcontext();
        public Project project;
        int projId;
        public List<Objects.Task> tasks = new List<Objects.Task>();
        public void OnGet()
        {
            int.TryParse(Request.Query.First().Key,out  projId);
            project = db.findProject(projId);
            project.idTasks.ForEach(x=>{
                tasks.Add(db.findTask(x));
                
            });
            Console.WriteLine(JsonSerializer.Serialize(project.idTasks));
            
        }
        
    }
}
