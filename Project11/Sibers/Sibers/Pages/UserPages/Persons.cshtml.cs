using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"DirectorPolicy")]
    public class PersonsModel : PageModel
    {
        DBcontext db = new DBcontext();
        public List<Person> people = new List<Person>();
        public void OnGet()
        {
            people = db.getPerson();
        }
        public void OnPost(string method, int id)
        {
            if(method == "delete")
            {
                db.delPerson(id);
                OnGet();
            }
            else if(method == "put")
            {
                Response.Redirect($"/UserPages/PutPerson?{id}");
            }
        }
    }
}
