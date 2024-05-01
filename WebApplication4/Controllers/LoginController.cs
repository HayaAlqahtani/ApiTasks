using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService service;

        public LoginController(TokenService service)
        {
            this.service = service;
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var response = this.service.GenerateToken(username, password);

            if (response.IsValid)
            {
                return Ok(new { Token = response.Token });
            }
            else
            {
                return BadRequest("Username and/or Password is wrong");
            }
        }

        public class UserLogin()
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
    public class TokenClaimsConstant
    {
        public static readonly string Username = "Kfh.Username";
        public static readonly string UserId = "Kfh.UserId";
    }
}
