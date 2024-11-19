using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Data;

namespace RepairPC1.Pages.UserPages
{
    [Authorize(policy: "MasterPolicy")]
    public class UserPageModel : PageModel
    {
        public List<Order> orders = new RPCDbContext().GetOrders();
        int masterId = 0;
        public void OnGet(string stat="Accept")
        {
            
            User.Claims.ToList().ForEach(x =>
            {
                if (x.Type == "MasterId")
                {
                    int.TryParse(x.Value, out masterId);
                }
            });

            orders = orders.FindAll(x => x.MasterId == masterId && x.Stat == stat);

        }

        public IActionResult OnPost(int id)
        {
            return RedirectToPage("/ContextOrder", new { id });
        }
    }
}
