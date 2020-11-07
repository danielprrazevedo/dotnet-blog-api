

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


        [HttpGet("username-exists/{id}")]
        public async Task<IActionResult> GetCheckUsernameExists(string id) {
            var user = await _service.GetUserByUsername(id);
            if (user != null) {
                return Json(new { exists = true });
            }
            return Json(new { exists = false });
        }
    }
}