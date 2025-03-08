using Microsoft.IdentityModel.Tokens;
using Sibers.Objects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sibers.Hashing
{
    public class JwtProvider
    {
        public string GenerateToken(Person person)
        {
            Console.WriteLine(person.role);
            Claim[] claims = [
                new("personId", person.id.ToString()),
                new("Role", person.role.ToString()),
                new("name", person.name.ToString()),
                ];


            var signingCredentails = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(/*_options.SecretKey*/ "qwedafe23rdsfvxqweasdsdgrtwer235gdfgsdf423reqwefdsf")),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentails,
                expires: DateTime.UtcNow.AddHours(/*_options.ExpitesHours*/ 12));


            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
