using Sibers.Objects;

namespace Sibers.Services
{
    public static class Authorization
    {
        public static void AddAuthorization(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthorization(Options =>
            {
                Options.AddPolicy("DirectorPolicy", policy =>
                {
                    policy.RequireClaim("Role", Role.Director.ToString());
                });

                Options.AddPolicy("ManagerPolicy", policy =>
                {
                    policy.RequireClaim("Role", Role.Director.ToString(), Role.Manager.ToString());
                });

                Options.AddPolicy("EmployeePolicy", policy =>
                {
                    policy.RequireClaim("Role", Role.Director.ToString(), Role.Manager.ToString(), Role.Employee.ToString());
                });
                
            });
        }
    }
}
