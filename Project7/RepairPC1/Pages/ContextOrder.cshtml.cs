using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Data;

namespace RepairPC1.Pages
{
    [Authorize(policy:"MasterPolicy")]
    public class ContextOrderModel : PageModel
    {
        public Order order {  get; set; }
        int i = 0; 
        public void OnGet()
        {
            if(i == 0)
            {

            string id = Request.RouteValues.Values.Last().ToString();
			int.TryParse( id, out i);
            order = new RPCDbContext().GetOrder(i);   
            }
		}

        public IActionResult OnPost(string masterId,string orderId)
        {
            int MasterID = 0;
            int OrderID = 0;
            int.TryParse(masterId, out MasterID);
            int.TryParse(orderId, out OrderID);
            Order order = new RPCDbContext().GetOrder(OrderID);
            
			if (order.Stat == "Accept")
            {
				new RPCDbContext().EditOrder(OrderID, MasterID, "Complite");
				return RedirectToPage("/ContextOrder", new { id = OrderID });
			}

            new RPCDbContext().EditOrder(OrderID, MasterID, "Accept");
            return RedirectToPage("/ContextOrder", new { id = OrderID });
        }
    }
}
