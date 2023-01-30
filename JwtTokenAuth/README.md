---
created: 2023-01-31T00:41:06 (UTC +06:00)
tags: []
source: https://chat.openai.com/chat/5e09ec7e-33db-477d-a7ff-e6a2898e4c8f
author: 
---

# JWT Token Auth in .NET Core WebAPI

> ## Excerpt
> A conversational AI system that listens, learns, and challenges

---
how to implement jwt token auth in dotnet core webapi

To implement JWT token authentication in a .NET Core Web API, you need to follow these steps:

1.  Install Microsoft.AspNetCore.Authentication.JwtBearer NuGet package.
2.  Add services in Startup.cs ConfigureServices method:
    
    ```cs
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    
    ```
    
3.  Add authentication middleware in Startup.cs Configure method:
    
    ```cs
    app.UseAuthentication();
    
    ```
    
4.  Add Authorize attribute to protect Web API controllers or methods:
    
    ```cs
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // ...
    }
    
    ```
    
5.  Generate JSON Web Tokens in the Login method and return it to the client:
    
    ```cs
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);
    
    return Ok(new {
        Id = user.Id,
        username = user.Username,
        token = tokenString
    });
    
    ```
    

This is a basic implementation of JWT token authentication in a .NET Core Web API. It can be further customized according to your requirements.


give me full code with file name and folder structure

Here's the complete code for a .NET Core Web API with JWT token authentication, including the file names and folder structure:

1.  Create a new .NET Core Web API project.
2.  Install the Microsoft.AspNetCore.Authentication.JwtBearer NuGet package.
3.  Create a Models folder and add a User.cs file:
    
    ```cs
    using System;
    using System.ComponentModel.DataAnnotations;
    
    namespace JwtTokenAuth.Models
    {
        public class User
        {
            public int Id { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
    
    ```
    
4.  Create a Controllers folder and add a ValuesController.cs file:
    
    ```cs
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
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
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
    
                    return Ok(new {
                        Id = user.Id,
                        username = user.Username,
                        token = tokenString
                    });
                }
    
                return Unauthorized();
            }
        }
    }
    
    ```
    
5.  Add appsettings.json file in the root directory:
    

```cs
   {
       "AppSettings": {
           "Token": "supersecretkeyformyapp"
       }
   }

```

6.  In Startup.cs, configure the JWT authentication middleware in the ConfigureServices method:
    
    ```cs
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    namespace JwtTokenAuth
    {
        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }
    
            public IConfiguration Configuration { get; }
    
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            }
        }
    }
    
    ```
    
7.  In the Configure method, add app.UseAuthentication() and app.UseAuthorization():
    
    ```cs
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    
    namespace JwtTokenAuth
    {
        public class Startup
        {
            // ... other code ...
    
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
    
                app.UseHttpsRedirection();
    
                app.UseRouting();
    
                app.UseAuthentication();
                app.UseAuthorization();
    
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
    
    ```
    

This should be enough to get you started with JWT token authentication in .NET Core Web API.


2 / 2

how to test in which endpoint to get a token, and how to use the token to get data

1.  To test getting a token, you can create a POST request to the /api/auth/login endpoint with a JSON payload that contains the user credentials (username and password).
2.  In the response, you should receive a JWT token. You can then use this token to access authorized routes.
3.  To use the token, you need to pass it in the Authorization header with the Bearer schema:

```cs
Authorization: Bearer <token>

```

4.  In your API controller methods, you can use the \[Authorize\] attribute to specify which methods require authentication:

```cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}

```

5.  You can test this by sending a GET request to the /api/values endpoint with the token passed in the Authorization header. If the token is valid and the user has the necessary permissions, you should receive the data.

You can use tools like Postman or cURL to test your API and send HTTP requests.


give response for unauthorized get request

In the case of an unauthorized GET request, the API should return a 401 Unauthorized HTTP response.

To achieve this, you can use the \[Authorize\] attribute on your controllers or specific actions to require authentication. If a user tries to access a protected endpoint without providing a valid JWT token, they will receive a 401 Unauthorized response.

Here's an example of how the code could look like in the controller:

```cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}

```

give a response body with " please give token "

Here's an example of how you could return a response with a message "Please give token" in the case of an unauthorized GET request:

```cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Please give token" });
            }

            return new string[] { "value1", "value2" };
        }
    }
}

```

In this example, the `User.Identity.IsAuthenticated` property is used to check if the user is authenticated, and if not, a 401 Unauthorized response with a custom message "Please give token" is returned.
