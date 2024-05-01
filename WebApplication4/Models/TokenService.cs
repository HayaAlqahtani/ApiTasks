using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication4.Controllers;
using static WebApplication4.Controllers.LoginController;

namespace WebApplication4.Models
{
        public class TokenService
        {
            private readonly IConfiguration _configuration;

            public TokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }
        private readonly BankContext context;

        public TokenService(BankContext context)
        {
            this.context = context;
        }
        public (bool IsValid, string Token) GenerateToken(string username, string password)
            {
             //   if (username != "admin" && password != "admin")
              //  {
              //      return (false, "");
               // }
            var userAccount = context.UserAccounts.SingleOrDefault(r => r.Username == username);
            if (userAccount == null)
            {
                return (false, "");
            }
            var validPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, userAccount,Password);
            if (!validPassword)
            {
                return (false, "");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credintials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
              new Claim(TokenClaimsConstant.Username, username),
              new Claim(TokenClaimsConstant.UserId, userAccount.Id.ToString()),
              new Claim(ClaimTypes.Role, userAccount.IsAdmin ? "admin" : "user")
};

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);
                var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
                return (true, generatedToken);
            }
        }
    }

