using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Data;
using Sibers.Objects;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy:"DirectorPolicy")]
    public class PutPersonModel : PageModel
    {
        DBcontext db = new DBcontext();
        public Person person;
        int personId;
        public void OnGet()
        {
            int.TryParse(Request.Query.First().Key,out personId);
            person = db.findPerson(personId);
        }
    }
}
