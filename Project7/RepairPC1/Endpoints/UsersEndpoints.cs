using Azure;
using RepairPC1.Masters;
using RepairPC1.Pages;
using RepairPC1.Services;

namespace RepairPC1.Endpoints
{
    public static class UsersEndpoints
    {
        
        public static async Task<IResult> Register(string Name, string Email, string Password, string Role, UsersServices usersServices)
        {
            await usersServices.Register(Name, Email, Password, Role);
            return Results.Ok();
        }

        public static async Task<string> Login(
            string Email, 
            string Password, 
            UsersServices usersServices,
            HttpResponse context)
        {
            var token = await usersServices.Login(Email, Password);

            if (token == "error")
            {
                
                return "error";
            }
            context.Cookies.Append("may", token);
            if (token != null)
            {
                if(token=="UserNotFound")
                {
                    return "UserNotFound";
                }
                if(token == "ErrorPass")
                {
					return "ErrorPass";
				}
                
                context.Redirect("/ViewOrders");
            }
            return "Ok";
        }

    }
}
