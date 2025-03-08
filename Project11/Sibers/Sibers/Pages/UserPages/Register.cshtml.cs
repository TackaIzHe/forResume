using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sibers.Objects;
using Sibers.Services;

namespace Sibers.Pages.UserPages
{
    [Authorize(policy: "DirectorPolicy")]
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {

        }
        public async void OnPost(
            string name,
            string surname,
            string patronymic,
            string email,
            string password,
            Role role
            )
        {
            await new UsersServices().Register(new Person
            {
                name= name,
                surname= surname,
                patronymic= patronymic,
                email= email,
                password= password,
                role=role
            });
            
        }
    }
}
