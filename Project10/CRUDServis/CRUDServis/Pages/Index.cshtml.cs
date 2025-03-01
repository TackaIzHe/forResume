using CRUDServis.Data;
using CRUDServis.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CRUDServis.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		public List<User> Users = new List<User>();
		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			Users = new DBcontext().get();
        }

		public void OnPost
		(
			string method, 
			int user, 
			string name,
			string department,
			string dsw,
			string db,
			string statys,
			int salary
		)
		{
			if(method=="put")
			{
				put(new User
                {
					Id = user,
                    Name = name,
                    Department = department,
                    DSW = dsw,
                    DB = db,
                    Statys = statys,
                    Salary = salary
                });
			}
			else if(method=="delete")
			{
				delete(user);
			}
			else if(method=="post")
			{
				post(new User 
				{
					Name = name,
					Department = department,
					DSW = dsw,
					DB = db,
					Statys = statys,
					Salary = salary
				});
			}
		}


		public void post(User user)
		{
			var statusCode=new DBcontext().post(user);
			if(statusCode==400)
			{
				Console.WriteLine(JsonSerializer.Serialize(user));
			}
			else
			{
				Console.WriteLine(JsonSerializer.Serialize(user));
				Console.WriteLine($"OK: {statusCode}");
            }
			Response.Redirect("/");
		}

		void put(User user)
		{
			new DBcontext().put(user);
            Console.WriteLine(user.Id);
            Response.Redirect("/");
        }

		void delete(int user)
		{
			new DBcontext().delete(user);
			Console.WriteLine(user);
            Response.Redirect("/");
        }
	}
}
