using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Data;

namespace RepairPC1.Pages
{
    [Authorize(policy: "MasterPolicy")]
    public class ViewOrdersModel : PageModel
    {
        public List<Order> orders = new RPCDbContext().GetOrders();
        
        public void OnGet()
        {
            orders = orders.ToList().FindAll(x => x.Stat == "Wait");
        }

        public IActionResult OnPost(int id)
        {
            return RedirectToPage("/ContextOrder", new {id=id});
        }

    }
}
