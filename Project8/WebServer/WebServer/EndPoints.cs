
using System.Text.Json;
using System.Text.Json.Serialization;
using WebServer.data;
using WebServer.Objects;

namespace WebServer
{
	public static class EndPoints
	{
		
		public static void addEndPoints(this IEndpointRouteBuilder app)
		{
			DBContext dbContext = new DBContext();
			var getUsers = app.MapGroup("/Users");
			getUsers.MapGet("/", () => dbContext.GetUsers());
			getUsers.MapGet("/{Id}", (int Id) => dbContext.GetUser(Id));

			app.MapDelete("/DeleteUser/{Id}", (int Id) => {
				dbContext.RemoveUser(dbContext.FindUser(Id));
				return Task.CompletedTask;
				});

			app.MapPut("/UpdateUser/", (context) =>
			{
				var user = context.Request.ReadFromJsonAsync<User>();
				dbContext.UpdateUser(user.Result);
				return Task.CompletedTask;
			});
				


			app.MapPost("/AddUser/",(context) => {
				var user = context.Request.ReadFromJsonAsync<User>();
				dbContext.AddUser(user.Result);
				return Task.CompletedTask;
				});

		}
	}
}
