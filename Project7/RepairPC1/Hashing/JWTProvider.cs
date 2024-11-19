using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepairPC1.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepairPC1.Hashing
{
    public class JwtProvider : IJwtProvider
    {
        //private readonly JwtOptions _options;

        //public JWTProvider(IOptions<JwtOptions> options) 
        //{
        //    _options = options.Value;   
        //}
        public string GenerateToken(Master master)
        {
            Claim[] claims = [
                new("MasterId", master.Id.ToString()),
                new("Role", master.Role.ToString()),
                new("Name", master.Name.ToString()),
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
