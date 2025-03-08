using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"ManagerPolicy")]
    public class NewTaskModel : PageModel
    {
        DBcontext db = new DBcontext();
        public List<Person> employee = new List<Person>();
        public int Id;
        public void OnGet()
        {
            db.getPerson().ForEach(x => {
                if(x.role == Role.Employee)
                    employee.Add(x);
                });
            int.TryParse(Request.Query.First().Key, out Id);
        }
    }
}
