using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RepairPC1.Extensions
{
    public static class ApiExtensions
    {

        public static void addMappedEndpoints(this IEndpointRouteBuilder app)
        {

        }

        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("qwedafe23rdsfvxqweasdsdgrtwer235gdfgsdf423reqwefdsf"))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["may"];

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(Options =>
			{
				Options.AddPolicy("AdminPolicy", policy =>
				{
					policy.RequireClaim("Role", "Admin");
				});

                Options.AddPolicy("MasterPolicy", policy =>
                {
                    policy.RequireClaim("Role", "Master","Admin");
                });
			});
        }

    }
}
