using System.Threading.Tasks;
using BlogApi.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.App.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)
        {
            var user = await _userService.AuthUser(auth.username, auth.password);
            if (user == null) return Unauthorized();
            var token = _userService.GenerateToken(user);
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