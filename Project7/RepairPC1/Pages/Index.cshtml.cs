using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairPC1.Data;
using System.Text.RegularExpressions;

namespace RepairPC1.Pages
{
	public class IndexModel : PageModel
	{
		

		public string order { get; set; }
		private readonly ILogger<IndexModel> _logger;
		Regex regex = new Regex("[0-9]{11}");
		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			
        }
		public void OnPost(
			string name,
			string numb,
			string adress,
			string servise1,
			string servise2,
			string servise3)
		{
			string[] servises = { servise1, servise2, servise3 };
			int i = 0;
			int price = 0;
			while (i < servises.Length)
			{
				if (servises[i] == null)
				{
					i++;
					continue;
				}
				else
				{
					if (i == 0)
					{
						price += 500;
					}

					if (i == 1)
					{

						price += 500;
					}

					if (i == 2)
					{
						price += 1000;
					}
					i++;
				}
			}


			if (name == null ||
				regex.Count(numb) != 1 ||
				numb.Length != 11 ||
				adress == null ||
				price==0
				)
			{
				this.order = "Проверьте коректность введёных данных";
				return;
			}

			Order orders = new Order
			{
				Name = name,
				Tel = numb,
				Stat = "Wait",
				Price = price,
				MasterId = 0,
				Adress = adress,
				Services =servises

				
			};
			this.order = "Спасибо что оставили заявку. Ожидайте звонка";
			new RPCDbContext().SetOrder(orders);
		}
	}
}

