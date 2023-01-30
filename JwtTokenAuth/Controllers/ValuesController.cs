using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtTokenAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ValuesController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // comment  [Authorize] to work this code
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Please give token" });
            }

            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.Username == "johndoe" && user.Password == "pass123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                         SecurityAlgorithms.HmacSha256Signature
                         )
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    Id = user.Id,
                    username = user.Username,
                    token = tokenString
                });
            }

            return Unauthorized();
        }
    }
}
