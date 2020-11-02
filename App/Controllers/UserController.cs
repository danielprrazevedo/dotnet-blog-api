using System;
using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.App.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase<User>
    {
        protected new readonly IUserService _service;
        public UserController(IUserService service) : base(service) => _service = service;


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)
        {
            var user = await _service.AuthUser(auth.username, auth.password);
            if (user == null) return Unauthorized();
            var token = _service.GenerateToken(user);
            var result = new
            {
                user = user,
                token = token
            };
            return Json(result);
        }
    }

    public class Auth
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}